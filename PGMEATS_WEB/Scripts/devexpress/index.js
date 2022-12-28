//$(function () {
    
//});
var data = troublesData,
    totalCount = data.reduce(function (prevValue, item) {
        return prevValue + item.count;
    }, 0),
    cumulativeCount = 0,
    dataSource = data.map(function (item, index) {
        cumulativeCount += item.count;
        return {
            trouble: item.trouble,
            count: item.count,
            cumulativePercentage: Math.round(cumulativeCount * 100 / totalCount)
        };
    });

var chart = $("#chart").dxChart({
    size: {
        height: 350,
    },
    palette: "Harmony Light",
    dataSource: dataSource,
    title: {
        text: ["Count Frequency Trouble"],
        font: { size: 15 },
        subtitle: {
            text: "Period :",
            font: { size: 12 }
        }
    },
    argumentAxis: {
        label: {
            overlappingBehavior: "rotate", rotationAngle: 90, alignment: "left"
        }
    },
    tooltip: {
        enabled: true,
        shared: true,
        customizeTooltip: function (info) {
            return {
                html: "<div><div class='tooltip-header'>" +
                    info.argumentText + "</div>" +
                    "<div class='tooltip-body'><div class='series-name'>" +
                    info.points[0].seriesName +
                    ": </div><div class='value-text'>" +
                    info.points[0].valueText +
                    "</div><div class='series-name'>" +
                    info.points[1].seriesName +
                    ": </div><div class='value-text'>" +
                    info.points[1].valueText +
                    "% </div></div></div>"
            };
        }
    },
    valueAxis: [{
        title: {
            text: "Qty Trouble",
            font: { size: 12 },
        },
        name: "frequency",
        position: "left",
        tickInterval: 0,
        min: 1,
        tick: {
            visible: false
        }
    }, {
        name: "percentage",
        position: "right",
        showZero: true,
        label: {
            customizeText: function (info) {
                return info.valueText + "%";
            }
        },
        tickInterval: 20,
        valueMarginsEnabled: false
    }],
    commonSeriesSettings: {
        argumentField: "trouble",
        border: { color: "black" }
    },
    series: [{
        type: "bar",
        valueField: "count",
        axis: "frequency",
        name: "Frequency Trouble",
        //color: "#F493B8",
        color: "#fcbcfc",
        showInLegend: false,
        barWidth: 100,
        border: { color: "black", dashStyle: "solid", visible: true, width: 0.2 },
        label: {
            visible: true,
            position: 'top',
            backgroundColor: "transparant",
            font: { color: "black" }
        }
    }, {
        type: "spline",
        valueField: "cumulativePercentage",
        axis: "percentage",
        name: "Percentage",
        color: "#6b71c3",
        showInLegend: false,
        width: 1
    }],
    legend: {
        verticalAlignment: "top",
        horizontalAlignment: "center"
    },
    barGroupPadding: 0
});