using PayPal.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PayPalServer.Models
{
    public class PayPalRequest : BaseSamplePage
    {
        private CardInput cardInput;
        public PayPalRequest(CardInput inputPar)
        {
            cardInput = inputPar;
        }
        protected override void RunSample()
        {
            var apiContext = PayPalConfig.GetAPIContext();

            // A transaction defines the contract of a payment.
            var transaction = new Transaction()
            {
                amount = new Amount()
                {
                    currency = "USD",
                    total = "7",
                    details = new Details()
                    {
                        shipping = "1",
                        subtotal = "5",
                        tax = "1"
                    }
                },
                description = "This is the payment transaction description.",
                item_list = new PayPal.Api.ItemList()
                {
                    items = new List<Item>()
                    {
                        new Item()
                        {
                            name = "Item Name",
                            currency = "USD",
                            price = "1",
                            quantity = "5",
                            sku = "sku"
                        }
                    },
                    shipping_address = new ShippingAddress
                    {
                        city = "Johnstown",
                        country_code = "US",
                        line1 = "52 N Main ST",
                        postal_code = "43210",
                        state = "OH",
                        recipient_name = "Basher Buyer"
                    }
                },
                invoice_number = PayPalCommon.GetRandomInvoiceNumber()
            };

            // A resource representing a Payer that funds a payment.
            var payer = new PayPal.Api.Payer()
            {
                payment_method = "credit_card",
                funding_instruments = new List<FundingInstrument>()
                {
                    new FundingInstrument()
                    {
                        credit_card = new CreditCard()
                        {
                            billing_address = new Address()
                            {
                                city = "Johnstown",
                                country_code = "US",
                                line1 = "52 N Main ST",
                                postal_code = "43210",
                                state = "OH"
                            },
                            cvv2 = "000",
                            expire_month = 01,
                            expire_year = 2022,
                            first_name = "Khademul",
                            last_name = "Basher",
                            number = "****************",
                            type = "visa"
                        }
                    }
                },
                payer_info = new PayerInfo
                {
                    email = "khademulbasher-buyer@gmail.com"
                }
            };

            // A Payment resource; create one using the above types and intent as `sale` or `authorize`
            var payment = new PayPal.Api.Payment()
            {
                intent = "sale",
                payer = payer,
                transactions = new List<Transaction>() { transaction }
            };

            // ^ Ignore workflow code segment
            #region Track Workflow
            this.flow.AddNewRequest("Create credit card payment", payment);
            #endregion

            // Create a payment using a valid APIContext
            var createdPayment = payment.Create(apiContext);

            // ^ Ignore workflow code segment
            #region Track Workflow
            this.flow.RecordResponse(createdPayment);
            #endregion

            // For more information, please visit [PayPal Developer REST API Reference](https://developer.paypal.com/docs/api/).
        }
    }
}