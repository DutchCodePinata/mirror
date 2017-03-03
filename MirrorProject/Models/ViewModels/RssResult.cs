using System.Collections.Generic;
using Microsoft.Ajax.Utilities;
using TNX.RssReader;
using MirrorProject.Models.ViewModels;

namespace MirrorProject.Models
{
    public class RssResult
    {
        private List<RssFeedItem> rssItems = new List<RssFeedItem>();

        public List<RssFeedItem> getRssItems()
        {
            return rssItems;
        }

        public void setRssItems(IEnumerable<RssItem> items)
        {
            items.ForEach(x => rssItems.Add(new RssFeedItem(x.Title, x.PublicationUtcTime.ToLocalTime().ToString("HH:mm"), x.Description)));
        }    
    }
}