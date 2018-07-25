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
            Merchant merchant = bancomerAPI.MerchantService.Get();
            Assert.IsNotNull(merchant);
            Assert.IsNotNull(merchant.Name);
            Assert.IsNotNull(merchant.Email);
            Assert.IsNotNull(merchant.CreationDate);
            Assert.IsNotNull(merchant.Status);
            Assert.IsNull(merchant.CLABE);
            Assert.IsNotNull(merchant.Phone);
            Assert.IsTrue(merchant.Balance.CompareTo(1000.00M) > 0);
            Assert.IsTrue(merchant.AvailableFunds.CompareTo(1000.00M) > 0);
        }

    }
        
}
