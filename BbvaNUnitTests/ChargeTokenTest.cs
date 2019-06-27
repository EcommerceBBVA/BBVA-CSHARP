using NUnit.Framework;
using System;
using Bbva;
using Bbva.Entities;
using Bbva.Entities.Request;
using System.Collections.Generic;

namespace BbvaNUnitTests
{
	[TestFixture ()]
	public class ChargeTokenTest
	{

		[Test()]
		public void TestGetId()
		{
			BbvaAPI bbvaAPI = new BbvaAPI(Constants.API_KEY, Constants.MERCHANT_ID);
			Decimal amount = new Decimal(200.00);

            Dictionary<String, Object> tokenDictionary = bbvaAPI.TokenService.Create(GetTokenRequest());
            ParameterContainer token = new ParameterContainer("token", tokenDictionary);

            List<IParameter> request = new List<IParameter> {
                new SingleParameter("affiliation_bbva", "720931"),
                new SingleParameter("amount", "200.00"),
                new SingleParameter("description", "Test Charge"),
                new SingleParameter("customer_language", "SP"),
                new SingleParameter("capture", "false"),
                new SingleParameter("use_3d_secure", "false"),
                new SingleParameter("use_card_points", "NONE"),
                new SingleParameter("token", token.GetSingleValue("id").ParameterValue),
                new SingleParameter("currency", "MXN"),
                new SingleParameter("order_id", "oid-00051"),
                GetCustomer()
            };

            Dictionary<String, Object> chargeDictionary = bbvaAPI.ChargeService.Create(request);
            ParameterContainer charge = new ParameterContainer("charge", chargeDictionary);
			Assert.IsNotNull(charge);
            String chargeId = charge.GetSingleValue("id").ParameterValue;

            chargeDictionary = bbvaAPI.ChargeService.Get(chargeId);
            charge = new ParameterContainer("charge", chargeDictionary);
			Assert.IsNotNull(charge);
            Assert.IsNotNull(charge.GetSingleValue("id").ParameterValue);
		}
        
		[Test()]
		public void TestChargeToken()
		{
			BbvaAPI bbvaAPI = new BbvaAPI(Constants.API_KEY, Constants.MERCHANT_ID);
			Decimal amount = new Decimal(200.00);

            Dictionary<String, Object> tokenDictionary = bbvaAPI.TokenService.Create(GetTokenRequest());
            ParameterContainer token = new ParameterContainer("token", tokenDictionary);

            List<IParameter> request = new List<IParameter> {
                new SingleParameter("affiliation_bbva", "720931"),
                new SingleParameter("amount", "200.00"),
                new SingleParameter("description", "Test Charge"),
                new SingleParameter("customer_language", "SP"),
                new SingleParameter("capture", "false"),
                new SingleParameter("use_3d_secure", "false"),
                new SingleParameter("use_card_points", "NONE"),
                new SingleParameter("token", token.GetSingleValue("id").ParameterValue),
                new SingleParameter("currency", "MXN"),
                new SingleParameter("order_id", "oid-00051"),
                GetCustomer()
            };

            Dictionary<String, Object> chargeDictionary = bbvaAPI.ChargeService.Create(request);
            ParameterContainer charge = new ParameterContainer("charge", chargeDictionary);
			Assert.IsNotNull(charge);
		}

