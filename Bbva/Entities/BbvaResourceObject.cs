using Newtonsoft.Json;
using System;

namespace Bbva.Entities
{
    public class BbvaResourceObject : JsonObject
    {
		[JsonProperty(PropertyName = "id", NullValueHandling=NullValueHandling.Ignore)]
        public String Id { get; set; }
    }
}
