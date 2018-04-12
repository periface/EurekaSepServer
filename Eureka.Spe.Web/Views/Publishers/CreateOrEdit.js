(function () {
    var uploadOptions = {
        baseUrl: "/upload",
        sizes: ["128x128"],
        optimize: true,
        uniqueFolder: "publishers",
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
            $("#img").val(data.Sizes["128x128"]);
            $("#iconPlaceHolder").attr("src", data.Sizes["128x128"]);
            abp.ui.clearBusy($("#upload"));
            abp.notify.info("Archivo procesado...");
        },
        onUploadError: function (err) {
            abp.ui.clearBusy($("#upload"));
        }
    };
    enableUploadOnFileElm("#icon", uploadOptions);
    var form = $("#AddEditPublisherForm");
    $.AdminBSB.input.activate(form);
})();