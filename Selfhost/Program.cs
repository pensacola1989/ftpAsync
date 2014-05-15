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
        private const string FtpUri = @"ftp://cnws-prod-bjb-001.ftp.chinacloudsites.chinacloudapi.cn";
        private const string UserName = @"ftptest\daniel1989";
        private const string UserPass = "wuweiwei1989";
        private const string LocalPath = @"D:\NodeWorkSpace\Athena\files\";
        static void Main(string[] args)
        {
            var ftpClient = new Ftp(FtpUri, UserName, UserPass);
            var list = GetFileNameList(ftpClient, "/files");
            if (list.Any())
            {
                Array.ForEach(list, filename =>
                {
                    if (filename == "") return;
                    var remotePath = "/files/" + filename;
                    var localPath = LocalPath + filename;
                    Console.WriteLine(remotePath + "..." + localPath);
                    ftpClient.Download(remotePath, localPath);
                });
            }
            //var schedule = new Schedule(13, 49);
            //schedule.ScheduleJob(t =>
            //{
            //    Console.WriteLine("Trigger");
            //    //schedule.Timer.Change(10000, 1000);
            //});
            Console.ReadKey();
        }

        static string[] GetFileNameList(Ftp client,string folder)
        {
            if (client == null)
                throw new NullReferenceException();
            var list = client.DirectoryListSimple(folder);
            return list;
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

        public string[] RangeOfWeek { get; set; }

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

            var startDelay = (int)((PointTime - CurrentTime).TotalMilliseconds);

            var interval = new TimeSpan(1, 0, 0, 0);
            Timer.Change(startDelay, (int)interval.TotalMilliseconds);
        }

        //public string GetDayOfWeek()
        //{
        //    string[] weekdays = { "星期日", "星期一", "星期二", "星期三", "星期四", "星期五", "星期六" };
        //    return weekdays[Convert.ToInt32(DateTime.Now.DayOfWeek)];
        //}
    }
}
