(function () {
    var id = $("#Id").val();
    startTabListener($("#sections"), id);
    loadUrlInDiv("#sections", "/Notifications/GetNotificationsForEntity?entityName=feeds&id=" + id);
})();