using System;
using Bbva.Entities;
using Bbva.Entities.Request;
using Bbva.Utils;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bbva
{
    public class CustomerService : BbvaResourceService<Customer, Dictionary<String, Object>>
    {

        public CustomerService(string api_key, string merchant_id, bool production = false)
            : base(api_key, merchant_id, production)
        {
            ResourceName = "customers";
        }

        internal CustomerService(BbvaHttpClient opHttpClient)
            : base(opHttpClient)
        {
            ResourceName = "customers";
        }

        public Dictionary<String, Object> Create(Customer customer)
        {
            return base.Create("", customer);
        }

        //public Customer Update(Customer customer)
        //{
        //    return base.Update(null, customer);
        //}

        public void Delete(string customer_id)
        {
            base.Delete(null, customer_id);
        }

        public Dictionary<String, Object> Get(string customer_id)
        {
            return base.Get(null, customer_id);
        }

        public List<Dictionary<String, Object>> List(SearchParams filters = null)
        {
            return base.List(null, filters);
        }
    }
}
