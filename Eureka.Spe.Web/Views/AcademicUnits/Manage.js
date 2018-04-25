(function () {
    var id = $("#Id").val();
    startTabListener($("#sections"), id);
    loadUrlInDiv("#sections", "/Careers?id=" + id);
    abp.event.on("career", function() {
        loadUrlInDiv("#sections", "/Careers?id=" + id);
    });
})();