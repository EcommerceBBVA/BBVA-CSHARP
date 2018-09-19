using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bancomer.Entities;
using Bancomer.Entities.Request;

namespace Bancomer
{
    public class ChargeService : BancomerResourceService<ChargeTokenRequest, Dictionary<String, Object>>
    {

        public ChargeService(string api_key, string merchant_id, bool production = false)
            : base(api_key, merchant_id, production)
        {
            ResourceName = "charges";
        }

        internal ChargeService(BancomerHttpClient opHttpClient)
            : base(opHttpClient)
        {
            ResourceName = "charges";
        }

        public Dictionary<String, Object> Create(List<IParameter> charge_request)
        {
            return base.Save(null, charge_request);
        }

        public Dictionary<String, Object> Capture(string charge_id, Decimal? amount)
        {
            return this.Capture(null, charge_id, amount);
        }

        public Dictionary<String, Object> Capture(string customer_id , string charge_id, Decimal? amount)
        {
            if (charge_id == null)
                throw new ArgumentNullException("charge_id cannot be null");
            string ep = GetEndPoint(customer_id, charge_id) + "/capture";
            CaptureRequest request = new CaptureRequest();
            request.Amount = amount;
            return this.httpClient.Post <Dictionary<String, Object>>(ep, request);
        }

		public Dictionary<String, Object> Refund(string charge_id, string description, Decimal? amount)
		{
			return this.Refund(null, charge_id, description, amount);
		}

        public Dictionary<String, Object> Refund(string customer_id, string charge_id, string description, Decimal? amount)
		{
			if (charge_id == null)
				throw new ArgumentNullException("charge_id cannot be null");
			string ep = GetEndPoint(customer_id, charge_id) + "/refund";
			RefundRequest request = new RefundRequest();
			request.Description = description;

			if (amount != null)
				request.Amount = amount;
			
			return this.httpClient.Post<Dictionary<String, Object>>(ep, request);
		}

        public Dictionary<String, Object> Get(string charge_id)
        {
            return base.Get(null, charge_id);
        }
      
    }
}
