(function () {

    var table = $("#table");
    var service = abp.services.app.publisher;
    function startTable() {
        table.bootstrapTable({
            method: "post",
            locale: 'es-Es',
            columns: [
                {
                    title: "Nombre", field: "name", sortable: true, formatter: (value, row, index) => {
                        return `<img style="width:32px;height:32px;" src="${row.img}" /> ${value}`;
                    }
                },
                {
                    title: "Ultima actualización", field: "lastModificationTime", sortable: true, formatter: (value, row, index) => {
                        var date = new Date(value).yyyymmddhhmm();
                        if (!value) {
                            return "-";
                        }
                        return `${date}`;
                    }
                },
                {
                    title: "Acciones",
                    formatter: (value, row, index) => {

                        var btns = `<div class="dropdown"><a href="#" class="dropdown-toggle" data-toggle="dropdown" role="button" aria-haspopup="true" aria-expanded="false">
                                        <i class="material-icons">menu</i>
                                    </a>
                                    <ul class="dropdown-menu pull-right">
                                        <li><a href="#" class="waves-effect waves-block js-edit-publisher" data-id="${row.id}"><i class="material-icons">edit</i>Editar</a></li>
                                        <li><a href="#" class="waves-effect waves-block js-delete-publisher" data-id="${row.id}"><i class="material-icons">delete_sweep</i>Eliminar</a></li>
                                    </ul></div>`;


                        return [
                            btns
                        ].join(' ');
                    }
                }
            ],
            toolbar: ".toolbar",
            clickToSelect: true,
            showRefresh: false,
            search: true,
            showToggle: false,
            showColumns: false,
            pagination: true,
            pageSize: 8,
            pageList: [8, 10, 25, 50, 100],
            formatRecordsPerPage: function (pageNumber) {
                return pageNumber + " rows visible";
            },
            icons: {
                refresh: 'fa fa-refresh',
                toggle: 'fa fa-th-list',
                columns: 'fa fa-columns',
                detailOpen: 'fa fa-plus-circle',
                detailClose: 'fa fa-minus-circle'
            },
            responseHandler: function (res) {
                return { rows: res.result.items, total: res.result.totalCount };
            }
        });
    }

    startTable();

    $("body").on("click",
        ".js-edit-publisher",
        function () {
            var id = $(this).data("id");

            window.eModal.ajax({
                loadingHtml: '<span class="fa fa-circle-o-notch fa-spin fa-3x text-primary"></span><span class="h4">Cargando</span>',
                url: '/Publishers/CreateOrEdit/' + id,
                title: 'Departamento',
                buttons: [
                    {
                        text: 'Cerrar', style: 'danger', close: true, click: function () {

                        }
                    },
                    {
                        text: 'Guardar', style: 'info', close: false, click: function (elm) {
                            var data = $("#AddEditPublisherForm").serializeFormToObject();
                            service.createOrUpdate(data).done(function () {
                                window.eModal.close();
                                table.bootstrapTable('refresh');
                                abp.notify.success("Elemento guardado con exito...");
                            });

                        }
                    }
                ]
            })
                .then(function () {

                });
        });
    $("body").on("click",
        ".js-delete-publisher",
        function () {
            var id = $(this).data("id");

            abp.message.confirm("Desea eliminar este elemento?",
                function (response) {
                    if (response) {
                        service.delete(id).done(function () {
                            table.bootstrapTable('refresh');
                            abp.notify.success("Elemento eliminado con exito...");
                        });
                    }
                });


        });
    $(".js-add-publisher").click(function () {
        window.eModal.ajax({
            loadingHtml: '<span class="fa fa-circle-o-notch fa-spin fa-3x text-primary"></span><span class="h4">Cargando</span>',
            url: '/Publishers/CreateOrEdit',
            title: 'Departamento',
            buttons: [
                {
                    text: 'Cerrar', style: 'danger', close: true, click: function () {

                    }
                },
                {
                    text: 'Guardar', style: 'info', close: false, click: function (elm) {
                        var data = $("#AddEditPublisherForm").serializeFormToObject();
                        console.log(data);
                        service.createOrUpdate(data).done(function () {
                            window.eModal.close();
                            table.bootstrapTable('refresh');
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