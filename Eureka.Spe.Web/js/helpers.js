// ReSharper disable once NativeTypePrototypeExtending
Date.prototype.yyyymmdd = function () {
    var yyyy = this.getFullYear();
    var mm = this.getMonth() < 9 ? "0" + (this.getMonth() + 1) : (this.getMonth() + 1); // getMonth() is zero-based
    var dd = this.getDate() < 10 ? "0" + this.getDate() : this.getDate();
    return "".concat(yyyy).concat(mm).concat(dd);
};
// ReSharper disable once NativeTypePrototypeExtending
Date.prototype.yyyymmddhhmm = function () {
    var yyyy = this.getFullYear();
    var mm = this.getMonth() < 9 ? "0" + (this.getMonth() + 1) : (this.getMonth() + 1); // getMonth() is zero-based
    var dd = this.getDate() < 10 ? "0" + this.getDate() : this.getDate();
    var hh = this.getHours() < 10 ? "0" + this.getHours() : this.getHours();
    var min = this.getMinutes() < 10 ? "0" + this.getMinutes() : this.getMinutes();
    return "".concat(yyyy + "-").concat(mm + "-").concat(dd + " ").concat(hh + ":").concat(min + " hrs");
};
// ReSharper disable once NativeTypePrototypeExtending
Date.prototype.yyyymmddhhmmss = function () {
    var yyyy = this.getFullYear();
    var mm = this.getMonth() < 9 ? "0" + (this.getMonth() + 1) : (this.getMonth() + 1); // getMonth() is zero-based
    var dd = this.getDate() < 10 ? "0" + this.getDate() : this.getDate();
    var hh = this.getHours() < 10 ? "0" + this.getHours() : this.getHours();
    var min = this.getMinutes() < 10 ? "0" + this.getMinutes() : this.getMinutes();
    var ss = this.getSeconds() < 10 ? "0" + this.getSeconds() : this.getSeconds();
    return "".concat(yyyy).concat(mm).concat(dd).concat(hh).concat(min).concat(ss);
};





function hexToRgb(hexCode) {
    var patt = /^#([\da-fA-F]{2})([\da-fA-F]{2})([\da-fA-F]{2})$/;
    var matches = patt.exec(hexCode);
    var rgb =
        "rgb(" +
        parseInt(matches[1], 16) +
        "," +
        parseInt(matches[2], 16) +
        "," +
        parseInt(matches[3], 16) +
        ")";
    return rgb;
}

