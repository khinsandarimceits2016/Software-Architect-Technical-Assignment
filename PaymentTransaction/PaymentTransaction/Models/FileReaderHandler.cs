using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PaymentTransaction.Models
{
    public abstract class FileReaderHandler
    {
        public static FileUploadReader GetFileReaderInstance(string fileType)
        {
            switch (fileType)
            {
                case ".xml": return new XmlFileReader();
                case ".csv":return new CsvFileReader();
                default:return null;
            }
        }


    }
}