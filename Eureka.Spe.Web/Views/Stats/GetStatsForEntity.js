(function () {
    var service = abp.services.app.stats;
    var myBarChart;
    async function startStats(startDate, endDate, byDay, filter) {
        var request = {
            EntityType: $("#EntityName").val(),
            EntityId: $("#EntityId").val(),
            StartDate: startDate,
            EndDate: endDate,
            ByDay: byDay,
            filter: filter
        };
        abp.ui.setBusy($("#chart"));
        if (myBarChart) {
            myBarChart.destroy();
        }
        
        let getMetricsReponse = await service.getMetricsForElement(request);
        let getMetricsData = await getMetricsReponse;
        let getClicksReponse = await service.getClickForElement(request);
        let clicksData = await getClicksReponse;
        let notificationsResponse = await service.getNotificationsStatsForElement(request);
        let notificationsData = await notificationsResponse;
        

        abp.ui.clearBusy($("#chart"));
        buildChart(getMetricsData, clicksData, notificationsData);
    }
    google.charts.load('current', { 'packages': ['bar'] });
    google.charts.setOnLoadCallback(startStats);

    var chart;
    function buildChart(data) {
        if (data.length <= 0) {
            console.log(chart);
            if (chart) {
                chart.clearChart();
            }
        } else {
            var formatedInfo = [[]];
            for (var i = 0; i < data.length; i++) {
                var elm = data[i];

                if (!formatedInfo[i]) formatedInfo[i] = [];

                formatedInfo[i].push(elm.date);
                var getOrdered = getOrderedFunc(elm.elementsDtos);
                formatedInfo[i].push(getOrdered.positiveCntr);
                formatedInfo[i].push(getOrdered.negativeCntr);
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
            chart = new google.charts.Bar(document.getElementById('canvas'));
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
        startStats(null, null, byDay, action);
    });
})();