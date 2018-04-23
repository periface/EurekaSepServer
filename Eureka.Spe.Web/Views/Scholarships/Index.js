(function () {
    var service = abp.services.app.scholarship;
    var table = $("#table");
    var modalIsOpen = false;
    function startTable() {
        table.bootstrapTable({
            method: "post",
            locale: 'es-Es',
            columns: [
                {
                    title: "Titulo", field: "title", sortable: true, formatter: (value, row, index) => {
                        return `${value}`;
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
                        var btnEdit = `<a class="btn btn-default btn-xs waves-effect waves-teal btn-flat js-edit-scholarship" data-id="${row.id}"><i data-id="${row.id}" class="material-icons left">edit</i></a>`;
                        var btnDelete = `<a class="btn btn-danger btn-xs waves-effect waves-teal btn-flat js-delete-scholarship" data-id="${row.id}"><i data-id="${row.id}" class="material-icons left">delete</i></a>`;
                        var btnAdvanced = `<a href="/Scholarships/Manage/${row.id}" class="btn btn-default btn-xs waves-effect waves-teal btn-flat js-edit-category" data-id="${row.id}"><i data-id="${row.id}" class="material-icons left">build</i></a>`;
                        return [
                            btnAdvanced,
                            btnEdit,
                            btnDelete
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
    $(".js-add-scholarship").click(function () {
        modalIsOpen = true;
        window.eModal.ajax({
            loadingHtml:
            '<span class="fa fa-circle-o-notch fa-spin fa-3x text-primary"></span><span class="h4">Cargando</span>',
            url: '/Scholarships/CreateOrEdit/',
            title: 'Beca',
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

    $("body").on("click", ".js-edit-scholarship", function () {
        var id = $(this).data("id");
        window.eModal.ajax({
            loadingHtml:
            '<span class="fa fa-circle-o-notch fa-spin fa-3x text-primary"></span><span class="h4">Cargando</span>',
            url: '/Scholarships/CreateOrEdit/' + id,
            title: 'Beca',
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

    $("body").on("click",
        ".js-delete-scholarship",
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

    function save() {
        var data = $("#AddEditScholarshipForm").serializeFormToObject();
        service.createOrUpdate(data).done(function () {
            window.eModal.close();
            table.bootstrapTable('refresh');
            modalIsOpen = false;
            unBindEnter();
            abp.notify.success("Elemento guardado con exito...");
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

})();