using Bancomer.Entities;
using Bancomer.Entities.Request;
using Bancomer.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bancomer
{
    public abstract class BancomerGenericService
    {
        protected BancomerHttpClient httpClient;

        // private static readonly string filter_date_format = "yyyy-MM-dd";

        // private static readonly string filter_amount_format = "0.00";

		public BancomerGenericService(string api_key, string merchant_id, bool production = false)
        {
            this.httpClient = new BancomerHttpClient(api_key, merchant_id, production);
        }

		internal BancomerGenericService(BancomerHttpClient opHttpClient)
        {
            this.httpClient = opHttpClient;
        }
			
        protected virtual T Get<T>(string ep)
        {
            return this.httpClient.Get<T>(ep);
        }

        protected List<T> List<T>(string url)
        {
            return this.httpClient.Get<List<T>>(url);
        }
    }
}