function hexToRgba(hexCode, opacity) {
    var patt = /^#([\da-fA-F]{2})([\da-fA-F]{2})([\da-fA-F]{2})$/;
    var matches = patt.exec(hexCode);
    var rgb =
        "rgba(" +
        parseInt(matches[1], 16) +
        "," +
        parseInt(matches[2], 16) +
        "," +
        parseInt(matches[3], 16) +
        "," +
        opacity +
        ")";
    return rgb;
}
function reloadPage(id) {
    var urlSplit = window.location.href.split("/");

    var last = urlSplit[urlSplit.length - 1];

    var isNotNumber = isNaN(last);

    if (isNotNumber) {
        window.location.href = window.location.href + "/" + id;
    }
}
function enableUploadOnFileElm(elm, options) {

    if (!options) options = {
        baseUrl: "/upload",
        sizes: [],
        optimize: true,
        uniqueFolder: "icons",
        onUploading: function () { },
        onUploadDone: function () { },
        onUploadError: function() {
            
        },
        onUploadStart: function () { }
    };

    var baseUrl = options.baseUrl;
    baseUrl = baseUrl + "?uniqueFolder=" + options.uniqueFolder;
    baseUrl = baseUrl + "&optimize=" + options.optimize;
    var sizesUrl = "";
    for (var i = 0; i < options.sizes.length; i++) {
        var size = options.sizes[i];

        sizesUrl += "&sizes=" + size;
    }
    baseUrl = baseUrl + sizesUrl;
    $(elm).on("change", function () {
        options.onUploadStart();
        var fileData = $(elm).prop("files")[0];
        var formData = new FormData();
        formData.append("file", fileData);
        $.ajax({
            url: baseUrl, // point to server-side script
            cache: false,
            contentType: false,
            processData: false,
            data: formData,
            type: "post",
            success: options.onUploadDone,
            error: options.onUploadError,
            xhr: function () {
                var jqXhr = null;
                if (window.ActiveXObject) {
                    jqXhr = new window.ActiveXObject("Microsoft.XMLHTTP");
                }
                else {
                    jqXhr = new window.XMLHttpRequest();
                }
                //Upload progress
                jqXhr.upload.addEventListener("progress", function (evt) {
                    if (evt.lengthComputable) {
                        var percentComplete = Math.round((evt.loaded * 100) / evt.total);
                        options.onUploading(percentComplete);
                    }
                }, false);
                return jqXhr;
            }
        });
    });
}
function startTabListener(container,id) {
    $('[data-toggle="tab"]').click(function () {
        console.log(id);
        var href = $(this).data("url");
        href = href + id;
        loadUrlInDiv(container, href);
    });    
}
function overrideNotify() {
    abp.notify.success = function (message) {
        var toast = $.mdtoast(message, { duration: 3000, init: true });
        // Displays the toast
        toast.show();
    }
}
function loadUrlInDiv(div, url) {
    abp.ui.setBusy($(div));
    $(div).load(url, function (response, status, xhr) {
        abp.ui.clearBusy($(div));
        if (status === "error") {
            $(div).append("<h2>Error</h2>");
        }
    });
}
var mapStyles = {
    gray: [
        { featureType: "all", elementType: "all", stylers: [{ visibility: "on" }] },
        {
            featureType: "all",
            elementType: "labels",
            stylers: [{ visibility: "off" }, { saturation: "-100" }]
        },
        {
            featureType: "all",
            elementType: "labels.text.fill",
            stylers: [
                { saturation: 36 },
                { color: "#F0F0F0" },
                { lightness: 40 },
                { visibility: "on" }
            ]
        },
        {
            featureType: "all",
            elementType: "labels.text.stroke",
            stylers: [{ visibility: "off" }, { color: "#000000" }, { lightness: 16 }]
        },
        {
            featureType: "all",
            elementType: "labels.icon",
            stylers: [{ visibility: "off" }]
        },
        {
            featureType: "administrative",
            elementType: "geometry.fill",
            stylers: [{ color: "#000000" }, { lightness: 20 }]
        },
        {
            featureType: "administrative",
            elementType: "geometry.stroke",
            stylers: [{ color: "#000000" }, { lightness: 17 }, { weight: 1.2 }]
        },
        {
            featureType: "landscape",
            elementType: "geometry",
            stylers: [{ color: "#000000" }, { lightness: 20 }]
        },
        {
            featureType: "landscape",
            elementType: "geometry.fill",
            stylers: [{ color: "#4d6059" }]
        },
        {
            featureType: "landscape",
            elementType: "geometry.stroke",
            stylers: [{ color: "#4d6059" }]
        },
        {
            featureType: "landscape.natural",
            elementType: "geometry.fill",
            stylers: [{ color: "#4d6059" }]
        },
        {
            featureType: "poi",
            elementType: "geometry",
            stylers: [{ lightness: 21 }]
        },
        {
            featureType: "poi",
            elementType: "geometry.fill",
            stylers: [{ color: "#4d6059" }]
        },
        {
            featureType: "poi",
            elementType: "geometry.stroke",
            stylers: [{ color: "#4d6059" }]
        },
        {
            featureType: "road",
            elementType: "geometry",
            stylers: [{ visibility: "on" }, { color: "#7f8d89" }]
        },
        {
            featureType: "road",
            elementType: "geometry.fill",
            stylers: [{ color: "#7f8d89" }]
        },
        {
            featureType: "road.highway",
            elementType: "geometry.fill",
            stylers: [{ color: "#7f8d89" }, { lightness: 17 }]
        },
        {
            featureType: "road.highway",
            elementType: "geometry.stroke",
            stylers: [{ color: "#7f8d89" }, { lightness: 29 }, { weight: 0.2 }]
        },
        {
            featureType: "road.arterial",
            elementType: "geometry",
            stylers: [{ color: "#000000" }, { lightness: 18 }]
        },
        {
            featureType: "road.arterial",
            elementType: "geometry.fill",
            stylers: [{ color: "#7f8d89" }]
        },
        {
            featureType: "road.arterial",
            elementType: "geometry.stroke",
            stylers: [{ color: "#7f8d89" }]
        },
        {
            featureType: "road.local",
            elementType: "geometry",
            stylers: [{ color: "#000000" }, { lightness: 16 }]
        },
        {
            featureType: "road.local",
            elementType: "geometry.fill",
            stylers: [{ color: "#7f8d89" }]
        },
        {
            featureType: "road.local",
            elementType: "geometry.stroke",
            stylers: [{ color: "#7f8d89" }]
        },
        {
            featureType: "transit",
            elementType: "geometry",
            stylers: [{ color: "#000000" }, { lightness: 19 }]
        },
        {
            featureType: "water",
            elementType: "all",
            stylers: [{ color: "#2b3638" }, { visibility: "on" }]
        },
        {
            featureType: "water",
            elementType: "geometry",
            stylers: [{ color: "#2b3638" }, { lightness: 17 }]
        },
        {
            featureType: "water",
            elementType: "geometry.fill",
            stylers: [{ color: "#24282b" }]
        },
        {
            featureType: "water",
            elementType: "geometry.stroke",
            stylers: [{ color: "#24282b" }]
        },
        {
            featureType: "water",
            elementType: "labels",
            stylers: [{ visibility: "on" }]
        },
        {
            featureType: "water",
            elementType: "labels.text",
            stylers: [{ visibility: "off" }]
        },
        {
            featureType: "water",
            elementType: "labels.text.fill",
            stylers: [{ visibility: "off" }]
        },
        {
            featureType: "water",
            elementType: "labels.text.stroke",
            stylers: [{ visibility: "off" }]
        },
        {
            featureType: "water",
            elementType: "labels.icon",
            stylers: [{ visibility: "off" }]
        }
    ]
};


