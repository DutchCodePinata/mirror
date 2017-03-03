namespace MirrorProject.Models.Wunderground
{
    public class FCTTIME
    {
        public string hour { get; set; }
        public string hour_padded { get; set; }
        public string min { get; set; }
        public string min_unpadded { get; set; }
        public string sec { get; set; }
        public string year { get; set; }
        public string mon { get; set; }
        public string mon_padded { get; set; }
        public string mon_abbrev { get; set; }
        public string mday { get; set; }
        public string mday_padded { get; set; }
        public string yday { get; set; }
        public string isdst { get; set; }
        public string epoch { get; set; }
        public string pretty { get; set; }
        public string civil { get; set; }
        public string month_name { get; set; }
        public string month_name_abbrev { get; set; }
        public string weekday_name { get; set; }
        public string weekday_name_night { get; set; }
        public string weekday_name_abbrev { get; set; }
        public string weekday_name_unlang { get; set; }
        public string weekday_name_night_unlang { get; set; }
        public string ampm { get; set; }
        public string tz { get; set; }
        public string age { get; set; }
        public string UTCDATE { get; set; }
    }
}