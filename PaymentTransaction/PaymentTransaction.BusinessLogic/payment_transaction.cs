//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PaymentTransaction.BusinessLogic
{
    using System;
    using System.Collections.Generic;
    
    public partial class payment_transaction
    {
        public int id { get; set; }
        public string transaction_id { get; set; }
        public Nullable<decimal> amount { get; set; }
        public string currency_code { get; set; }
        public Nullable<System.DateTime> transaction_date { get; set; }
        public string transaction_status { get; set; }
        public Nullable<System.DateTime> created_datetime { get; set; }
    }
}