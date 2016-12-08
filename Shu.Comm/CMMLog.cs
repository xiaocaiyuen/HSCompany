using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shu.Comm
{
    /// <name> CommonLog </name>
    /// <summary>
    /// 使用log4net封装对Log的操作
    /// </summary>
    public sealed class CMMLog
    {
        /// <summary>
        /// 声明私有的构造函数
        /// </summary>
        private CMMLog()
        {
        }
        //用于记录信息的log
        private static ILog _log;

        /// <summary>
        /// 用于Trace的log
        /// </summary>
        private static ILog Log
        {
            get
            {

                if (_log == null)
                {
                    log4net.Config.XmlConfigurator.Configure();
                    _log = LogManager.GetLogger(Constant.DS_PROFILE_LOG);
                }

                return _log;
            }
        }

        /// <summary>
        /// 记录错误信息
        /// </summary>
        /// <param name="msg">错误消息</param>
        /// <param name="ex">Exception</param>
        public static void Error(String msg, Exception ex)
        {
            Log.Error(msg, ex);
        }

        /// <summary>
        /// 记录错误信息
        /// </summary>
        /// <param name="msg">错误信息</param>
        public static void Error(String msg)
        {
            Log.Error(msg);
        }

        /// <summary>
        /// 记录一般信息
        /// </summary>
        /// <param name="msg">一般信息</param>
        public static void Info(String msg)
        {
            Log.Info(msg);
        }

        /// <summary>
        /// 记录调试信息
        /// </summary>
        /// <param name="msg">调试信息</param>
        public static void Debug(String msg)
        {
            Log.Debug(msg);
        }

        /// <summary>
        /// 记录调试信息
        /// </summary>
        /// <param name="msg">调试信息</param>
        public static void Fatal(String msg)
        {
            Log.Fatal(msg);
        }

    }
}
