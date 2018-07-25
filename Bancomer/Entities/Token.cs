using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bancomer.Entities
{
    public class Token : BancomerResourceObject
    {
        [JsonProperty(PropertyName = "card")]
        public Card Card { get; set; }
    }
}
