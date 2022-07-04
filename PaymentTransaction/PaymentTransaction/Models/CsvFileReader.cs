using PaymentTransaction.BusinessLogic;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

namespace PaymentTransaction.Models
{
    public class CsvFileReader : FileUploadReader
    {
        public override List<PaymentTransModel> ReadFile(string filePath, ref string validationMessage)
        {
            try
            {
                List<PaymentTransModel> paymentTransModels = new List<PaymentTransModel>();
                StringBuilder sbValidator = new StringBuilder();
                int lineNo = 0;
                string[] strArray;
                using (StreamReader sr = new StreamReader(filePath))
                {
                    string strLine = string.Empty;
                    //write log start reading file
                    while (sr.Peek() >= 0)
                    {
                        try
                        {
                            strLine = sr.ReadLine();
                            lineNo++;
                            Regex CSVParser = new Regex(",(?=(?:[^\"]*\"[^\"]*\")*(?![^\"]*\"))");
                            String[] Fields = CSVParser.Split(strLine);
                            strArray = SpitCsv(strLine);
                            if (IsFileFormatValid(strArray))
                            {
                                PaymentTransModel paymentTrans = new PaymentTransModel();
                                paymentTrans.TransactionId = strArray[0].Trim();
                                string tt = strArray[1].Trim().Replace(",", "");
                                paymentTrans.Amount = Convert.ToDecimal(strArray[1].Trim().Replace(",", ""));
                                paymentTrans.CurrencyCode = strArray[2].Trim();
                                paymentTrans.TransactionDate = ChangeStringToDatetime(strArray[3].Trim(), "dd/MM/yyyy hh:mm:ss", "dd-MM-yyyy hh:mm:ss");
                                paymentTrans.Status = strArray[4].Trim();
                                paymentTrans.CreatedDateTime = DateTime.Now;
                                string checkEmpty = CheckValueNullOrEmpty(paymentTrans);
                                if (string.IsNullOrEmpty(checkEmpty))
                                {
                                    paymentTransModels.Add(paymentTrans);
                                }
                                else
                                {
                                    sbValidator.Append(checkEmpty + "at lineNo: " + lineNo + "." );
                                }

                            }
                            else
                            {
                                sbValidator.Append("File format is not valid at lineNo: " + lineNo + "." );
                            }
                        }
                        catch (Exception ex)
                        {
                            sbValidator.Append("Error occur at LineNo: " + lineNo + ". Error Message: " + ex.Message + ".");
                            return null;
                        }

                    } //finish reading file Read count:.
                }
                //validationMessage = sbValidator.ToString();
                validationMessage = string.IsNullOrEmpty(sbValidator.ToString()) ? Messages.VALID : sbValidator.ToString();
                return paymentTransModels;
            }
            catch (Exception ex)
            {
                validationMessage = ex.Message;
                return null;
            }

        }
    }
}