(function () {
    var service = abp.services.app.stats;
    var myBarChart;
    function startStats(startDate, endDate, byDay) {
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
            ByDay: byDay
        }));
        asyncCalls.push(service.getClickForElement({
            EntityType: $("#EntityName").val(),
            EntityId: $("#EntityId").val(),
            StartDate: startDate,
            EndDate: endDate,
            ByDay: byDay
        }));

        asyncCalls.push(service.getNotificationsStatsForElement({
            EntityType: $("#EntityName").val(),
            EntityId: $("#EntityId").val(),
            StartDate: startDate,
            EndDate: endDate,
            ByDay: byDay
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
            console.log(notificationsData);

            if (notificationsData.length) {
                $("#container-non").css("display", "none");
                buildChart(data, clicksData, notificationsData);
            } else {
                if (!byDay) {

                    $("#container").css("display", "none");
                    $("#container-non").css("display", "block");
                }
            }

        });
    }
    function getDataSetsForClicks(data) {
        var result = {
            type: 'line', fill: false, borderColor: "#86C7F3", borderWidth: 2, backgroundColor: "#86C7F3",
            data: [],
            label: "Clicks"

        };
        for (var i = 0; i < data.length; i++) {
            result.data.push(data[i].clickDtos.length);
        }
        return result;
    }
    function getDataSetsForNotifications(data) {
        var result = [{
                type: 'line', fill: false, borderColor: "red", borderWidth: 2, backgroundColor: "red",
                data: [],
                label: "Notificaciones (No vistas)"
            },
            {
                type: 'line', fill: false, borderColor: "green", borderWidth: 2, backgroundColor: "green",
                data: [],
                label: "Notificaciones (Vistas)"
            }];
        for (var i = 0; i < data.length; i++) {
            result[0].data.push(data[i].unseenCount);
            result[1].data.push(data[i].seenCount);
        }
        return result;
    }
    function buildChart(data, clickData, notificationsData) {
        var dataLabels = getDataLabels(data);
        var dataSets = getDataSets(data);
        var dataSetsForClicks = getDataSetsForClicks(clickData);
        var dataSetsForNotifications = getDataSetsForNotifications(notificationsData);
        dataSets.push(dataSetsForClicks);
        dataSets.push(dataSetsForNotifications[0]);
        dataSets.push(dataSetsForNotifications[1]);
        var chartData = {
            labels: dataLabels,
            datasets: dataSets
        }
        myBarChart = new Chart(document.getElementById('canvas'), {
            type: 'bar',
            data: chartData,
            options: {
                responsive: true,
                legend: {
                    position: 'top',
                },
                title: {
                    display: true,
                    text: '¿La información fue de ayuda? / Clicks / Notificaciones'
                },
                scales: {
                    yAxes: [{ ticks: { beginAtZero: true } }]
                },
                tooltips: {
                    mode: 'index',
                    intersect: true
                }
            }
        });
    }
    function getDataLabels(data) {
        var labels = [];
        for (var i = 0; i < data.length; i++) {
            var elm = data[i];
            labels.push(elm.date);
        }
        return labels;
    }
    function getDataSets(data) {

        var sets = [
            {
                label: "Si",
                borderWidth: 1,
                data: [],
                backgroundColor: "rgba(255, 99, 132, 0.2)",
            },
            {
                label: "No",
                borderWidth: 1,
                data: []
            }];

        for (var i = 0; i < data.length; i++) {
            var elms = data[i].elementsDtos;
            var negatives = elms.filter(a => a.note === 0);
            var positives = elms.filter(a => a.note > 0);

            sets[0].data.push(positives.length);
            sets[1].data.push(negatives.length);
        }


        return sets;
    }

    startStats(null, null);
    var byDay = false;
    $(".js-filter").click(function () {
        byDay = $("#ByDay").is(":checked");
        startStats($("#StartDate").val(), $("#EndDate").val(), byDay);
    });
    $.AdminBSB.input.activate($("#form"));
})();