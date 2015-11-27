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
            try
            {
                Timer tmrTimersTimer = new Timer();
                tmrTimersTimer.Interval = 180 * 1000; // 24 hours
                // Anonymous delegate
                ElapsedEventHandler handler = new ElapsedEventHandler(delegate(object o, ElapsedEventArgs e)
                {
                    Services.WundergroundQuery();
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