function startTinyMce(selector, callback) {
    if (!callback) callback = function () { };
    window.tinymce.init({
        selector: selector, setup: function (editor) {
            callback(editor);
        },
        height: "480",
        theme: 'modern',
        plugins: 'print preview fullpage searchreplace autolink directionality visualblocks visualchars fullscreen image link media template codesample table charmap hr pagebreak nonbreaking anchor toc insertdatetime advlist lists textcolor wordcount imagetools contextmenu colorpicker textpattern help',
        toolbar1: 'formatselect | bold italic strikethrough forecolor backcolor | link | alignleft aligncenter alignright alignjustify  | numlist bullist outdent indent  | removeformat',
        image_advtab: true,
        templates: [
            { title: 'Test template 1', content: 'Test 1' },
            { title: 'Test template 2', content: 'Test 2' }
        ],
        content_css: [
            '//fonts.googleapis.com/css?family=Lato:300,300i,400,400i',
            '//www.tinymce.com/css/codepen.min.css'
        ],
        file_picker_types: 'image',
        // and here's our custom image picker
        file_picker_callback: function (cb) {
            var input = document.createElement('input');
            input.setAttribute('type', 'file');
            input.setAttribute('accept', 'image/*');

            // Note: In modern browsers input[type="file"] is functional without 
            // even adding it to the DOM, but that might not be the case in some older
            // or quirky browsers like IE, so you might want to add it to the DOM
            // just in case, and visually hide it. And do not forget do remove it
            // once you do not need it anymore.

            input.onchange = function () {
                var file = this.files[0];

                var reader = new FileReader();
                reader.onload = function () {
                    // Note: Now we need to register the blob in TinyMCEs image blob
                    // registry. In the next release this part hopefully won't be
                    // necessary, as we are looking to handle it internally.
                    var id = 'blobid' + (new Date()).getTime();
                    var blobCache = tinymce.activeEditor.editorUpload.blobCache;
                    var base64 = reader.result.split(',')[1];
                    var blobInfo = blobCache.create(id, file, base64);
                    blobCache.add(blobInfo);

                    // call the callback and populate the Title field with the file name
                    cb(blobInfo.blobUri(), { title: file.name });
                };
                reader.readAsDataURL(file);
            };

            input.click();
        }
    });
}
