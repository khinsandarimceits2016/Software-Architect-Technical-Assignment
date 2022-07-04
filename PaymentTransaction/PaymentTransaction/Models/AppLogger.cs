using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace PaymentTransaction.Models
{
    public class AppLogger
    {
        private static Object myLock = new Object();
        public static void WriteLog(string data)
        {
            try
            {
                lock (myLock)
                {
                    string serverPath = AppDomain.CurrentDomain.BaseDirectory+ "App_Data\\";
                    //create directory if doesn't exist
                    if (!Directory.Exists(serverPath + AppCode.LOG_FILE_PATH))
                    {
                        Directory.CreateDirectory(serverPath + AppCode.LOG_FILE_PATH);
                    }
                    string logFileNamewithPath = serverPath+AppCode.LOG_FILE_PATH + "\\APILog.log";
                    using (StreamWriter writer = new StreamWriter(logFileNamewithPath, true))
                    {
                        writer.WriteLine(DateTime.Now.ToString("yyyy-MMM-dd HH:mm:ss.fff tt") + ":" + data);
                    }
                    // Rename the file as backup, if the file size exceeds 5MB
                    FileInfo FinLog = new FileInfo(logFileNamewithPath);
                    long nFilelen = FinLog.Length;
                    if (nFilelen > (1024 * 1024 * 5))
                    {
                        File.Move(logFileNamewithPath, serverPath + AppCode.LOG_FILE_PATH + "\\APILog_" + DateTime.Now.ToString("ddMMyyHHmm") + ".log");
                    }
                }
            }
            catch (Exception ex)
            {
                string message = ex.Message;
            }
        }

    }
}