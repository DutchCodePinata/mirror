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
                int second = 1000;
                int minute = second*60;
                tmrTimersTimer.Interval = 0.5 * minute; // 
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