(function () {

    var service = abp.services.app.scholarship;

    var id = $("#Id").val();


    loadUrlInDiv("#sections", "/Scholarships/Sections/" + id);
    startTabListener("#sections", id);


    abp.event.on('notifications', function () {
        loadUrlInDiv("#sections", "/Notifications/GetNotificationsForEntity?entityName=scholarships&id=" + id);
    });
    abp.event.on('sections', function() {
        loadUrlInDiv("#sections", "/Scholarships/Sections?id=" + id);
    });
})();