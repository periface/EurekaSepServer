(function () {
    var service = abp.services.app.course;
    var categoryService = abp.services.app.courseCategories;
    $("#content").trumbowyg({
        lang: 'es',
        btnsDef: {
            // Create a new dropdown
            image: {
                dropdown: ['insertImage', 'base64'],
                ico: 'insertImage'
            }
        },
        autogrow: true,
        btns: [
            ['viewHTML'],
            ['formatting'],
            ['strong', 'em', 'del'],
            ['superscript', 'subscript'],
            ['link'],
            ['image'], // Our fresh created dropdown
            ['justifyLeft', 'justifyCenter', 'justifyRight', 'justifyFull'],
            ['unorderedList', 'orderedList'],
            ['horizontalRule'],
            ['removeformat'],
            ['fullscreen']
        ]
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
        onUploadError: function () {
            abp.ui.clearBusy($("#upload"));
        }
    };
    enableUploadOnFileElm("#icon", uploadOptions);

    $("#CreateEditFeedForm").on("submit",
        function (e) {
            e.preventDefault();
            var data = $(this).serializeFormToObject();
            service.createOrUpdate(data).done(function (response) {
                abp.notify.success("Cambios guardados...");
                reloadPage(response);
            });
        });
    $("#imgPlaceHolder").click(function () {
        $("#icon").click();
    });

    var modalIsOpen = false;
    $(".js-add-category").click(function () {
        modalIsOpen = true;
        window.eModal.ajax({
            loadingHtml: '<span class="fa fa-circle-o-notch fa-spin fa-3x text-primary"></span><span class="h4">Cargando</span>',
            url: '/CourseCategories/CreateOrEdit/',
            title: 'Crear categoría',
            buttons: [
                {
                    text: 'Cerrar', style: 'danger', close: true, click: function () {

                    }
                },
                {
                    text: 'Guardar', style: 'info', close: false, click: function (elm) {
                        save();
                    }
                }
            ]
        })
            .then(function () {
                bindEnter();
            });
    });

    function enterFunc(e) {
        if (e.keyCode === 13 && modalIsOpen) {
            e.preventDefault();
            save();
        }
    }

    function bindEnter() {
        unBindEnter();
        $(document).on('keypress',
            'input', enterFunc);
    }
    function unBindEnter() {
        $(document).unbind("keypress", enterFunc);
    }
    function save() {
        var data = $("#AddEditCategoryForm").serializeFormToObject();
        categoryService.createOrUpdate(data).done(function (response) {
            window.eModal.close();
            abp.notify.success("Elemento guardado con exito...");
            modalIsOpen = false;
            unBindEnter();
            reloadCategories(response);
        });
    }
    function reloadCategories(id) {
        $("#publisher").load("/layout/CourseCategorySelector?selected=" + id, function () {
            $.AdminBSB.select.activate();
        });
    }

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
        $('.datetimepicker-start').bootstrapMaterialDatePicker({
            format: "YYYY-MM-DD",
            time: false,
            lang: 'es'
        }).on("change", function (e, date) {
            $('.datetimepicker-end').bootstrapMaterialDatePicker('setMinDate', date);
        });
        $('.datetimepicker-end').bootstrapMaterialDatePicker({
            format: "YYYY-MM-DD",
            time: false,
            lang: 'es'
        }).on("change", function (e, date) {

        });
    }

    startDates();
})();