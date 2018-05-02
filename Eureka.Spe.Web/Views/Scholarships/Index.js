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

                        var btnAdvanced = `<a href="/Scholarships/Manage/${row.id}" class="waves-effect waves-teal js-edit-category" data-id="${row.id}"><i data-id="${row.id}" class="material-icons left">build</i>Administrar</a>`;

                        var btns = `<div class="dropdown"><a href="#" class="dropdown-toggle" data-toggle="dropdown" role="button" aria-haspopup="true" aria-expanded="false">
                                        <i class="material-icons">menu</i>
                                    </a>
                                    <ul class="dropdown-menu pull-right">
                                        <li>${btnAdvanced}</li>
                                        <li><a href="/Scholarships/CreateOrEdit/${row.id}" class="waves-effect waves-block" data-id="${row.id}"><i class="material-icons">edit</i>Editar</a></li>
                                        <li><a href="#" class="waves-effect waves-block js-delete-scholarship" data-id="${row.id}"><i class="material-icons">delete_sweep</i>Eliminar</a></li>
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
        
})();