using System;
using System.Linq;
using System.Collections.Generic;
using System.Web;
using Microsoft.Ajax.Utilities;
using QDFeedParser;

namespace MirrorProject.Models
{
    public class RssSingleton
    {
        private static RssSingleton instance = new RssSingleton();

        private ServiceScheduler scheduler = new ServiceScheduler();

        private List<RssItem> rssItems = new List<RssItem>();

        public static RssSingleton GetInstance()
        {
            return instance;
        }

        public List<RssItem> GetRssItems()
        {
            return rssItems;
        }

        public void SetRssItems(List<BaseFeedItem> items)
        {
            items.Clear();
            items.ForEach(x => rssItems.Add(new RssItem(x.Title, x.DatePublished.ToString("HH:mm"))));
        }

        public class RssItem
        {
            public RssItem(string title, string publicationTime)
            {
                this.title = title;
                this.publicationTime = publicationTime;
            }

            public string title { get; set; }
            public string publicationTime { get; set; }
        }
    }
}