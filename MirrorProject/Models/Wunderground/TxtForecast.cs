using System.Collections.Generic;

namespace MirrorProject.Models.Wunderground
{
    public class TxtForecast
    {
        public string date { get; set; }
        public List<Forecastday> forecastday { get; set; }
    }
}