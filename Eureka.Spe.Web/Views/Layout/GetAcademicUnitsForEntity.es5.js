///Requires jquery
"use strict";

(function () {
    var service = abp.services.app.academicUnit;

    $("input[name='academicunit']").change(function () {
        var values = [];
        var checkboxes = $("input[name='academicunit']:checked");
        if (checkboxes) {
            for (var roleIndex = 0; roleIndex < checkboxes.length; roleIndex++) {
                var checkbox = $(checkboxes[roleIndex]);
                var value = checkbox.attr('data-id');
                values.push(value);
            }
        }
        var result = {
            ids: values,
            id: $("#Id").val(),
            entityName: $("#EntityName").val()
        };
        console.log(result);
        service.addAcademicUnitToEntity(result).done(function () {});
    });
})();

