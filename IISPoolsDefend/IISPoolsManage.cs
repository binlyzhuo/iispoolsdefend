using System;
using System.Configuration;
using System.DirectoryServices;
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
            _timer.Elapsed += OnTick;
            //_timer.Elapsed += (sender, eventArgs) => Console.WriteLine("It is {0} and all is well", DateTime.Now);
        }
        public void Start()
        {
            LogHelper.Debug("Start!");
            _timer.Start();
        }

        public void Stop()
        {
            LogHelper.Debug("Stop");
            _timer.Stop();
        }

        protected virtual void OnTick(object sender, ElapsedEventArgs e)
        {
            LogHelper.Debug("Tick:" + DateTime.Now.ToLongTimeString());
            IISPoolsDefend();
        }

        void IISPoolsDefend()
        {
            try
            {
                string entPath = "IIS://LOCALHOST/W3SVC/AppPools";

                DirectoryEntry rootEntry = new DirectoryEntry(entPath);

                foreach (DirectoryEntry appPool in rootEntry.Children)
                {
                    string appState = appPool.Properties["AppPoolState"].Value.ToString();

                    //Console.WriteLine(appPool.Name + ",state:" + appState);
                    if (appState != "2")
                    {
                        appPool.Invoke("Start", null);
                        appPool.CommitChanges();
                        LogHelper.Debug($"{appPool.Name} reboot,time:{DateTime.Now}");
                    }
                    appPool.Close();
                }

                

            }
            catch (Exception ex)
            {
                LogHelper.Error("IISPoolsDefend",ex);
            }
        }
    }
}