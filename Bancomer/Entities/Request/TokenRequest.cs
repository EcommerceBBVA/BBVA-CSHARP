using System;
using Newtonsoft.Json;

namespace Bancomer.Entities.Request
{
    [JsonObject(MemberSerialization.OptIn)]
    public class TokenRequest : JsonObject
    {
        [JsonProperty(PropertyName = "holder_name")]
        public String HolderName { get; set; }

        [JsonProperty(PropertyName = "card_number")]
        public String CardNumber { get; set; }

        [JsonProperty(PropertyName = "cvv2")]
        public String Cvv2 { get; set; }

        [JsonProperty(PropertyName = "expiration_month")]
        public String ExpirationMonth { get; set; }

        [JsonProperty(PropertyName = "expiration_year")]
        public String ExpirationYear { get; set; }

        [JsonProperty(PropertyName = "address", NullValueHandling=NullValueHandling.Ignore)]
        public Address Address { set; get; }

	}
}
