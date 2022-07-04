using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web;
using PaymentTransaction.BusinessLogic;

namespace PaymentTransaction.Models
{
    public abstract class FileUploadReader
    {
        public string[] SpitCsv(string line)
        {
            List<string> result = new List<string>();
            StringBuilder currentStr = new StringBuilder("");
            bool inQuotes = false;
            for (int i = 0; i < line.Length; i++) // For each character
            {
                if (line[i] == '\"') // Quotes are closing or opening
                    inQuotes = !inQuotes;
                else if (line[i] == ',') // Comma
                {
                    if (!inQuotes) // If not in quotes, end of current string, add it to result
                    {
                        result.Add(currentStr.ToString());
                        currentStr.Clear();
                    }
                    else
                        currentStr.Append(line[i]); // If in quotes, just add it 
                }
                else // Add any other character to current string
                    currentStr.Append(line[i]);
            }
            result.Add(currentStr.ToString());
            return result.ToArray(); // Return array of all strings
        }
        public string CheckValueNullOrEmpty(PaymentTransModel transdata)
        {
            StringBuilder sbValidate = new StringBuilder();
            sbValidate.Append(string.IsNullOrEmpty(transdata.TransactionId) ? "TransactionId is not valid." : "");
            sbValidate.Append(transdata.Amount == null ? "Amount is not valid." : "");
            sbValidate.Append(string.IsNullOrEmpty(transdata.CurrencyCode) ? "Currency code is not valid." : "");
            sbValidate.Append(transdata.TransactionDate == null ? "Transaction date is not valid." : "");
            sbValidate.Append(string.IsNullOrEmpty(transdata.Status) ? "Status is not valid." : "");

            return sbValidate.ToString();
        }
        public Boolean IsFileFormatValid(string[] transArray)
        {
            return transArray.Length == 5;
        }
        public DateTime? ChangeStringToDatetime(string fromDatetimeString, string fromFormatDatetime, string toFormatDatetime)
        {
            try
            {
                IFormatProvider culture = new CultureInfo("en-GB", true);
                string formatStringDateTime = DateTime.ParseExact(fromDatetimeString, fromFormatDatetime, culture).ToString(toFormatDatetime);
                return Convert.ToDateTime(formatStringDateTime, culture);
            }
            catch (FormatException f)
            {
                AppLogger.WriteLog("Format exception occur ChangeStringToDatetime.Datetime string: " + fromDatetimeString + ".Error Message:" + f.Message + ".");
                return null;
            }
            catch (Exception ex)
            {
                AppLogger.WriteLog("Exception occur ChangeStringToDatetime.Datetime string: " + fromDatetimeString + ".Error Message:" + ex.Message + ".");
                return null;
            }
        }
        public DateTime? ChangeISODateStringToDatetime(string fromDatetimeString)
        {
            try
            {
                return Convert.ToDateTime(fromDatetimeString);
            }
            catch(Exception ex)
            {
                AppLogger.WriteLog("Exception occur ChangeStringToDatetime.Datetime string: " + fromDatetimeString + ".Error Message:" + ex.Message + ".");
                return null;
            }
        }
        public abstract List<PaymentTransModel> ReadFile(string filePath, ref string returnString);
    }
}