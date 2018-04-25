(function () {
    var service = abp.services.app.student;
    $("#iconPlaceHolder").click(function () {
        $("#icon").click();
    });
    var uploadOptions = {
        baseUrl: "/upload",
        sizes: ["128x128"],
        optimize: true,
        uniqueFolder: "students",
        onUploadStart: function () {
            abp.ui.setBusy($("#upload"));
        },
        onUploading: function (xhr) {
            if (xhr === 100) {
            }
        },
        onUploadDone: function (data) {
            $("#img").val(data.FileUrl);
            $("#iconPlaceHolder").attr("src", data.FileUrl);
            abp.ui.clearBusy($("#upload"));
        },
        onUploadError: function (err) {
            abp.ui.clearBusy($("#upload"));
        }
    };

    $("#AddEditStudentForm").on("submit",
        function (e) {
            e.preventDefault();
            var data = $(this).serializeFormToObject();
            service.createOrUpdate(data).done(function (response) {
                abp.notify.success("Elemento guardado con éxito...");
                reloadPage(response);
            });
        });
    enableUploadOnFileElm("#icon", uploadOptions);
})();