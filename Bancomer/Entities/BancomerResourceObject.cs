using Newtonsoft.Json;
using System;

namespace Bancomer.Entities
{
    public class BancomerResourceObject : JsonObject
    {
		[JsonProperty(PropertyName = "id", NullValueHandling=NullValueHandling.Ignore)]
        public String Id { get; set; }
    }
}
