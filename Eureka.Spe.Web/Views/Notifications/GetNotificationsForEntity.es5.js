
///Requires jquery
'use strict';

(function () {
    var service = abp.services.app.notification;
    function openCreateModal() {
        window.eModal.ajax({
            loadingHtml: '<span class="fa fa-circle-o-notch fa-spin fa-3x text-primary"></span><span class="h4">Cargando</span>',
            url: '/Notifications/Schedule?entitype=' + $("#EntityName").val() + '&entityId=' + $("#Id").val(),
            title: 'Notificación',
            buttons: [{
                text: 'Cerrar',
                style: 'danger',
                close: true,
                click: function click() {}
            }, {
                text: 'Agendar', style: 'info', close: false, click: function click(elm) {
                    save();
                }
            }]
        }).then(function () {
            bindEnter();
        });
    }
    function deleteNotification() {
        var id = $(this).data("id");
        abp.message.confirm("¿Eliminar elemento?", function (response) {
            if (response) {
                service['delete'](id).done(function () {
                    abp.notify.success("Elemento eliminado...");
                    abp.event.trigger('notifications');
                });
            }
        });
    }
    window.openNotificationDetails = function (e) {
        var id = $(e).data("id");

        var container = $(".js-element-container-" + id);
        abp.ui.setBusy(container);
        container.load("/Notifications/GetStatusStats/" + id, function () {
            abp.ui.clearBusy(container);
        });
    };
    var modalIsOpen = false;
    $(".js-place-notification").click(openCreateModal);
    function save() {
        var data = $("#ScheduleForm").serializeFormToObject();
        console.log(data);
        data.NotifyDate = data.NotifyDate + " " + data.NotifyTime;
        service.createOrUpdate(data).done(function () {
            window.eModal.close();
            modalIsOpen = false;
            unBindEnter();
            abp.notify.success("Notificación agendada...");
            abp.event.trigger('notifications');
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
        $(document).on('keypress', 'input', enterFunc);
    }
    function unBindEnter() {
        $(document).unbind("keypress", enterFunc);
    }
    $("body").on("click", ".js-delete-notification", deleteNotification);
    function initSparkline() {
        $(".sparkline").each(function () {
            var $this = $(this);
            $this.sparkline('html', $this.data());
        });
    }

    initSparkline();
})();

