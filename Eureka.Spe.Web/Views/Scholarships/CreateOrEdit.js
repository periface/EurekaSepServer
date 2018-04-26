(function () {
    $("#iconPlaceHolder").click(function () {
        $("#icon").click();
    });
    var service = abp.services.app.scholarship;
    var uploadOptions = {
        baseUrl: "/upload",
        sizes: ["128x128"],
        optimize: true,
        uniqueFolder: "scholarships",
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
    enableUploadOnFileElm("#icon", uploadOptions);
    var form = $("#AddEditScholarshipForm");
    $.AdminBSB.input.activate(form);
    function startDates() {

        $('.datetimepicker-r-start').bootstrapMaterialDatePicker({
            format: "YYYY-MM-DD",
            time: false,
            lang: 'es'
        }).on("change", function (e, date) {
            $('.datetimepicker-r-end').bootstrapMaterialDatePicker('setMinDate', date);
        });
        $('.datetimepicker-r-end').bootstrapMaterialDatePicker({
            format: "YYYY-MM-DD",
            time: false,
            lang: 'es'
        }).on("change", function (e, date) {
            $('.datetimepicker-start').bootstrapMaterialDatePicker('setMinDate', date);
        });
    }

    startDates();


    $("#AddEditScholarshipForm").on("submit",
        function (e) {
            e.preventDefault();
            save();
        });
    function save() {
        var data = $("#AddEditScholarshipForm").serializeFormToObject();
        service.createOrUpdate(data).done(function (response) {

            abp.notify.success("Elemento guardado con exito...");

            reloadPage(response);
        });
    }
})();