(function () {
    var table = $("#table");
    var service = abp.services.app.career;
    var selectedId = 0;
    $("body").on("click", ".js-delete-career", function () {
        var id = $(this).data("id");

        abp.message.confirm("¿Eliminar esta carrera?", function (response) {
            if (response) {
                service.delete(id).done(function () {
                    abp.notify.success("Elemento eliminado con exito...");
                    table.bootstrapTable("refresh", { query: { academicUnitId: selectedId } });
                });
            }
        });
    });
    $("body").on("click", ".js-edit-career", function () {
        var id = $(this).data("id");

        window.eModal.ajax({
            loadingHtml:
            '<span class="fa fa-circle-o-notch fa-spin fa-3x text-primary"></span><span class="h4">Cargando</span>',
            url: '/Careers/CreateOrEdit/' + id + '?selected=' + selectedId,
            title: 'Crear Unidad Academica',
            buttons: [
                {
                    text: 'Cerrar',
                    style: 'danger',
                    close: true,
                    click: function () {

                    }
                },
                {
                    text: 'Guardar', style: 'info', close: false, click: function (elm) {
                        var data = $("#AddEditCareerForm").serializeFormToObject();
                        service.createOrUpdate(data).done(function () {
                            window.eModal.close();
                            abp.notify.success("Elemento guardado con exito...");
                        });
                    }
                }
            ]
        })
            .then(function () {

            });

    });
    $(".js-add-career").click(function () {
        window.eModal.ajax({
            loadingHtml:
            '<span class="fa fa-circle-o-notch fa-spin fa-3x text-primary"></span><span class="h4">Cargando</span>',
            url: '/Careers/CreateOrEdit?selected=' + selectedId,
            title: 'Crear Unidad Academica',
            buttons: [
                {
                    text: 'Cerrar',
                    style: 'danger',
                    close: true,
                    click: function () {

                    }
                },
                {
                    text: 'Guardar', style: 'info', close: false, click: function (elm) {
                        var data = $("#AddEditCareerForm").serializeFormToObject();
                        service.createOrUpdate(data).done(function () {
                            window.eModal.close();
                            abp.notify.success("Elemento guardado con exito...");
                        });
                    }
                }
            ]
        })
            .then(function () {

            });


    });

})();