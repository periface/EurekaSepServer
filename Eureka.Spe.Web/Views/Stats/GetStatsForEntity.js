(function () {
    var service = abp.services.app.stats;
    var myBarChart;
    function startStats(startDate, endDate, byDay, filter) {
        abp.ui.setBusy($("#chart"));
        if (myBarChart) {
            myBarChart.destroy();
        }
        var asyncCalls = [];
        asyncCalls.push(service.getMetricsForElement({
            EntityType: $("#EntityName").val(),
            EntityId: $("#EntityId").val(),
            StartDate: startDate,
            EndDate: endDate,
            ByDay: byDay,
            filter: filter
        }));
        asyncCalls.push(service.getClickForElement({
            EntityType: $("#EntityName").val(),
            EntityId: $("#EntityId").val(),
            StartDate: startDate,
            EndDate: endDate,
            ByDay: byDay,
            filter: filter
        }));

        asyncCalls.push(service.getNotificationsStatsForElement({
            EntityType: $("#EntityName").val(),
            EntityId: $("#EntityId").val(),
            StartDate: startDate,
            EndDate: endDate,
            ByDay: byDay,
            filter: filter
        }));

        $.when.apply(this, asyncCalls).done(function () {
            abp.ui.clearBusy($("#chart"));
            var data = {};
            var clicksData = {};
            var notificationsData = {};
            for (var i = 0; i < arguments.length; i++) {
                var dataobject = arguments[i][0];
                if (i === 0) {
                    data = dataobject;
                }
                if (i === 1) {
                    clicksData = dataobject;
                }
                if (i === 2) {
                    notificationsData = dataobject;
                }
            }
            buildChart(data, clicksData, notificationsData);

        });
    }
    google.charts.load('current', { 'packages': ['bar'] });
    google.charts.setOnLoadCallback(startStats);
    function buildChart(data) {
        console.log("The data", data);
        if (data.length > 0) {
            var formatedInfo = [[]];

            for (var i = 0; i < data.length; i++) {
                var elm = data[i];

                if (!formatedInfo[i]) formatedInfo[i] = [];

                formatedInfo[i].push(elm.date);
                var getOrdered = getOrderedFunc(elm.elementsDtos);
                formatedInfo[i].push(getOrdered.positiveCntr);
                formatedInfo[i].push(getOrdered.negativeCntr);
                console.log(formatedInfo);
            }
            var leData = google.visualization.arrayToDataTable([
                ['Mes', 'Me resulto útil', "No me resulto útil"],
                ...formatedInfo
            ]);
            var options = {
                chart: {
                    title: 'Estadísticas'
                }
            };
            var chart = new google.charts.Bar(document.getElementById('canvas'));
            chart.draw(leData, google.charts.Bar.convertOptions(options));
        }

    }
    function getOrderedFunc(elementsDtos) {
        var positiveCntr = 0;
        var negativeCntr = 0;
        for (var i = 0; i < elementsDtos.length; i++) {
            if (elementsDtos[i].note <= 0) {
                negativeCntr++;
            } else {
                positiveCntr++;
            }
        }
        return {
            positiveCntr,
            negativeCntr
        }
    }
    //startStats(null, null);
    var byDay = false;
    $(".js-filter").click(function () {
        byDay = $("#ByDay").is(":checked");
        startStats($("#StartDate").val(), $("#EndDate").val(), byDay);
    });
    $.AdminBSB.input.activate($("#form"));

    $(".js-buttons").click(function () {
        var selected = $(this);
        $(".js-buttons").each(function (i, elm) {
            $(elm).removeClass("btn-primary");
            $(elm).addClass("btn-default");
        });
        selected.addClass("btn-primary");
        selected.removeClass("btn-default");
        var action = $(this).data("action");
        console.log(action);

        startStats(null, null, byDay, action);

    });




})();