using BusinessLogic.Logs;
using NLog;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Utilities
{
    public static class Utiliti
    {
        private static ILogger logger = LogEx.GetLogger();
        public static void Log(this Exception ex, string message = "")
        {
            var msg = string.IsNullOrEmpty(message) ? ex.Message : message;
            msg = msg + "||||" + ex.StackTrace + "||||" + GetInnerExceptions(ex);
            logger.Error(ex, msg);
        }
        /// <summary>
        /// Lấy inner message sâu nhất!
        /// </summary>
        /// <param name="ex"></param>
        /// <returns></returns>
        public static string GetInnerException(this Exception ex)
        {
            while (ex.InnerException != null)
            {
                ex = ex.InnerException;
            }
            return ex.Message;
        }

        /// <summary>
        /// Lấy toàn bộ thông tin lỗi!
        /// </summary>
        /// <param name="exception"></param>
        /// <returns></returns>
        public static string GetInnerExceptions(this Exception exception)
        {
            var limit = 0;
            var result = new List<string>();
            var ex = exception;
            while (ex != null && limit++ < 10)
            {
                result.Add(ex.Message);
                ex = ex.InnerException;
            }

            var inner = string.Join("->", result.ToArray());
            return inner;
        }
        public static string GetConfigurationSettingValue(string configName, string defaultValue)
        {
            var configValue = ConfigurationManager.AppSettings[configName];
            return string.IsNullOrEmpty(configValue) ? defaultValue : configValue;
        }
    }


}
