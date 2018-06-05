(function () {
    var service = abp.services.app.courseTheme;
    let courseId = $("#courseId").val();
    overrideNotify();
    $(".js-add-theme").click(function () {
        openCreateEditWindow(null, courseId);
    });
    $('body').on('click',
        '.js-edit-theme',
        function () {

            var id = $(this).data('id');
            openCreateEditWindow(id, 0);
        });

    function openCreateEditWindow(themeId, courseId) {
        window.eModal.ajax({
            loadingHtml: '<span class="fa fa-circle-o-notch fa-spin fa-3x text-primary"></span><span class="h4">Cargando</span>',
            url: '/Courses/CreateOrEditCourseTheme?courseId=' + courseId + "&id="+themeId,
            title: 'Tema',
            buttons: [
                {
                    text: 'Cerrar', style: 'danger', close: true, click: function () {
                    }
                },
                {
                    text: 'Guardar', style: 'info', close: false, click: function (elm) {
                        var data = $("#AddEditCourseThemeForm").serializeFormToObject();
                        service.createOrEditTheme(data).done(function () {
                            window.eModal.close();
                            abp.notify.success("Elemento guardado con exito...");
                        });
                    }
                }
            ]
        })
            .then(function () {
            });
    }
})();