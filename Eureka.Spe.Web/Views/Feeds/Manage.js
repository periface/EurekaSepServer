﻿(function () {
    var id = $("#Id").val();
    startTabListener($("#sections"), id);
    loadUrlInDiv("#sections", "/Notifications/GetNotificationsForEntity?entityName=feeds&id=" + id);
    abp.event.on('notifications', function () {
        loadUrlInDiv("#sections", "/Notifications/GetNotificationsForEntity?entityName=feeds&id=" + id);
    });
})();