(function () {
    $('#summernote').trumbowyg({
        lang: 'es',
        btns: [
            ['viewHTML'],
            ['formatting'],
            ['strong', 'em', 'del'],
            ['superscript', 'subscript'],
            ['justifyLeft', 'justifyCenter', 'justifyRight', 'justifyFull'],
            ['unorderedList', 'orderedList'],
            ['horizontalRule'],
            ['removeformat'],
            ['fullscreen']
        ]
    });
    var form = $("#AddEditScholarshipSectionForm");
    $.AdminBSB.input.activate(form);
        
})();