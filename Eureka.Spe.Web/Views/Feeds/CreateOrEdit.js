
(function () {
    var service = abp.services.app.feed;
    var publisherService = abp.services.app.publisher;
    var tinyMce;
    startTinyMce("#content", function (instance) {
        tinyMce = instance;
    });
    var uploadOptions = {
        baseUrl: "/upload",
        sizes: ["128x128"],
        optimize: true,
        uniqueFolder: "feeds",
        onUploadStart: function () {
            abp.ui.setBusy($("#upload"));
        },
        onUploading: function (xhr) {
            if (xhr === 100) {
                //abp.notify.success("Archivo enviado...");
                //abp.notify.info("Procesando archivo...");
            }
        },
        onUploadDone: function (data) {
            $("#img").val(data.FileUrl);
            $("#imgPlaceHolder").attr("src", data.FileUrl);
            abp.ui.clearBusy($("#upload"));
            //abp.notify.info("Archivo procesado...");
            checkImg();
        },
        onUploadError: function (err) {
            abp.ui.clearBusy($("#upload"));

            abp.notify.error("Error procesando la imagen...");


        }
    };
    enableUploadOnFileElm("#icon", uploadOptions);

    $("#CreateEditFeedForm").on("submit",
        function (e) {
            checkImg();
            e.preventDefault();
            $('button[type="submit"]').prop("disabled", true);
            var data = $(this).serializeFormToObject();
            data.content = tinyMce.save();
            service.createOrUpdate(data).done(function (response) {
                abp.notify.info("Cambios guardados...");
                $('button[type="submit"]').prop("disabled", false);
                reloadPage(response);
            });
        });

    function checkImg() {
        var img = document.getElementById('imgPlaceHolder');
        //or however you get a handle to the IMG
        var width = img.clientWidth;
        var height = img.clientHeight;

        if (width != 450 && height != 200) {
            alert("Buuu");
        }
    }
    $("#imgPlaceHolder").click(function () {
        $("#icon").click();
    });

    $(".js-add-publisher").click(function () {
        
        window.eModal.ajax({
                loadingHtml: '<span class="fa fa-circle-o-notch fa-spin fa-3x text-primary"></span><span class="h4">Cargando</span>',
                url: '/Publishers/CreateFast',
                title: 'Nuevo departamento',
                buttons: [
                    {
                        text: 'Cerrar', style: 'danger', close: true, click: function () {

                        }
                    },
                    {
                        text: 'Guardar', style: 'info', close: false, click: function (elm) {
                            var data = $("#AddEditPublisherForm").serializeFormToObject();
                            publisherService.createOrUpdate(data).done(function (data) {
                                window.eModal.close();
                                abp.notify.success("Elemento guardado con exito...");
                                reloadPublishers(data);
                            });

                        }
                    }
                ]
            })
            .then(function () {

            });
    });
    function reloadPublishers(id) {
        $("#publisher").load("/layout/PublishersSelector?selected="+id, function() {
            $.AdminBSB.select.activate();
        });
    }
})();