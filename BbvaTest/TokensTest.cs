using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Bbva;
using Bbva.Entities;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using Bbva.Entities.Request;

namespace BbvaTest
{
	[TestClass]
	public class TokensTest
	{
		[TestMethod]
		public void TestTokens_Create_Get()
		{
			BbvaAPI bbvaAPI = new BbvaAPI(Constants.API_KEY, Constants.MERCHANT_ID);
            List<IParameter> request = new List<IParameter>{
                new SingleParameter("holder_name", "Juan Perez Ramirez"),
                new SingleParameter("card_number", "4111111111111111"),
                new SingleParameter("cvv2", "022"),
                new SingleParameter("expiration_month", "12"),
                new SingleParameter("expiration_year", "20"),
            };

            Dictionary<String, Object> tokenDictionary = bbvaAPI.TokenService.Create(request);
            ParameterContainer token = new ParameterContainer("token", tokenDictionary);
            Assert.IsNotNull(token);
            String tokenId = token.GetSingleValue("id").ParameterValue;
            Assert.IsNotNull(tokenId);
            Assert.IsNotNull(token.GetContainerValue("card"));

            tokenDictionary = bbvaAPI.TokenService.Get(tokenId);
            token = new ParameterContainer("token", tokenDictionary);
			Assert.IsNotNull(token);
            Assert.AreEqual(tokenId, token.GetSingleValue("id").ParameterValue);
		}

		private string GetResponseAsString(WebResponse response)
		{
			using (StreamReader sr = new StreamReader(response.GetResponseStream(), Encoding.UTF8))
			{
				return sr.ReadToEnd();
			}
		}
	}
}

