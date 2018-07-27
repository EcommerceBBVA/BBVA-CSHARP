using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Bancomer;
using Bancomer.Entities;
using Bancomer.Entities.Request;
using System.Collections.Generic;

namespace BancomerTest
{
    [TestClass]
    public class ChargeServiceTest
    {
        [TestMethod]
        public void TestChargeAndGetToMerchant()
        {
            BancomerAPI bancomerAPI = new BancomerAPI(Constants.API_KEY, Constants.MERCHANT_ID);
            
            Token tokenCreated = bancomerAPI.TokenService.Create(GetTokenRequest());

            List<IParameter> request = new List<IParameter> {
                new SingleParameter("affiliation_bbva", "720931"),
                new SingleParameter("amount", "200.00"),
                new SingleParameter("description", "Test Charge"),
                new SingleParameter("customer_language", "SP"),
                new SingleParameter("capture", "false"),
                new SingleParameter("use_3d_secure", "false"),
                new SingleParameter("use_card_points", "NONE"),
                new SingleParameter("token", tokenCreated.Id),
                new SingleParameter("currency", "MXN"),
                new SingleParameter("order_id", "oid-00051")
            };
            
            request.Add(GetCustomer());
            
            Charge charge = bancomerAPI.ChargeService.Create(request);
            Assert.IsNull(charge.CardPoints);
            Assert.IsNotNull(charge);
            Assert.IsNotNull(charge.Id);

            Charge charge2 = bancomerAPI.ChargeService.Get(charge.Id);
            Assert.IsNotNull(charge2);
            Assert.IsNull(charge2.CardPoints);
            Assert.AreEqual(charge.Id, charge2.Id);
            Assert.AreEqual(charge.Amount, charge2.Amount);
        }
        
        [TestMethod]
        public void TestChargeToMerchantAndRefund()
        {
            BancomerAPI bancomerAPI = new BancomerAPI(Constants.API_KEY, Constants.MERCHANT_ID);

            Token tokenCreated = bancomerAPI.TokenService.Create(GetTokenRequest());

            List<IParameter> request = new List<IParameter> {
                new SingleParameter("affiliation_bbva", "720931"),
                new SingleParameter("amount", "200.00"),
                new SingleParameter("description", "Test Charge"),
                new SingleParameter("customer_language", "SP"),
                new SingleParameter("capture", "TRUE"),
                new SingleParameter("use_3d_secure", "FALSE"),
                new SingleParameter("use_card_points", "NONE"),
                new SingleParameter("token", tokenCreated.Id),
                new SingleParameter("currency", "MXN"),
                new SingleParameter("order_id", "oid-00051")
            };
            
            Charge charge = bancomerAPI.ChargeService.Create(request);
            Assert.IsNotNull(charge);
            Assert.IsNotNull(charge.Id);

            Charge refund = bancomerAPI.ChargeService.Refund(charge.Id, "Merchant Refund", new Decimal(200.00));
            Assert.IsNotNull(refund);
            Assert.IsNotNull(refund.Refund);
        }

        private ParameterContainer GetCustomer()
        {
            ParameterContainer address = new ParameterContainer("address");
            address.addValue("line1", "Calle Morelos #12 - 11");
            address.addValue("line2", "Colonia Centro");           // Optional
            address.addValue("line3", "Cuauhtémoc");               // Optional
            address.addValue("city", "Queretaro");
            address.addValue("postal_code", "12345");
            address.addValue("state", "Queretaro");
            address.addValue("country_code", "MX");

            ParameterContainer customer = new ParameterContainer("customer");
            customer.addValue("name", "John");
            customer.addValue("last_name", "Doe");
            customer.addValue("email", "johndoe@example.com");
            customer.addValue("phone_number", "554-170-3567");
            customer.addMultiValue(address);
            return customer;
        }

        private List<IParameter> GetTokenRequest()
        {
            return new List<IParameter>{
                new SingleParameter("holder_nane", "Juan Perez Ramirez"),
                new SingleParameter("card_number", "4111111111111111"),
                new SingleParameter("cvv2", "022"),
                new SingleParameter("expiration_month", "12"),
                new SingleParameter("expiration_year", "20"),
            };
        }
    }
}
