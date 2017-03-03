namespace MirrorProject.Models.ViewModels
{
    public class RssFeedItem
    {   
        public string title { get; set; }
        public string publicationTime { get; set; }
        public string content { get; set; }

        public RssFeedItem(string title, string publicationTime, string content)
        {
            this.title = title;
            this.publicationTime = publicationTime;
            this.content = content;
        }
    }
}