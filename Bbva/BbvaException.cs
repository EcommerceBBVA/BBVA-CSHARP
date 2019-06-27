using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace Bbva
{
    [JsonObject(MemberSerialization.OptIn)]
    public class BbvaException : Exception
    {
        [JsonConstructor]
        internal BbvaException()
        {
        }

        internal static BbvaException GetFromJSON(HttpStatusCode code, string json)
        {
            BbvaException result = JsonConvert.DeserializeObject<BbvaException>(json);
            result.StatusCode = code;
            return result;
        }

        [JsonProperty(PropertyName = "description")]
        public String Description { get; set; }

        [JsonProperty(PropertyName = "category")]
        public String Category { get; set; }

        [JsonProperty(PropertyName = "request_id")]
        public String RequestId { get; set; }

        [JsonProperty(PropertyName = "error_code")]
        public int ErrorCode { get; set; }

        public HttpStatusCode StatusCode { get; internal set; }

        public override string Message
        {
            get
            {
                return this.ErrorCode + ": " + this.Description;
            }
        }
    }
}
