using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PaymentTransaction.Models
{
    public class CustomJsonObject
    {
    }

    public class CurrencyType
    {
        [JsonProperty(PropertyName = "currencytype")]
        public string Currency_Type { get; set; }
    }

    public class StatusCode
    {
        [JsonProperty(PropertyName = "statuscode")]
        public string status { get; set; }
    }

    public class DateRange
    {
        [JsonProperty(PropertyName = "fromdate")]
        public string FromDate { get; set; }
        [JsonProperty(PropertyName = "todate")]
        public string ToDate { get; set; }
    }
}