using Bbva.Entities;
using Bbva.Entities.Request;
using Bbva.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bbva
{
    public class MerchantService : BbvaResourceService<Merchant, Dictionary<String, Object>>
    {

        public MerchantService(string api_key, string merchant_id, bool production = false)
            : base(api_key, merchant_id, production)
        {
            ResourceName = "";
        }

        internal MerchantService(BbvaHttpClient opHttpClient)
            : base(opHttpClient)
        {
            ResourceName = "";
        }

        public Dictionary<String, Object> Get()
        {
            return base.Get(null, null);
        }
       
    }
}
