using NUnit.Framework;
using System;
using Bancomer;
using Bancomer.Entities;
using Bancomer.Entities.Request;
using System.Collections.Generic;

namespace BancomerNUnitTests
{
	[TestFixture()]
	public class TokenTest
	{
        
		[Test()]
		public void TestCreateToken()
		{
			Decimal amount = new Decimal(111.11);

			BancomerAPI bancomerAPI = new BancomerAPI(Constants.API_KEY, Constants.MERCHANT_ID);

            List<IParameter> request = new List <IParameter>{
                new SingleParameter("holder_nane", "Juan Perez Ramirez"),
                new SingleParameter("card_number", "4111111111111111"),
                new SingleParameter("cvv2", "022"),
                new SingleParameter("expiration_month", "12"),
                new SingleParameter("expiration_year", "20"),
            };

            Dictionary<String, Object> tokenObj = bancomerAPI.TokenService.Create(request);
            ParameterContainer tokenCreated = new ParameterContainer("token", tokenObj);
            Assert.IsNotNull(tokenCreated);
            Assert.IsNotNull(tokenCreated.GetSingleValue("id").ParameterValue);
        }
        
		[Test()]
		public void TestGetToken()
		{
			BancomerAPI bancomerAPI = new BancomerAPI(Constants.API_KEY, Constants.MERCHANT_ID);
            List<IParameter> request = new List<IParameter>{
                new SingleParameter("holder_nane", "Juan Perez Ramirez"),
                new SingleParameter("card_number", "4111111111111111"),
                new SingleParameter("cvv2", "022"),
                new SingleParameter("expiration_month", "12"),
                new SingleParameter("expiration_year", "20"),
            };

            Dictionary<String, Object> tokenObj = bancomerAPI.TokenService.Create(request);
            ParameterContainer tokenCreated = new ParameterContainer("token", tokenObj);
			Assert.IsNotNull(tokenCreated.GetSingleValue("id").ParameterValue);

            Dictionary<String, Object> tokenGet = bancomerAPI.TokenService.Get(tokenCreated.GetSingleValue("id").ParameterValue);
			Assert.IsNotNull(tokenGet);
		}

	}
}