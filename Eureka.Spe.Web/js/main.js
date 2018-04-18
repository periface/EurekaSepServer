(function ($) {

    //Notification handler
    abp.event.on('abp.notifications.received', function (userNotification) {
        abp.notifications.showUiNotifyForUserNotification(userNotification);

        //Desktop notification
        Push.create("AbpZeroTemplate", {
            body: userNotification.notification.data.message,
            icon: abp.appPath + 'images/app-logo-small.png',
            timeout: 6000,
            onClick: function () {
                window.focus();
                this.close();
            }
        });
    });

    //serializeFormToObject plugin for jQuery
    $.fn.serializeFormToObject = function () {
        //serialize to array
        var data = $(this).serializeArray();

        //add also disabled items
        $(':disabled[name]', this).each(function () {
            data.push({ name: this.name, value: $(this).val() });
        });

        //map to object
        var obj = {};
        data.map(function (x) { obj[x.name] = x.value; });

        return obj;
    };

    //Configure blockUI
    if ($.blockUI) {
        $.blockUI.defaults.baseZ = 2000;
    }

    function startDismissListener() {
        var $dismissBtn = $(".js-dismiss");
        $dismissBtn.click(function() {
            var elm = $(this).data("div");

            dismissAndRemember("#"+elm);
        });
    }

    function dismissAndRemember(div) {
        var $div = $(div);
        $div.css("display", "none");
        var dismissed = localStorage.getItem("dismissed");
        if (!dismissed) {
            dismissed = [];
        } else {

            dismissed = JSON.parse(dismissed);
        }
        dismissed.push(div);
        localStorage.setItem("dismissed", JSON.stringify(dismissed));
    }
    function loadDismissed() {
        var dismissed = localStorage.getItem("dismissed");
        if (!dismissed) {
            dismissed = [];
        } else {

            dismissed = JSON.parse(dismissed);
        }

        for (var i = 0; i < dismissed.length; i++) {
            var div = $(dismissed[i]);
            var $div = $(div);
            $div.css("display", "none");
        }
    }

    startDismissListener();
    loadDismissed();
})(jQuery);