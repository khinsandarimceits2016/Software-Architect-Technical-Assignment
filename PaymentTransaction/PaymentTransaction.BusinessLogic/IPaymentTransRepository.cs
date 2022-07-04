using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentTransaction.BusinessLogic
{
    public interface IPaymentTransRepository
    {
        string SavePaymentTransaction(PaymentTransModel paymentTrans);
        List<PaymentTransViewModel> GetPaymentTransByCurrency(string currencyType);
        List<PaymentTransViewModel> GetPaymentTransByDateRange(string fromDate, string toDate);
        List<PaymentTransViewModel> GetPayentTransByStatus(string statuscode);
    }
}
