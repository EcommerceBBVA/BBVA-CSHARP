using Bancomer.Entities;
using Bancomer.Entities.Request;
using Bancomer.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bancomer
{
    public class MerchantService : BancomerResourceService<Merchant, Dictionary<String, Object>>
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

        public Dictionary<String, Object> Get()
        {
            return base.Get(null, null);
        }
       
    }
}
