using System;
using System.Configuration;
using System.Timers;

namespace IISPoolsDefend
{
    public class IISPoolsManage
    {
        readonly Timer _timer;

        public IISPoolsManage()
        {
            int timeConfig =int.Parse(ConfigurationManager.AppSettings["time"]) ;
            _timer = new Timer(timeConfig*1000) { AutoReset = true };
            _timer.Elapsed += new ElapsedEventHandler(OnTick);
            _timer.Elapsed += (sender, eventArgs) => Console.WriteLine("It is {0} and all is well", DateTime.Now);
        }
        public void Start()
        {
            LogHelper.Debug("Start!");
        }

        public void Stop()
        {
            LogHelper.Debug("Stop");
        }

        protected virtual void OnTick(object sender, ElapsedEventArgs e)
        {
            LogHelper.Debug("Tick:" + DateTime.Now.ToLongTimeString());
        }
    }
}