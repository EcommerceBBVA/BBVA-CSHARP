using Newtonsoft.Json;
using System;

namespace Bbva.Entities
{
    public class Affiliation
    {
        [JsonProperty(PropertyName = "number", NullValueHandling=NullValueHandling.Ignore)]
        public String Number { get; set; }

        [JsonProperty(PropertyName = "name", NullValueHandling=NullValueHandling.Ignore)]
        public String Name { get; set; }

        [JsonProperty(PropertyName = "merchant_name", NullValueHandling=NullValueHandling.Ignore)]
        public String MerchantName { get; set; }

    }
}
