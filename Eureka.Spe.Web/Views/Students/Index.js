(function() {
    var table = $("#table");
    var service = abp.services.app.student;
    function startTable() {
        table.bootstrapTable({
            method: "post",
            locale: 'es-Es',
            columns: [
                {
                    title: "Matricula", field: "enrollCode", sortable: false, align: "center", formatter: (value, row, index) => {
                        return `<a href="/Students/Manage/${row.id}">${value}</a>`;
                    }
                },
                {
                    title: "Perfil", field: "img", sortable: false, align: "center", formatter: (value, row, index) => {
                        return `<img style="width:32px;height:32px;" src="${value}" />`;
                    }
                },
                {
                    title: "Nombre", field: "name", sortable: true, formatter: (value, row, index) => {
                        return `${value}`;
                    }
                },
                {
                    title: "Apellidos", field: "surname", sortable: true, formatter: (value, row, index) => {
                        return `${value}`;
                    }
                },
                {
                    title: "Acciones",
                    formatter: (value, row, index) => {
                        var btnEdit = `<a class="btn btn-primary btn-xs waves-effect waves-teal btn-flat js-edit-student" data-id="${row.id}"><i data-id="${row.id}" class="material-icons left">edit</i></a>`;
                        var btnDelete = `<a class="btn btn-danger btn-xs waves-effect waves-teal btn-flat js-delete-student" data-id="${row.id}"><i data-id="${row.id}" class="material-icons left">delete</i></a>`;

                        return [
                            btnEdit,
                            btnDelete
                        ].join(' ');
                    }
                }
            ],
            toolbar: ".toolbar",
            clickToSelect: true,
            showRefresh: true,
            search: true,
            showToggle: true,
            showColumns: true,
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
        ".js-edit-student",
        function() {
            var id = $(this).data("id");
            console.log(id);
            window.eModal.ajax({
                    loadingHtml: '<span class="fa fa-circle-o-notch fa-spin fa-3x text-primary"></span><span class="h4">Cargando</span>',
                    url: '/Students/CreateOrEdit/' + id,
                    title: 'Estudiante',
                    buttons: [
                        {
                            text: 'Cerrar', style: 'danger', close: true, click: function () {

                            }
                        },
                        {
                            text: 'Guardar', style: 'info', close: false, click: function (elm) {
                                var data = $("#AddEditStudentForm").serializeFormToObject();
                                service.createOrUpdate(data).done(function () {
                                    window.eModal.close();
                                    table.bootstrapTable('refresh');
                                    abp.notify.success("Elemento guardado con exito...");
                                });
                                console.log(data);
                            }
                        }
                    ]
                })
                .then(function () {

                });
        });

    $("body").on("click",
        ".js-delete-student",
        function() {

            var id = $(this).data("id");

            abp.message.confirm("¿Eliminar Elemento?", function(response) {
                if (response) {
                    service.delete(id).done(function () {
                        table.bootstrapTable('refresh');
                        abp.notify.success("Elemento eliminado con éxito...");
                    });
                }
            });

        });


    $(".js-add-student").click(function() {
        
        window.eModal.ajax({
                loadingHtml: '<span class="fa fa-circle-o-notch fa-spin fa-3x text-primary"></span><span class="h4">Cargando</span>',
                url: '/Students/CreateOrEdit/',
                title: 'Estudiante',
                buttons: [
                    {
                        text: 'Cerrar', style: 'danger', close: true, click: function () {

                        }
                    },
                    {
                        text: 'Guardar', style: 'info', close: false, click: function (elm) {
                            var data = $("#AddEditStudentForm").serializeFormToObject();
                            service.createOrUpdate(data).done(function () {
                                window.eModal.close();
                                table.bootstrapTable('refresh');
                                abp.notify.success("Elemento guardado con exito...");
                            });
                            console.log(data);
                        }
                    }
                ]
            })
            .then(function () {

            });
    });




})();