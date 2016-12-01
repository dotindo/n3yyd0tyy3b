using System;
using NLog;

namespace DotWeb.Utils
{
    public class AppLogger
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        public static void LogError(Exception ex)
        {
            logger.Error(ex.Message);
            logger.Error(ex.InnerException);
            logger.Error(ex.StackTrace);
        }

        public static void LogError(string exception)
        {
            logger.Error(exception);
        }

        public static void Info(string info)
        {
            logger.Info(info);
        }
    }
}
