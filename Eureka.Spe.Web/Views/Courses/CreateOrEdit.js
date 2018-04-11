(function () {
    var service = abp.services.app.course;
    var tinyMce;
    startTinyMce("#content", function (instance) {
        tinyMce = instance;
    });
    var uploadOptions = {
        baseUrl: "/upload",
        sizes: ["128x128"],
        optimize: true,
        uniqueFolder: "courses",
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
            $("#imgPlaceHolder").attr("src", data.FileUrl);
            abp.ui.clearBusy($("#upload"));
            abp.notify.info("Archivo procesado...");
        },
        onUploadError: function (err) {
            abp.ui.clearBusy($("#upload"));
        }
    };
    enableUploadOnFileElm("#icon", uploadOptions);

    $("#CreateEditFeedForm").on("submit",
        function (e) {
            e.preventDefault();
            var data = $(this).serializeFormToObject();
            data.content = tinyMce.save();
            console.log(data);
            service.createOrUpdate(data).done(function () {
                abp.notify.success("Cambios guardados...");
            });
        });
    $("#imgPlaceHolder").click(function () {
        $("#icon").click();
    });
})();