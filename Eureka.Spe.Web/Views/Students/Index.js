(function() {
    var table = $("#table");
    var service = abp.services.app.student;
    function startTable() {
        table.bootstrapTable({
            method: "post",
            locale: 'es-Es',
            columns: [
                {
                    title: "Alumno", field: "enrollCode", sortable: false, formatter: (value, row, index) => {

                        var html = `<div class="row"> 
                                        <div class="col-lg-3" style="width:10%;">
                                            <img style="width:32px;height:32px;" src="${row.img}" />
                                        </div>
                                        <div class="col-lg-9">
                                            <p style="margin:0;"> ${row.name} ${row.surname} </p> <p style="margin:0;" class="help-block"> ${row.enrollCode} </p>
                                        </div>
                                    </div>`;
                        //`<a href="/Students/Manage/${row.id}">${value}</a>`
                        return html;
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
                        var btnEdit = `<a href="/Students/CreateOrEdit/${row.id}" class="btn btn-default btn-xs waves-effect waves-teal btn-flat" data-id="${row.id}"><i data-id="${row.id}" class="material-icons left">edit</i></a>`;
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
})();