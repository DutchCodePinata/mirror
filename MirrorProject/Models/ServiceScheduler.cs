using System;
using System.Timers;
using System.Web;

namespace MirrorProject.Models
{
    public class ServiceScheduler
    {
        public ServiceScheduler()
        {
            Services.WundergroundQuery();
            //Services.RssQuery();

            try
            {
                Timer tmrTimersTimer = new Timer();
                int second = 1000;
                int minute = second*60;
                tmrTimersTimer.Interval = 10 * minute; // 
                // Anonymous delegate
                ElapsedEventHandler handler = new ElapsedEventHandler(delegate(object o, ElapsedEventArgs e)
                {
                    Services.WundergroundQuery();
                    Services.RssQuery();
                });
                tmrTimersTimer.Elapsed += handler;
                tmrTimersTimer.Start();
            }
            catch (Exception ex)
            {
                
            }
        }

    }
}