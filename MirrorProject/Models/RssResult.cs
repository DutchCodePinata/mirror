using System;
using System.Linq;
using System.Collections.Generic;
using System.Web;
using Microsoft.Ajax.Utilities;
using TNX.RssReader;

namespace MirrorProject.Models
{
    public class RssResult
    {
        private List<RssFeedItem> rssItems = new List<RssFeedItem>();

        public List<RssFeedItem> GetRssItems()
        {
            return rssItems;
        }

        public void SetRssItems(IEnumerable<RssItem> items)
        {
            items.ForEach(x => rssItems.Add(new RssFeedItem(x.Title, x.PublicationUtcTime.ToLocalTime().ToString("HH:mm"), x.Description)));
        }

        public class RssFeedItem
        {
            public RssFeedItem(string title, string publicationTime, string content)
            {
                this.title = title;
                this.publicationTime = publicationTime;
                this.content = content;
            }

            public string title { get; set; }
            public string publicationTime { get; set; }
            public string content { get; set; }
        }
    }
}