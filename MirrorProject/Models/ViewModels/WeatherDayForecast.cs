namespace MirrorProject.Models.ViewModels
{
    public class WeatherDayForecast
    {
        public Date Date { get; set; }
        public string TempHigh { get; set; }
        public string TempLow { get; set; }
        public int RainChance { get; set; }
    }
}