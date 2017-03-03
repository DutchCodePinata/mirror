using System.Collections.Generic;

namespace MirrorProject.Models.Wunderground
{
    public class WundergroundData
    {
        public Response response { get; set; }
        public CurrentObservation current_observation { get; set; }
        public Forecast forecast { get; set; }
        public List<HourlyForecast> hourly_forecast { get; set; }
    }
}