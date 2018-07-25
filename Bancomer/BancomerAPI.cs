using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bancomer
{
    public class BancomerAPI
    {  
        public ChargeTokenService ChargeTokenService { get; internal set; }

        public MerchantService MerchantService { get; set; }

        public TokenService TokenService { get; set; }

        private BancomerHttpClient httpClient;

        public BancomerAPI( string api_key, string merchant_id,bool production = false)
        {
            this.httpClient = new BancomerHttpClient(api_key, merchant_id, production);
            ChargeTokenService = new ChargeTokenService(this.httpClient);
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
