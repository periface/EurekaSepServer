(function () {

    var service = abp.services.app.scholarship;

    var id = $("#Id").val();
    
    $('[data-toggle="tab"]').click(function () {
        console.log("click");
        var href = $(this).data("url");
        var div = $(this).attr("href");
        href = href + id;
        loadUrlInDiv(div,href);
    });

    function loadUrlInDiv(div, url) {
        $(div).empty();
        $(div).load(url, function(response,status,xhr) {
            //console.log(response);
            //console.log(status);
            if (status === "error") {
                $(div).append("<h2>Error</h2>");
            }
            //console.log(xhr);
        });
    }
    loadUrlInDiv("#sections", "/Scholarships/Sections/" + id);
})();