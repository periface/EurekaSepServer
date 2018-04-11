(function () {

    var form = $("#AddEditCategoryForm");
    form.on("submit", function (e) {
        e.preventDefault();
    });
    $.AdminBSB.input.activate(form);
})();