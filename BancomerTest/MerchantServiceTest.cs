using Microsoft.VisualStudio.TestTools.UnitTesting;
using Bancomer;
using Bancomer.Entities;
using Bancomer.Entities.Request;
using System;
using System.Collections.Generic;

namespace BancomerTest
{
    [TestClass]
    public class MerchantServiceTest
    {

        [TestMethod]
        public void TestMerchant_Get()
        {
            BancomerAPI bancomerAPI = new BancomerAPI(Constants.API_KEY, Constants.MERCHANT_ID);
            Dictionary<String, Object> merchantDictionary = bancomerAPI.MerchantService.Get();
            ParameterContainer merchant = new ParameterContainer("merchant", merchantDictionary);
            Assert.IsNotNull(merchant);
            Assert.IsNotNull(merchant.GetSingleValue("name").ParameterValue);
            Assert.IsNotNull(merchant.GetSingleValue("email").ParameterValue);
            Assert.IsNotNull(merchant.GetSingleValue("creation_date").ParameterValue);
            Assert.IsNotNull(merchant.GetSingleValue("status").ParameterValue);
            Assert.IsNull(merchant.GetSingleValue("clabe").ParameterValue);
            Assert.IsNotNull(merchant.GetSingleValue("phone").ParameterValue);
        }

    }
        
}
