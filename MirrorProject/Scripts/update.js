// Run first time
makeAjaxCalls();

// Schedule updates
setInterval(function () {
    makeAjaxCalls();
}, 60000 * 10);

// Make one call for all services
function makeAjaxCalls() {
    $.ajax({
        url: 'Home/WeatherUpdate',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: weatherUpdate,
        error: errorFunc
    });

    $.ajax({
        url: 'Home/RssUpdate',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: newsUpdate,
        error: errorFunc
    });
}

// Update weather html
function weatherUpdate(data, status) {
    var weatherReport = JSON.parse(data);
    if (weatherReport.Daily != null) {
        $(".temp-now").html(weatherReport.Temp + "&deg;");
        $(".winddir-now").html(weatherReport.WindDir);
        $("#temp-now-icon").attr("class", "wi " + weatherReport.Icon + " xdimmed");
        $(".windspeed-now").html(weatherReport.WindSpeed + "km/h");

        $(".today-temp-high").html(weatherReport.Daily[0].TempHigh + "&deg;");
        $(".today-temp-low").html(weatherReport.Daily[0].TempLow + "&deg;");
        $(".today-rain-chance").html(weatherReport.Daily[0].RainChance + "&#37;");

        $(".tommorow-temp-high").html(weatherReport.Daily[1].TempHigh + "&deg;");
        $(".tommorow-temp-low").html(weatherReport.Daily[1].TempLow + "&deg;");
        $(".tommorow-rain-chance").html(weatherReport.Daily[1].RainChance + "&#37;");

        $(".dayaftertommorow-temp-high").html(weatherReport.Daily[2].TempHigh + "&deg;");
        $(".dayaftertommorow-temp-low").html(weatherReport.Daily[2].TempLow + "&deg;");
        $(".dayaftertommorow-rain-chance").html(weatherReport.Daily[2].RainChance + "&#37;");


        drawChart(weatherReport.Hourly);


    }
}

// Update news html
function newsUpdate(data, status) {
    var rssItems = JSON.parse(data);
    if (rssItems != null) {
        var childs = $(".rss-feed").children();
        for (var i = 0; i < childs.length; i++) {
            var rssItem = rssItems[i];
            var child = childs[i];

            $(child).find(".rss-publication-time").html(rssItem.publicationTime);
            $(child).find(".rss-title").html(rssItem.title);
            $(child).find(".rss-description").html(rssItem.content);
        }
    }
}

function errorFunc(data) {

}


$(document).ready(function () {

    $("#accordion").accordion({ header: ".slide", heightStyle: "content" });
    setInterval((function () {
        var count = 0;
        var list = $('.slides .slide');

        return function () {
            list.eq(++count % list.length).click();
        };
    })(), 10000);
});


function drawChart(dataArray) {
    var hourly = [["Hour","mm"]];


    for (var i = 0; i < 12 ; i++) {
        var item = [dataArray[i].Hour.toString(),dataArray[i].RainFall];
        hourly.push(item);
    }


    var data = google.visualization.arrayToDataTable(hourly);

    var options = {
        chartArea : { left: 20 },
        isStacked: true,
        colors: ['#FFF'],
        backgroundColor: 'black',
        legend: { position: 'none' },
        tooltip: { isHtml: 'true' },
        hAxes: [
            {
                textStyle: {
                    color: 'white',
                    fontSize: '20px'
                }
            }
        ],
        vAxes: [
            {
                minValue: 0,
                textStyle: {
                    color: 'white',
                    fontSize: '20px'
                }
            }
        ],
        vAxis: {
                minValue: 0,
                viewWindow: {
                    min: 0
                }
            }
        }
        

    var chart = new google.visualization.SteppedAreaChart(document.getElementById('chart_div'));

    chart.draw(data, options);
}