namespace MirrorProject.Models.Wunderground
{
    public class HourlyForecast
    {
        public FCTTIME FCTTIME { get; set; }
        public Temp temp { get; set; }
        public Dewpoint dewpoint { get; set; }
        public string condition { get; set; }
        public string icon { get; set; }
        public string icon_url { get; set; }
        public string fctcode { get; set; }
        public string sky { get; set; }
        public Wspd wspd { get; set; }
        public Wdir wdir { get; set; }
        public string wx { get; set; }
        public string uvi { get; set; }
        public string humidity { get; set; }
        public Windchill windchill { get; set; }
        public Heatindex heatindex { get; set; }
        public Feelslike feelslike { get; set; }
        public Qpf qpf { get; set; }
        public Snow snow { get; set; }
        public string pop { get; set; }
        public Mslp mslp { get; set; }
    }
}