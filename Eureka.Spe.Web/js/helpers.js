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

function enableUploadOnFileElm(elm, done, onUploading, options) {

    if (!options) options = {
        baseUrl: "/admin/upload",
        sizes: [],
        optimize: true,
        uniqueFolder: "icons"
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
    if (!onUploading) onUploading = function () { };
    $(elm).on("change", function () {
        onUploading();
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
            success: function (response) {
                done(response);
            }
        });
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
