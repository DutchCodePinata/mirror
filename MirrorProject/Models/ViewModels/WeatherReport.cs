using System.Collections.Generic;

namespace MirrorProject.Models.ViewModels
{
    public class WeatherReport
    {
        public double Temp { get; set; }
        public string WindDir { get; set; }
        public double WindSpeed { get; set; }
        public string Icon { get; set; }
        public List<WeatherDayForecast> Daily { get; set; }
        public List<WeatherHourForecast> Hourly { get; set; }
    }
}