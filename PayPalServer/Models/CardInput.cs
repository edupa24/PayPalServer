using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PayPalServer.Models
{
    public class CardInput
    {
        public string cardNumber { get; set; }
        public string city { get; set; }
        public string country { get; set; }
        public string cvc { get; set; }
        public string email { get; set; }
        public string expirationDate { get; set; }
        public string nameonCard { get; set; }
        public string postalCode { get; set; }
        public string state { get; set; }
        public string street { get; set; }
               
    }
}