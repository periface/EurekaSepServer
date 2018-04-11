
(function () {
    var service = abp.services.app.feed;
    var tinyMce;
    startTinyMce("#content", function (instance) {
        tinyMce = instance;
    });
    var uploadOptions = {
        baseUrl: "/upload",
        sizes: ["128x128"],
        optimize: true,
        uniqueFolder: "feeds",
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

            abp.notify.error("Error procesando la imagen...");


        }
    };
    enableUploadOnFileElm("#icon", uploadOptions);

    $("#CreateEditFeedForm").on("submit",
        function (e) {
            e.preventDefault();
            $('button[type="submit"]').prop("disabled", true);
            var data = $(this).serializeFormToObject();
            data.content = tinyMce.save();
            service.createOrUpdate(data).done(function (response) {
                abp.notify.success("Cambios guardados...");
                $('button[type="submit"]').prop("disabled", false);
                reloadPage(response);
            });
        });
    $("#imgPlaceHolder").click(function () {
        $("#icon").click();
    });
})();