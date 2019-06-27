using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bbva.Entities
{
    public class Token : BbvaResourceObject
    {
        [JsonProperty(PropertyName = "card")]
        public Card Card { get; set; }
    }
}
