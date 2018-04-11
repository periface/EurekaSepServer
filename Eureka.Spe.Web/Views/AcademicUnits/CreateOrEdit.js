(function () {
    var uploadOptions = {
        baseUrl: "/upload",
        sizes: ["128x128"],
        optimize: true,
        uniqueFolder: "academicunits",
        onUploadStart: function () {
            abp.ui.setBusy($("#upload"));
        },
        onUploading: function (xhr) {
            if (xhr === 100) {
                abp.notify.success("Archivo enviado...");
                abp.notify.info("Procesando archivo...");
            }
        },
        onUploadDone: function (data) {
            $("#img").val(data.FileUrl);
            $("#iconPlaceHolder").attr("src", data.FileUrl);
            abp.ui.clearBusy($("#upload"));
            abp.notify.info("Archivo procesado...");
        },
        onUploadError: function (err) {
            abp.ui.clearBusy($("#upload"));
        }
    };
    enableUploadOnFileElm("#icon", uploadOptions);
    var form = $("#AddEditAcademicUnitForm");
    $.AdminBSB.input.activate(form);
})();