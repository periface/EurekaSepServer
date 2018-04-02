(function () {

    var service = abp.services.app.scholarship;

    var id = $("#Id").val();
    
    
    loadUrlInDiv("#sections", "/Scholarships/Sections/" + id);
})();