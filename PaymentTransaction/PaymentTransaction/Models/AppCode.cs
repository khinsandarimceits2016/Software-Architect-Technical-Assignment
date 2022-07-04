using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace PaymentTransaction.Models
{
    public class AppCode
    {
        public static readonly  string UPLOAD_FILE_PATH = ConfigurationManager.AppSettings["uploadPath"].ToString();
        public static readonly string LOG_FILE_PATH = ConfigurationManager.AppSettings["logFilePath"].ToString();
    }
}