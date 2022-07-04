using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentTransaction.BusinessLogic
{
    public class PaymentTransService : IPaymentTransRepository
    {
        public List<PaymentTransViewModel> GetPayentTransByStatus(string statuscode)
        {
            try
            {
                using (TechnicalAssignmentEntities _dbContext = new TechnicalAssignmentEntities())
                {
                    List<PaymentTransViewModel> lstTrans = new List<PaymentTransViewModel>();
                    List<uspGetTransByStatus_Result> results = _dbContext.uspGetTransByStatus(statuscode).ToList();
                    if (results != null)
                    {
                        foreach (var item in results)
                        {
                            PaymentTransViewModel tran = new PaymentTransViewModel();
                            tran.id = item.transaction_id;
                            tran.payment = item.payment;
                            tran.status = item.status_code;
                            lstTrans.Add(tran);
                        }
                        return lstTrans;
                    }
                    return null;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public List<PaymentTransViewModel> GetPaymentTransByCurrency(string currencyType)
        {
            try
            {
                using (TechnicalAssignmentEntities _dbContext = new TechnicalAssignmentEntities())
                {
                    List<PaymentTransViewModel> lstTrans = new List<PaymentTransViewModel>();
                    List<uspGetTransByCurrency_Result> results = _dbContext.uspGetTransByCurrency(currencyType).ToList();
                    if (results != null)
                    {
                        foreach (var item in results)
                        {
                            PaymentTransViewModel tran = new PaymentTransViewModel();
                            tran.id = item.transaction_id;
                            tran.payment = item.payment;
                            tran.status = item.status_code;
                            lstTrans.Add(tran);
                        }
                        return lstTrans;
                    }
                    return null;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public List<PaymentTransViewModel> GetPaymentTransByDateRange(string fromDate, string toDate)
        {
            try
            {
                using (TechnicalAssignmentEntities _dbContext = new TechnicalAssignmentEntities())
                {
                    List<PaymentTransViewModel> lstTrans = new List<PaymentTransViewModel>();
                    List<uspGetTransByDateRange_Result> results = _dbContext.uspGetTransByDateRange(fromDate, toDate).ToList();
                    if (results != null)
                    {
                        foreach (var item in results)
                        {
                            PaymentTransViewModel tran = new PaymentTransViewModel();
                            tran.id = item.transaction_id;
                            tran.payment = item.payment;
                            tran.status = item.status_code;
                            lstTrans.Add(tran);
                        }
                        return lstTrans;
                    }
                    return null;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public string SavePaymentTransaction(PaymentTransModel paymentTrans)
        {
            try
            {
                using (TechnicalAssignmentEntities _dbContext = new TechnicalAssignmentEntities())
                {
                    payment_transaction objTran = new payment_transaction();
                    objTran.transaction_id = paymentTrans.TransactionId;
                    objTran.amount = paymentTrans.Amount;
                    objTran.currency_code = paymentTrans.CurrencyCode;
                    objTran.transaction_date = paymentTrans.TransactionDate;
                    objTran.transaction_status = paymentTrans.Status;
                    objTran.created_datetime = paymentTrans.CreatedDateTime;

                    _dbContext.payment_transaction.Add(objTran);
                    _dbContext.SaveChanges();
                    return Messages.SAVESUCCESS;
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
                //write log file
            }
        }
    }
}
