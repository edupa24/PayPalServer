using Newtonsoft.Json;
using PayPalServer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace PayPalServer.Controllers
{
    public class PayPalController : ApiController
    {
        public HttpResponseMessage Get(string cardData)
        {
            CardInput cartInput = JsonConvert.DeserializeObject<CardInput>(cardData);
            PayPalRequest payPalRequest = new PayPalRequest(cartInput);

            try
            {
                RequestFlow flow = payPalRequest.GetFlow();
                return Request.CreateResponse(HttpStatusCode.OK, flow);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.OK, "Fail");
            }
        }
    }
}
