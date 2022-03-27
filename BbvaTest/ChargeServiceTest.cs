﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Bbva;
using Bbva.Entities;
using Bbva.Entities.Request;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace BbvaTest
{
    [TestClass]
    public class ChargeServiceTest
    {
        [Test]
        public void TestChargeAndGetToMerchant()
        {
            BbvaAPI bbvaAPI = new BbvaAPI(Constants.API_KEY, Constants.MERCHANT_ID);

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
                new SingleParameter("order_id", "oid-00051")
            };
            
            request.Add(GetCustomer());

            Dictionary<String, Object> charge = bbvaAPI.ChargeService.Create(request);
            Assert.IsNotNull(charge);
        }
        
        [Test]
        public void TestChargeToMerchantAndRefund()
        {
            BbvaAPI bbvaAPI = new BbvaAPI(Constants.API_KEY, Constants.MERCHANT_ID);

            Dictionary<String, Object> tokenCreated = bbvaAPI.TokenService.Create(GetTokenRequest());

            ParameterContainer customer = new ParameterContainer("customer");
            customer.AddValue("name", "John");
            customer.AddValue("last_name", "Doe");
            customer.AddValue("email", "johndoe@example.com");
            customer.AddValue("phone_number", "554-170-3567");
            var customerParameterValues = customer.ParameterValues;

            List<IParameter> request = new List<IParameter> {
                new SingleParameter("affiliation_bbva", "720931"),
                new SingleParameter("amount", "200.00"),
                new SingleParameter("description", "Test Charge"),
                new SingleParameter("customer_language", "SP"),
                new SingleParameter("capture", "TRUE"),
                new SingleParameter("use_3d_secure", "FALSE"),
                new SingleParameter("use_card_points", "NONE"),
                new SingleParameter("token", tokenCreated.ToString()),
                new SingleParameter("currency", "MXN"),
                new SingleParameter("order_id", "oid-00051"),
                customer
            };

            Dictionary<String, Object> chargeDictionary = bbvaAPI.ChargeService.Create(request);
            ParameterContainer charge = new ParameterContainer("charge", chargeDictionary);
            Assert.IsNotNull(charge);
            Assert.IsNotNull(charge.GetSingleValue("id").ParameterValue);
            String chargeId = charge.GetSingleValue("id").ParameterValue;

            Dictionary<String, Object> refundDict = bbvaAPI.ChargeService.Refund(chargeId, "Merchant Refund", new Decimal(200.00));
            ParameterContainer refund = new ParameterContainer("refund", refundDict);
            Assert.IsNotNull(refund);
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
