(function() {

    var table = $("#table");

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
                    title: "Dependencia", field: "publisherName", sortable: true, formatter: (value, row, index) => {
                        return `${value}`;
                    }
                },
                {
                    title: "Acciones",
                    formatter: (value, row, index) => {
                        var btnEdit = `<a href="/Feeds/CreateOrEdit/${row.id}" class="btn btn-primary btn-xs waves-effect waves-teal btn-flat js-edit-category" data-id="${row.id}"><i data-id="${row.id}" class="material-icons left">edit</i></a>`;
                        var btnDelete = `<a class="btn btn-danger btn-xs waves-effect waves-teal btn-flat js-delete-category" data-id="${row.id}"><i data-id="${row.id}" class="material-icons left">delete</i></a>`;
                        var btnAdvanced = `<a href="/Feeds/Manage/${row.id}" class="btn btn-default btn-xs waves-effect waves-teal btn-flat js-edit-category" data-id="${row.id}"><i data-id="${row.id}" class="material-icons left">build</i></a>`;
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


    $(".js-add-category").click(function() {
        window.eModal.ajax({
                loadingHtml: '<span class="fa fa-circle-o-notch fa-spin fa-3x text-primary"></span><span class="h4">Cargando</span>',
                url: '/Feeds/CreateOrEdit',
                title: 'Crear categoría',
                buttons: [
                    {
                        text: 'Cerrar', style: 'danger', close: true, click: function () {

                        }
                    },
                    {
                        text: 'Guardar', style: 'info', close: false, click: function (elm) {
                            var data = $("#createCategory").serializeFormToObject();
                            console.log(data);
                        }
                    }
                ]
            })
            .then(function () {

            });


    });
})();