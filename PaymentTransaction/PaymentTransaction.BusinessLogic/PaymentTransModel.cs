using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaymentTransaction.BusinessLogic
{
    public class PaymentTransModel
    {
        public string TransactionId { get; set; }
        public decimal? Amount { get; set; }
        public string CurrencyCode { get; set; }
        public DateTime? TransactionDate { get; set; }
        public string Status { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public string OrgFileName { get; set; }
        public string ServerFileName { get; set; }
    }
}
