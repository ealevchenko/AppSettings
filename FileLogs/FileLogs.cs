using log4net;
using log4net.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageLog
{
    public static class FileLogs
    {
        private static ILog log = LogManager.GetLogger("LOGGER");


        public static ILog Log
        {
            get { return log; }
        }

        public static void InitLogger()
        {
            XmlConfigurator.Configure();
        }

        public static void SaveError(this string message)
        {
            Log.Error(message);
        }

        public static void SaveError(this string message, Exception ex )
        {
            Log.Error(message, ex);
        }

        public static void SaveDebug(this string message)
        {
            Log.Debug(message);
        }

        public static void SaveDebug(this string message, Exception ex)
        {
            Log.Debug(message, ex);
        }

        public static void SaveInformation(this string message)
        {
            Log.Info(message);
        }

        public static void SaveInformation(this string message, Exception ex)
        {
            Log.Info(message, ex);
        }

        public static void SaveWarning(this string message)
        {
            Log.Warn(message);
        }

        public static void SaveWarning(this string message, Exception ex )
        {
            Log.Warn(message, ex);
        }

        public static void SaveFatal(this string message)
        {
            Log.Fatal(message);
        }

        public static void SaveFatal(this string message, Exception ex)
        {
            Log.Fatal(message, ex);
        }
        /// <summary>
        /// Сохранить ошибку выполнения метода
        /// </summary>
        /// <param name="e"></param>
        /// <param name="method"></param>
        public static void SaveErrorMethod(this Exception e, string method, bool blog)
        {
            Console.WriteLine(e.ToString());
            String.Format("Ошибка выполнения метода {0}", method).SaveError(e);
        }

    }
}
