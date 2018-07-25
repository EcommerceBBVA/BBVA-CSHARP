using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Bancomer.Entities.Request
{
    [JsonObject(MemberSerialization.OptIn)]
    public class ChargeTokenRequest : JsonObject
    {
         public ChargeTokenRequest()
         {
             Capture = "TRUE";
         }

        [JsonProperty(PropertyName = "affiliation_bbva")]
        public String AffiliationBbva { set; get; }

		[JsonProperty(PropertyName = "amount")]
        public Decimal Amount { set; get; }

		[JsonProperty(PropertyName = "currency", NullValueHandling=NullValueHandling.Ignore)]
		public String Currency { set; get; }

		[JsonProperty(PropertyName = "order_id", NullValueHandling=NullValueHandling.Ignore)]
        public String OrderId { set; get; }

        [JsonProperty(PropertyName = "description")]
        public String Description { set; get; }

		[JsonProperty(PropertyName = "customer")]
        public Customer Customer { set; get; }

		[JsonProperty(PropertyName = "customer_language")]
        public String CustomerLanguage { set; get; }

		[JsonProperty(PropertyName = "payment_plan", NullValueHandling=NullValueHandling.Ignore)]
        public PaymentPlan PaymentPlan { set; get; }

		[JsonProperty(PropertyName = "redirect_url", NullValueHandling = NullValueHandling.Ignore)]
		public String RedirectUrl { set; get; }

		[JsonProperty(PropertyName = "use_card_points", NullValueHandling=NullValueHandling.Ignore)]
		public String UseCardPoints { set; get; }

		[JsonProperty(PropertyName = "use_3d_secure", NullValueHandling = NullValueHandling.Ignore)]
		public Boolean Use3DSecure { set; get; }

        [JsonProperty(PropertyName = "token")]
        public String Token { set; get; }

		[JsonProperty(PropertyName = "metadata", NullValueHandling=NullValueHandling.Ignore)]
		public Dictionary<String, String> Metadata { set; get; }

		[JsonProperty(PropertyName = "capture", NullValueHandling=NullValueHandling.Ignore)]
		public String Capture { set; get; }
	}
}
