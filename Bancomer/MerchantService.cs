using Bancomer.Entities;
using Bancomer.Entities.Request;
using Bancomer.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bancomer
{
    public class MerchantService : BancomerResourceService<Merchant, Merchant>
    {

        public MerchantService(string api_key, string merchant_id, bool production = false)
            : base(api_key, merchant_id, production)
        {
            ResourceName = "";
        }

        internal MerchantService(BancomerHttpClient opHttpClient)
            : base(opHttpClient)
        {
            ResourceName = "";
        }

        public Merchant Get()
        {
            return base.Get(null, null);
        }
       
    }
}
