using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Selfhost
{
    class Program
    {
        static void Main(string[] args)
        {
            var schedule = new Schedule(21, 39);
            schedule.ScheduleJob(t => Console.WriteLine("fuck"));
            Console.ReadKey();
        }
    }

    public class Schedule
    {
        public double Hours { get; set; }
        public double Minutes { get; set; }
        public double Seconds { get; set; }
        public DateTime CurrentTime { get; set; }
        public DateTime PointTime { get; set; }

        public Timer Timer { get; set; }
        public Schedule(int hours = 0,int minutes = 0,int seconds = 0)
        {
            PointTime = DateTime.Now;
            CurrentTime = DateTime.Now;
            Hours = (double) hours;
            Minutes = (double) minutes;
            Seconds = (double) seconds;
        }
        public void ScheduleJob(TimerCallback callback)
        {
            Timer = new Timer(callback);
           

            PointTime = DateTime.Today.AddHours(Hours).AddMinutes(Minutes).AddSeconds(Seconds);
            if (CurrentTime > PointTime)
                PointTime = PointTime.AddDays(1.0);

            var msUnitNine = (int)((PointTime - CurrentTime).TotalMilliseconds);
            Timer.Change(msUnitNine, Timeout.Infinite);
        }
    }
}
