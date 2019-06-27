using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace Bbva.Entities
{
    [JsonObject(MemberSerialization.OptIn)]
    public class JsonObject
    {
        public string ToJson()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
