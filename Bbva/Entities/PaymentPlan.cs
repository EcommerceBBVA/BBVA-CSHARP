using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bbva.Entities
{
    public class PaymentPlan
    {
		[JsonProperty(PropertyName = "payments")]
        public int Payments { get; set; }

        [JsonProperty(PropertyName = "payments_type")]
        public String PaymentsType { get; set; }

        [JsonProperty(PropertyName = "deferred_months")]
        public int DeferredMonths { get; set; }
    }
}
