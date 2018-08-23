
var chart_plot_03_data = [

];

$(document).ready(function () {
    initChart();
});

/**
 * Settings for dashboard chart
 */
var chart_plot_03_settings = {
    series: {
        curvedLines: {
            apply: true,
            active: true,
            monotonicFit: true
        }
    },
    colors: ["#26B99A"],
    grid: {
        borderWidth: {
            top: 0,
            right: 0,
            bottom: 1,
            left: 1
        },
        borderColor: {
            bottom: "#7F8790",
            left: "#7F8790"
        }
    },
    tooltip: true,
    tooltipOpts: {
        content: "%s: %y.0",
        xDateFormat: "%d/%m",
        shifts: {
            x: -30,
            y: -50
        },
        defaultTheme: false
    },
    yaxis: {
        min: 0
    },
    xaxis: {
        mode: "time",
        minTickSize: [1, "day"],
        timeformat: "%d/%m"
    }
};



function initChart() {
    for (var i = 0; i < 30; i++) {
        chart_plot_03_data.push([new Date(Date.today().add(i).days()).getTime(), randNum() + i + i + 10]);
    }

    if ($("#chart_plot_03").length) {
        console.log('Initial invoice chart');

        $.plot($("#chart_plot_03"), [{
            label: "Hóa đơn phát hành",
            data: chart_plot_03_data,
            lines: {
                fillColor: "rgba(150, 202, 89, 0.12)"
            },
            points: {
                fillColor: "#fff"
            }
        }], chart_plot_03_settings);

    };
}
