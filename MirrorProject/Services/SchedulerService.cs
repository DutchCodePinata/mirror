using System;
using System.Timers;

namespace MirrorProject.Models
{
    public class SchedulerService
    {
        public SchedulerService()
        {
            DataService.getWunderground();

            try
            {
                Timer tmrTimersTimer = new Timer();
                int second = 1000;
                int minute = second*60;
                tmrTimersTimer.Interval = 10 * minute; // 
                // Anonymous delegate
                ElapsedEventHandler handler = new ElapsedEventHandler(delegate(object o, ElapsedEventArgs e)
                {
                    DataService.getWunderground();
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