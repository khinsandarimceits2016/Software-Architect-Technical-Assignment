using Newtonsoft.Json;
using PaymentTransaction.BusinessLogic;
using PaymentTransaction.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace PaymentTransaction.Controllers
{
    public class TransactionsController : ApiController
    {
        // GET api/<controller>
        [HttpPost]
        //[Route("api/transactions/bycurrency")]
        [ActionName("currency")]
        public HttpResponseMessage GetPaymentTransByCurrency([FromBody] CurrencyType currencyType)// type object
        {
            
            PaymentTransactionHandler transactionHandler = new PaymentTransactionHandler();
            
            if (currencyType.Currency_Type.Length > 3)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
            IEnumerable<PaymentTransViewModel> lstTrans = transactionHandler.GetPaymentTransByCurrency(currencyType.Currency_Type);
            if (lstTrans != null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, lstTrans);
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.NoContent, lstTrans);
            }
            
        }
        [HttpPost]
        [ActionName("status")]
        public HttpResponseMessage GetPaymentTransByStatus([FromBody] StatusCode statuscode)
        {
            PaymentTransactionHandler transactionHandler = new PaymentTransactionHandler();

            if (statuscode.status.Length > 1)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
            IEnumerable<PaymentTransViewModel> lstTrans = transactionHandler.GetPayentTransByStatus(statuscode.status);
            if (lstTrans != null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, lstTrans);
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.NoContent, lstTrans);
            }
        }
        
        [HttpPost]
        [ActionName("daterange")]
        public HttpResponseMessage GetPaymentTransByDateRange([FromBody] DateRange daterange)
        {
            PaymentTransactionHandler transactionHandler = new PaymentTransactionHandler();

            if (string.IsNullOrEmpty(daterange.FromDate) || string.IsNullOrEmpty(daterange.ToDate))
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
            IEnumerable<PaymentTransViewModel> lstTrans = transactionHandler.GetPaymentTransByDateRange(daterange.FromDate,daterange.ToDate);
            if (lstTrans != null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, lstTrans);
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.NoContent, lstTrans);
            }
        }

    }
}