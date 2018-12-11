using System;
using System.IO;
using log4net;

namespace IISPoolsDefend
{
    /// <summary>
    /// 日志相关公共方法
    /// </summary>
    public class LogHelper
    {
        private static readonly ILog logerror = LogManager.GetLogger("logerror");
        private static readonly ILog logdebug = LogManager.GetLogger("logdebug");
        private static readonly ILog loginfo = LogManager.GetLogger("loginfo");

        /// <summary>
        /// 加载配置文件
        /// </summary>
        public static void LogConfig(string fileName)
        {
            log4net.Config.XmlConfigurator.Configure(new System.IO.FileInfo(fileName));
        }

        public static void LogConfig(FileInfo file)
        {
            log4net.Config.XmlConfigurator.Configure(file);
        }

        /// <summary>
        /// 记录错误信息
        /// </summary>
        /// <param name="errorMessage"></param>
        /// <param name="ex"></param>
        public static void Error(string errorMessage, Exception ex)
        {
            logerror.Error(errorMessage, ex);
        }

        public static void Error(string errorMessage, Exception ex, Action<object> action, object obj)
        {
            action?.Invoke(obj);
            logerror.Error(errorMessage, ex);
        }

        /// <summary>
        /// 记录错误信息
        /// </summary>
        /// <param name="errorMessage"></param>
        public static void Error(string errorMessage)
        {
            logerror.Error(errorMessage);
        }

        /// <summary>
        /// 记录错误信息
        /// </summary>
        /// <param name="ex"></param>
        public static void Error(Exception ex)
        {
            logerror.Error(ex);
        }

        /// <summary>
        /// 记录信息
        /// </summary>
        /// <param name="info"></param>
        public static void Info(string info)
        {
            loginfo.Info(info);
        }

        /// <summary>
        /// 记录debug信息
        /// </summary>
        /// <param name="debug"></param>
        public static void Debug(string debug)
        {
            logdebug.Debug(debug);
        }
    }
}