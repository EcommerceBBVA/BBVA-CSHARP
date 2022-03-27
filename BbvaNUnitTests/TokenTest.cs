using NUnit.Framework;
using System;
using Bbva;
using Bbva.Entities;
using Bbva.Entities.Request;
using System.Collections.Generic;

namespace BbvaNUnitTests
{
	[TestFixture()]
	public class TokenTest
	{
        
		[Test()]
		public void TestCreateToken()
		{
			Decimal amount = new Decimal(111.11);

			BbvaAPI bbvaAPI = new BbvaAPI(Constants.API_KEY, Constants.MERCHANT_ID);

            List<IParameter> request = new List <IParameter>{
                new SingleParameter("holder_name", "Juan Perez Ramirez"),
                new SingleParameter("card_number", "4111111111111111"),
                new SingleParameter("cvv2", "022"),
                new SingleParameter("expiration_month", "12"),
                new SingleParameter("expiration_year", "25"),
            };

            Dictionary<String, Object> tokenObj = bbvaAPI.TokenService.Create(request);
            ParameterContainer tokenCreated = new ParameterContainer("token", tokenObj);
            Assert.IsNotNull(tokenCreated);
            Assert.IsNotNull(tokenCreated.GetSingleValue("id").ParameterValue);
        }
        
		[Test()]
		public void TestGetToken()
		{
			BbvaAPI bbvaAPI = new BbvaAPI(Constants.API_KEY, Constants.MERCHANT_ID);
            List<IParameter> request = new List<IParameter>{
                new SingleParameter("holder_nane", "Juan Perez Ramirez"),
                new SingleParameter("card_number", "4111111111111111"),
                new SingleParameter("cvv2", "022"),
                new SingleParameter("expiration_month", "12"),
                new SingleParameter("expiration_year", "20"),
            };

            Dictionary<String, Object> tokenObj = bbvaAPI.TokenService.Create(request);
            ParameterContainer tokenCreated = new ParameterContainer("token", tokenObj);
			Assert.IsNotNull(tokenCreated.GetSingleValue("id").ParameterValue);

            Dictionary<String, Object> tokenGet = bbvaAPI.TokenService.Get(tokenCreated.GetSingleValue("id").ParameterValue);
			Assert.IsNotNull(tokenGet);
		}

	}
}
