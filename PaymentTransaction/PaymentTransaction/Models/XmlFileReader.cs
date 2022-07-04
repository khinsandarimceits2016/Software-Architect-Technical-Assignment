using PaymentTransaction.BusinessLogic;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;

namespace PaymentTransaction.Models
{
    public class XmlFileReader : FileUploadReader
    {
        public override List<PaymentTransModel> ReadFile(string filePath, ref string validationMessage)
        {
            try
            {
                List<PaymentTransModel> lstPaymentTransModels = new List<PaymentTransModel>();
                StringBuilder sbValidator = new StringBuilder();
                DataSet ds = new DataSet();//Using dataset to read xml file  
                try
                {
                    ds.ReadXml(filePath);
                }catch(Exception ex)
                {
                    sbValidator.Append("File cannot read.");
                    return null;
                }
                
                
                if (ds.Tables.Contains("Transaction") && ds.Tables.Contains("PaymentDetails"))
                {
                    int outerIdx = 0;
                    int innerIdx = 1;
                    foreach (DataRow dr in ds.Tables["Transaction"].Rows)
                    {
                        outerIdx++;
                        PaymentTransModel paymentTrans = new PaymentTransModel();
                        paymentTrans.TransactionId = dr["id"].ToString().Trim();
                        paymentTrans.TransactionDate = ChangeISODateStringToDatetime(dr["TransactionDate"].ToString().Trim());
                        paymentTrans.Status = dr["Status"].ToString().Trim();
                        paymentTrans.CreatedDateTime = DateTime.Now;
                        foreach (DataRow row in ds.Tables["PaymentDetails"].Rows)
                        {

                            if (innerIdx == outerIdx)
                            {
                                // add to object
                                paymentTrans.Amount = Convert.ToDecimal(row["Amount"].ToString().Trim().Replace(",", ""));
                                paymentTrans.CurrencyCode = row["CurrencyCode"].ToString().Trim();
                                innerIdx++;
                            }
                        }
                        string checkEmpty = CheckValueNullOrEmpty(paymentTrans);
                        if (string.IsNullOrEmpty(checkEmpty))
                        {
                            lstPaymentTransModels.Add(paymentTrans);
                        }
                        else
                        {
                            sbValidator.Append(checkEmpty + Environment.NewLine);
                        }
                    }
                }
                else
                {
                    sbValidator.Append("File format is not valid." + Environment.NewLine);
                }

                validationMessage = string.IsNullOrEmpty(sbValidator.ToString()) ? Messages.VALID : sbValidator.ToString();
                return lstPaymentTransModels;
            }
            catch (Exception ex)
            {
                validationMessage = ex.Message;
            }
            return null;
        }
    }
}