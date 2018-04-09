(function () {
    var id = $("#Id").val();
    startTabListener($("#sections"), id);
    loadUrlInDiv("#sections", "/Notifications/GetNotificationsForEntity?entityName=courses&id=" + id);
})();