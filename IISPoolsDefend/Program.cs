using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Topshelf;

namespace IISPoolsDefend
{
    class Program
    {
        static void Main(string[] args)
        {
            FileInfo fi = new FileInfo(AppDomain.CurrentDomain.BaseDirectory + "log4net.config");
            LogHelper.LogConfig(fi);

            HostFactory.Run(x =>
            {
                x.Service<IISPoolsManage>(s =>
                {
                    s.ConstructUsing(name => new IISPoolsManage());
                    s.WhenStarted(tc => tc.Start());
                    s.WhenStopped(tc => tc.Stop());
                });
                x.RunAsLocalSystem();
                x.StartAutomatically();

                x.SetDescription("IIS应用程序池守护服务");
                x.SetDisplayName("IISPoolsDefend");
                x.SetServiceName("IISPoolsDefend");
            });
        }
    }
}
