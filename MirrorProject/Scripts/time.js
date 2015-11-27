var time = {
    timeFormat: 24,
    dateLocation: '.date',
    timeLocation: '.time',
    updateInterval: 1000,
    intervalId: null
};

var weekday = new Array("Zondag","Maandag","Dinsdag","Woensdag","Donderdag","Vrijdag","Zaterdag");
var month = new Array("Januari", "Februari", "Maart","April","Mei","Juni","Juli","Augustus","September","Oktober","November","December");

/**
 * Updates the time that is shown on the screen
 */
time.updateTime = function () {

    var _now = new Date()

    $(this.dateLocation).html(weekday[_now.getDay()] + " " + _now.getDate() + " " + month[_now.getMonth()]);
    $(this.timeLocation).html(new Date().toTimeString().replace(/.*(\d{2}:\d{2})(:\d{2}).*/, "$1"));

}

time.init = function () {
    if (parseInt(time.timeFormat) === 12) {
        time._timeFormat = 'hh'
    } else {
        time._timeFormat = 'HH';
    }
    this.intervalId = setInterval(function () {
        this.updateTime();
    }.bind(this), 1000);

}

time.init();