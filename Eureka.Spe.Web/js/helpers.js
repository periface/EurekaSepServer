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