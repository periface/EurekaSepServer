(function () {

    var table = $("#table");
    var service = abp.services.app.courseCategories;
    var modalIsOpen = false;
    function startTable() {
        table.bootstrapTable({
            method: "post",
            locale: 'es-Es',
            columns: [
                {
                    title: "Nombre", field: "name", sortable: true, formatter: (value, row, index) => {
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

                        var btns = `<div class="dropdown"><a href="#" class="dropdown-toggle" data-toggle="dropdown" role="button" aria-haspopup="true" aria-expanded="false">
                                        <i class="material-icons">menu</i>
                                    </a>
                                    <ul class="dropdown-menu pull-right">
                                        <li><a <a href="#" class="waves-effect waves-block js-edit-category" data-id="${row.id}"><i class="material-icons">edit</i>Editar</a></li>
                                        <li><a href="#" class="waves-effect waves-block js-delete-category" data-id="${row.id}"><i class="material-icons">delete_sweep</i>Eliminar</a></li>
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

    $(".js-add-category").click(function () {
        modalIsOpen = true;
        window.eModal.ajax({
            loadingHtml: '<span class="fa fa-circle-o-notch fa-spin fa-3x text-primary"></span><span class="h4">Cargando</span>',
            url: '/CourseCategories/CreateOrEdit/',
            title: 'Crear categoría',
            buttons: [
                {
                    text: 'Cerrar', style: 'danger', close: true, click: function () {

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
    $("body").on("click", ".js-edit-category", function () {
        var id = $(this).data("id");
        modalIsOpen = true;
        window.eModal.ajax({
            loadingHtml: '<span class="fa fa-circle-o-notch fa-spin fa-3x text-primary"></span><span class="h4">Cargando</span>',
            url: '/CourseCategories/CreateOrEdit/' + id,
            title: 'Crear categoría',
            buttons: [
                {
                    text: 'Cerrar', style: 'danger', close: true, click: function () {

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
    $("body").on("click", ".js-delete-category", function () {
        var id = $(this).data("id");
        abp.message.confirm("¿Desea eliminar esta categoría?", function(response) {
            if (response) {
                service.delete(id).done(function() {
                    table.bootstrapTable('refresh');
                    abp.notify.success("Elemento eliminado con exito...");
                });
            }
        });
    });
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
    function save() {
        var data = $("#AddEditCategoryForm").serializeFormToObject();
        service.createOrUpdate(data).done(function () {
            window.eModal.close();
            table.bootstrapTable('refresh');
            abp.notify.success("Elemento guardado con exito...");
            modalIsOpen = false;
            unBindEnter();
        });
    }

})();