
(function() {

    var btn = $(".js-add-section");
    var service = abp.services.app.scholarshipSection;
    var modalIsOpen = false;

    $("body").on("click",
        ".js-edit-section",
        function() {
            var id = $(this).data("id");
            window.eModal.ajax({
                    loadingHtml:
                        '<span class="fa fa-circle-o-notch fa-spin fa-3x text-primary"></span><span class="h4">Cargando</span>',
                    url: '/Scholarships/CreateOrEditSection?id=' + id+'&scholarshipId=' + $("#Id").val(),
                    title: 'Notificación',
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
                                save();
                            }
                        }
                    ]
                })
                .then(function () {
                    bindEnter();
                });
        });

    btn.click(function() {
        window.eModal.ajax({
                loadingHtml:
                    '<span class="fa fa-circle-o-notch fa-spin fa-3x text-primary"></span><span class="h4">Cargando</span>',
                url: '/Scholarships/CreateOrEditSection?scholarshipId=' + $("#Id").val(),
                title: 'Notificación',
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
                            save();
                        }
                    }
                ]
            })
            .then(function () {
                bindEnter();
            });
    });
    function save() {
        var data = $("#AddEditScholarshipSectionForm").serializeFormToObject();
        service.createOrUpdate(data).done(function () {
            window.eModal.close();
            modalIsOpen = false;
            unBindEnter();
            abp.notify.success("Sección creada");

            abp.event.trigger('sections');

        });
    }
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

    $("body").on("click", ".js-delete-scholarshipSection", function () {
        var id = $(this).data("id");
        abp.message.confirm("¿Desea eliminar este elemento?", function(response) {
            if (response) {
                service.delete(id).done(function () {
                    abp.notify.success("Sección eliminada");
                    abp.event.trigger('sections');
                });
            }
        });
    });
})();