using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PaymentTransaction.BusinessLogic;

namespace PaymentTransaction.Models
{
    public class PaymentTransactionHandler
    {
        PaymentTransService transRepository=new PaymentTransService ();
        public string SavePaymentTransaction(PaymentTransModel paymentTrans)
        {
            return transRepository.SavePaymentTransaction(paymentTrans);
        }

        public List<PaymentTransViewModel> GetPaymentTransByCurrency(string currencyType)
        {
            return transRepository.GetPaymentTransByCurrency(currencyType);
        }

        public List<PaymentTransViewModel> GetPaymentTransByDateRange(string fromDate, string toDate)
        {
            return transRepository.GetPaymentTransByDateRange(fromDate,toDate);
        }

        public List<PaymentTransViewModel> GetPayentTransByStatus(string statuscode)
        {
            return transRepository.GetPayentTransByStatus(statuscode);
        }
    }
}