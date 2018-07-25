using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Bancomer;
using Bancomer.Entities;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using Bancomer.Entities.Request;

namespace BancomerTest
{
	[TestClass]
	public class TokensTest
	{
		[TestMethod]
		public void TestTokens_Create_Get()
		{
			BancomerAPI bancomerAPI = new BancomerAPI(Constants.API_KEY, Constants.MERCHANT_ID);
            List<IParameter> request = new List<IParameter>{
                new SingleParameter("holder_nane", "Juan Perez Ramirez"),
                new SingleParameter("card_number", "4111111111111111"),
                new SingleParameter("cvv2", "022"),
                new SingleParameter("expiration_month", "12"),
                new SingleParameter("expiration_year", "20"),
            };

            Token tokenCreated = bancomerAPI.TokenService.Create(request);
			Assert.IsNotNull(tokenCreated.Id);

			Token tokenGet = bancomerAPI.TokenService.Get(tokenCreated.Id);
			Assert.IsNotNull(tokenGet.Id);
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

