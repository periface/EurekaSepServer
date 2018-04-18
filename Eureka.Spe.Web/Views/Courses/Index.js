(function() {

    var table = $("#table");

    function startTable() {
        table.bootstrapTable({
            method: "post",
            locale: 'es-Es',
            columns: [
                {
                    title: "Titulo", field: "title", sortable: true, formatter: (value, row, index) => {
                        return `<p style="margin:0;"><strong>${value}</strong></p><p style="margin:0;">${row.categoryName}</p>`;
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
                        var btnAdvanced = `<a href="/Courses/Manage/${row.id}" class="btn btn-default btn-xs waves-effect waves-teal btn-flat js-edit-category" data-id="${row.id}"><i data-id="${row.id}" class="material-icons left">build</i></a>`;
                        var btnEdit = `<a href="/Courses/CreateOrEdit/${row.id}" class="btn btn-default btn-xs waves-effect waves-teal btn-flat js-edit-category" data-id="${row.id}"><i data-id="${row.id}" class="material-icons left">edit</i></a>`;
                        var btnDelete = `<a class="btn btn-danger btn-xs waves-effect waves-teal btn-flat js-delete-category" data-id="${row.id}"><i data-id="${row.id}" class="material-icons left">delete</i></a>`;
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



})();