(function () {
    var service = abp.services.app.courseTheme;
    const courseId = $("#courseId").val();
    overrideNotify();
    $(".js-add-theme").click(function () {
        openCreateEditWindow(0, courseId);
    });
    $('body').on('click',
        '.js-edit-theme',
        function () {

            const id = $(this).data('id');
            openCreateEditWindow(id, 0);
        });
    $('body').on('click', '.js-delete-theme', function () {
        var id = $(this).data('id');
        abp.message.confirm('¿Desea eliminar este elemento?',
            function (response) {
                if (response) {
                    const result = deleteElm(id);
                    result.then(function () {
                        abp.notify.success('Elemento elminado');
                        abp.event.trigger('themes');
                    });
                }
            });
    });
    async function deleteElm(id) {
        await service.delete(id);
    }
    function openCreateEditWindow(themeId, courseId) {
        window.eModal.ajax({
            loadingHtml: '<span class="fa fa-circle-o-notch fa-spin fa-3x text-primary"></span><span class="h4">Cargando</span>',
            url: '/Courses/CreateOrEditCourseTheme?courseId=' + courseId + "&id=" + themeId,
            title: 'Tema',
            buttons: [
                {
                    text: 'Cerrar', style: 'danger', close: true, click: function () {
                    }
                },
                {
                    text: 'Guardar', style: 'info', close: false, click: function () {
                        const data = $("#AddEditCourseThemeForm").serializeFormToObject();
                        service.createOrEditTheme(data).done(function () {
                            window.eModal.close();
                            abp.notify.success("Elemento guardado con exito...");
                            abp.event.trigger('themes');
                        });
                    }
                }
            ]
        })
            .then(function () {
            });
    }
})();