using NUnit.Framework;
using System;
using Bancomer;
using Bancomer.Entities;
using Bancomer.Entities.Request;
using System.Collections.Generic;

namespace BancomerNUnitTests
{
	[TestFixture ()]
	public class ChargeTokenTest
	{

		[Test()]
		public void TestGetId()
		{
			BancomerAPI bancomerAPI = new BancomerAPI(Constants.API_KEY, Constants.MERCHANT_ID);
			Decimal amount = new Decimal(200.00);
			
			Token token = bancomerAPI.TokenService.Create(GetTokenRequest());
            
            List<IParameter> request = new List<IParameter> {
                new SingleParameter("affiliation_bbva", "720931"),
                new SingleParameter("amount", "200.00"),
                new SingleParameter("description", "Test Charge"),
                new SingleParameter("customer_language", "SP"),
                new SingleParameter("capture", "false"),
                new SingleParameter("use_3d_secure", "false"),
                new SingleParameter("use_card_points", "NONE"),
                new SingleParameter("token", token.Id),
                new SingleParameter("currency", "MXN"),
                new SingleParameter("order_id", "oid-00051"),
                GetCustomer()
            };

			Charge charge = bancomerAPI.ChargeService.Create(request);
			Assert.IsNotNull(charge);
			Assert.IsNotNull(charge.Id);
			Assert.IsNotNull(charge.CreationDate);

			Charge chargeFound = bancomerAPI.ChargeService.Get(charge.Id);
			Assert.IsNotNull(chargeFound);
		}
        
		[Test()]
		public void TestChargeToken()
		{
			BancomerAPI bancomerAPI = new BancomerAPI(Constants.API_KEY, Constants.MERCHANT_ID);
			Decimal amount = new Decimal(200.00);

			Token token = bancomerAPI.TokenService.Create(GetTokenRequest());

            List<IParameter> request = new List<IParameter> {
                new SingleParameter("affiliation_bbva", "720931"),
                new SingleParameter("amount", "200.00"),
                new SingleParameter("description", "Test Charge"),
                new SingleParameter("customer_language", "SP"),
                new SingleParameter("capture", "false"),
                new SingleParameter("use_3d_secure", "false"),
                new SingleParameter("use_card_points", "NONE"),
                new SingleParameter("token", token.Id),
                new SingleParameter("currency", "MXN"),
                new SingleParameter("order_id", "oid-00051"),
                GetCustomer()
            };

            Charge charge = bancomerAPI.ChargeService.Create(request);
			Assert.IsNotNull(charge);
			Assert.IsNotNull(charge.Id);
			Assert.IsNotNull(charge.CreationDate);
		}

		[Test()]
		public void TestRefundCharge()
		{

			BancomerAPI bancomerAPI = new BancomerAPI(Constants.NEW_API_KEY, Constants.NEW_MERCHANT_ID);

			Decimal amount = new Decimal(200.00);
			
			Token token = bancomerAPI.TokenService.Create(GetTokenRequest());

            List<IParameter> request = new List<IParameter> {
                new SingleParameter("affiliation_bbva", "720931"),
                new SingleParameter("amount", "200.00"),
                new SingleParameter("description", "Test Charge"),
                new SingleParameter("customer_language", "SP"),
                new SingleParameter("capture", "TRUE"),
                new SingleParameter("use_3d_secure", "FALSE"),
                new SingleParameter("use_card_points", "NONE"),
                new SingleParameter("token", token.Id),
                new SingleParameter("currency", "MXN"),
                new SingleParameter("order_id", "oid-00051"),
                GetCustomer()
            };

            Charge charge = bancomerAPI.ChargeService.Create(request);
			Assert.IsNotNull(charge);
			Assert.IsNotNull(charge.Id);
			Assert.IsNotNull(charge.CreationDate);
			Assert.AreEqual("completed", charge.Status);

			string description = "reembolso desce .Net de " + amount;

			Charge refund = bancomerAPI.ChargeService.Refund(charge.Id, description, amount);

			Assert.IsNotNull(refund);
			Assert.IsNotNull(refund.Id);
			Assert.IsNotNull(refund.CreationDate);
			Assert.AreEqual("completed", refund.Status);
		}

		[Microsoft.VisualStudio.TestTools.UnitTesting.TestMethod]
        public void TestChargeAndCapture()
        {
            BancomerAPI bancomerAPI = new BancomerAPI(Constants.API_KEY, Constants.MERCHANT_ID);
            Decimal amount = new Decimal(200.00);
			Token token = bancomerAPI.TokenService.Create(GetTokenRequest());

            List<IParameter> request = new List<IParameter> {
                new SingleParameter("affiliation_bbva", "720931"),
                new SingleParameter("amount", "200.00"),
                new SingleParameter("description", "Test Charge"),
                new SingleParameter("customer_language", "SP"),
                new SingleParameter("capture", "FALSE"),
                new SingleParameter("use_3d_secure", "FALSE"),
                new SingleParameter("use_card_points", "NONE"),
                new SingleParameter("token", token.Id),
                new SingleParameter("currency", "MXN"),
                new SingleParameter("order_id", "oid-00051"),
                GetCustomer()
            };

            Charge charge = bancomerAPI.ChargeService.Create(request);
            Assert.IsNotNull(charge);
            Assert.IsNotNull(charge.Id);
            Assert.IsNotNull(charge.CreationDate);
            Assert.AreEqual("in_progress", charge.Status);

            Charge chargeCompleted = bancomerAPI.ChargeService.Capture(charge.Id, amount);
            Assert.IsNotNull(chargeCompleted);
            Assert.AreEqual("completed", chargeCompleted.Status);
            Assert.AreEqual(charge.Amount, chargeCompleted.Amount);
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