		[Test()]
		public void TestRefundCharge()
		{

			BbvaAPI bbvaAPI = new BbvaAPI(Constants.NEW_API_KEY, Constants.NEW_MERCHANT_ID);

			Decimal amount = new Decimal(200.00);

            Dictionary<String, Object> tokenDictionary = bbvaAPI.TokenService.Create(GetTokenRequest());
            ParameterContainer token = new ParameterContainer("token", tokenDictionary);

            List<IParameter> request = new List<IParameter> {
                new SingleParameter("affiliation_bbva", "720931"),
                new SingleParameter("amount", "200.00"),
                new SingleParameter("description", "Test Charge"),
                new SingleParameter("customer_language", "SP"),
                new SingleParameter("capture", "TRUE"),
                new SingleParameter("use_3d_secure", "FALSE"),
                new SingleParameter("use_card_points", "NONE"),
                new SingleParameter("token", token.GetSingleValue("id").ParameterValue),
                new SingleParameter("currency", "MXN"),
                new SingleParameter("order_id", "oid-00051"),
                GetCustomer()
            };

            Dictionary<String, Object> chargeDictionary = bbvaAPI.ChargeService.Create(request);
            ParameterContainer charge = new ParameterContainer("charge", chargeDictionary);
            Assert.IsNotNull(charge);

			string description = "reembolso desce .Net de " + amount;

            Dictionary<String, Object> refund = bbvaAPI.ChargeService.Refund(charge.GetSingleValue("id").ParameterValue, description, amount);
			Assert.IsNotNull(refund);
		}

		[Microsoft.VisualStudio.TestTools.UnitTesting.TestMethod]
        public void TestChargeAndCapture()
        {
            BbvaAPI bbvaAPI = new BbvaAPI(Constants.API_KEY, Constants.MERCHANT_ID);
            Decimal amount = new Decimal(200.00);
            Dictionary<String, Object> tokenDictionary = bbvaAPI.TokenService.Create(GetTokenRequest());
            ParameterContainer token = new ParameterContainer("token", tokenDictionary);

            List<IParameter> request = new List<IParameter> {
                new SingleParameter("affiliation_bbva", "720931"),
                new SingleParameter("amount", "200.00"),
                new SingleParameter("description", "Test Charge"),
                new SingleParameter("customer_language", "SP"),
                new SingleParameter("capture", "FALSE"),
                new SingleParameter("use_3d_secure", "FALSE"),
                new SingleParameter("use_card_points", "NONE"),
                new SingleParameter("token", token.GetSingleValue("id").ParameterValue),
                new SingleParameter("currency", "MXN"),
                new SingleParameter("order_id", "oid-00051"),
                GetCustomer()
            };

            Dictionary<String, Object> chargeDictionary = bbvaAPI.ChargeService.Create(request);
            ParameterContainer charge = new ParameterContainer("charge", chargeDictionary);
            Assert.IsNotNull(charge);
            Assert.IsNotNull(charge.GetSingleValue("id").ParameterValue);
            Assert.IsNotNull(charge.GetSingleValue("creation_date").ParameterValue);
            Assert.AreEqual("in_progress", charge.GetSingleValue("status").ParameterValue);

            Dictionary<String, Object> chargeCompletedDict = bbvaAPI.ChargeService.Capture(charge.GetSingleValue("id").ParameterValue, amount);
            ParameterContainer chargeCompleted = new ParameterContainer("charge", chargeCompletedDict);
            Assert.IsNotNull(chargeCompleted);
            Assert.AreEqual("completed", charge.GetSingleValue("status").ParameterValue);
            Assert.AreEqual(charge.GetSingleValue("amount").ParameterValue, chargeCompleted.GetSingleValue("amount").ParameterValue);
        }

        private ParameterContainer GetCustomer()
		{
            ParameterContainer address = new ParameterContainer("address");
            address.AddValue("line1", "Calle Morelos #12 - 11");
            address.AddValue("line2", "Colonia Centro");           // Optional
            address.AddValue("line3", "Cuauhtémoc");               // Optional
            address.AddValue("city", "Queretaro");
            address.AddValue("postal_code", "12345");
            address.AddValue("state", "Queretaro");
            address.AddValue("country_code", "MX");

            ParameterContainer customer = new ParameterContainer("customer");
            customer.AddValue("name", "John");
            customer.AddValue("last_name", "Doe");
            customer.AddValue("email", "johndoe@example.com");
            customer.AddValue("phone_number", "554-170-3567");
            customer.AddMultiValue(address);
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

