﻿"use strict";

(function () {

    var table = $("#table");
    var service = abp.services.app.publisher;
    function startTable() {
        table.bootstrapTable({
            method: "post",
            locale: 'es-Es',
            columns: [{
                title: "Logo", field: "img", sortable: false, align: "center", formatter: function formatter(value, row, index) {
                    return "<img style=\"width:32px;height:32px;\" src=\"" + value + "\" />";
                }
            }, {
                title: "Nombre", field: "name", sortable: true, formatter: function formatter(value, row, index) {
                    return "" + value;
                }
            }, {
                title: "Acciones",
                formatter: function formatter(value, row, index) {
                    var btnEdit = "<a class=\"btn btn-primary btn-xs waves-effect waves-teal btn-flat js-edit-publisher\" data-id=\"" + row.id + "\"><i data-id=\"" + row.id + "\" class=\"material-icons left\">edit</i></a>";
                    var btnDelete = "<a class=\"btn btn-danger btn-xs waves-effect waves-teal btn-flat js-delete-publisher\" data-id=\"" + row.id + "\"><i data-id=\"" + row.id + "\" class=\"material-icons left\">delete</i></a>";

                    return [btnEdit, btnDelete].join(' ');
                }
            }],
            toolbar: ".toolbar",
            clickToSelect: true,
            showRefresh: true,
            search: true,
            showToggle: true,
            showColumns: true,
            pagination: true,
            pageSize: 8,
            pageList: [8, 10, 25, 50, 100],
            formatRecordsPerPage: function formatRecordsPerPage(pageNumber) {
                return pageNumber + " rows visible";
            },
            icons: {
                refresh: 'fa fa-refresh',
                toggle: 'fa fa-th-list',
                columns: 'fa fa-columns',
                detailOpen: 'fa fa-plus-circle',
                detailClose: 'fa fa-minus-circle'
            },
            responseHandler: function responseHandler(res) {
                return { rows: res.result.items, total: res.result.totalCount };
            }
        });
    }

    startTable();

    $("body").on("click", ".js-edit-publisher", function () {
        var id = $(this).data("id");

        window.eModal.ajax({
            loadingHtml: '<span class="fa fa-circle-o-notch fa-spin fa-3x text-primary"></span><span class="h4">Cargando</span>',
            url: '/Publishers/CreateOrEdit/' + id,
            title: 'Crear categoría',
            buttons: [{
                text: 'Cerrar', style: 'danger', close: true, click: function click() {}
            }, {
                text: 'Guardar', style: 'info', close: false, click: function click(elm) {
                    var data = $("#AddEditPublisherForm").serializeFormToObject();
                    service.createOrUpdate(data).done(function () {
                        window.eModal.close();
                        table.bootstrapTable('refresh');
                        abp.notify.success("Elemento guardado con exito...");
                    });
                }
            }]
        }).then(function () {});
    });
    $("body").on("click", ".js-delete-publisher", function () {
        var id = $(this).data("id");

        abp.message.confirm("Desea eliminar este elemento?", function (response) {
            if (response) {
                service["delete"](id).done(function () {
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
            title: 'Crear categoría',
            buttons: [{
                text: 'Cerrar', style: 'danger', close: true, click: function click() {}
            }, {
                text: 'Guardar', style: 'info', close: false, click: function click(elm) {
                    var data = $("#AddEditPublisherForm").serializeFormToObject();
                    console.log(data);
                    service.createOrUpdate(data).done(function () {
                        window.eModal.close();
                        table.bootstrapTable('refresh');
                        abp.notify.success("Elemento guardado con exito...");
                    });
                }
            }]
        }).then(function () {});
    });
})();

