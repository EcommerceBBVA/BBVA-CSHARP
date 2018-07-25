using Newtonsoft.Json;
using System;

namespace Bancomer.Entities
{
    [JsonObject(MemberSerialization.OptIn)]
    public class Store
    {
        [JsonProperty(PropertyName = "reference")]
        public String Reference { get; set; }

        [JsonProperty(PropertyName = "barcode_url")]
        public String BarcodeURL { get; set; }
    }
}