using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bbva
{
    public class BbvaAPI
    {  
        public ChargeService ChargeService { get; internal set; }

        public MerchantService MerchantService { get; set; }

        public TokenService TokenService { get; set; }

        private BbvaHttpClient httpClient;

        public BbvaAPI( string api_key, string merchant_id,bool production = false)
        {
            this.httpClient = new BbvaHttpClient(api_key, merchant_id, production);
            ChargeService = new ChargeService(this.httpClient);
            MerchantService = new MerchantService (this.httpClient);
            TokenService = new TokenService (this.httpClient);
        }

        public bool Production {
            get
            {
                return this.httpClient.Production;
            }
            set
            {
                this.httpClient.Production = value;
            }
        }
    }
}
