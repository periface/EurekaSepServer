(function () {

    $(document).ready(function() {
        var id = $("#Id").val();
        startTabListener($("#sections"), id);
        loadUrlInDiv("#sections", "/Courses/Themes?courseId=" + id);


        abp.event.on('notifications', function () {
            loadUrlInDiv("#sections", "/Notifications/GetNotificationsForEntity?entityName=courses&id=" + id);
        });
    });
    
})();