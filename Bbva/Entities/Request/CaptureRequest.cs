using System;
using Newtonsoft.Json;

namespace Bbva.Entities.Request
{
    [JsonObject(MemberSerialization.OptIn)]
    internal class CaptureRequest : JsonObject
    {
        [JsonProperty(PropertyName = "amount")]
        public Decimal? Amount { set; get; }
    }
}
