using Newtonsoft.Json;
using Bancomer.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using Bancomer.Entities.Request;

namespace Bancomer
{
    public class BancomerHttpClient
    {
        private static readonly string api_endpoint = "https://api.ecommercebbva.com/v1/";
        private static readonly string api_endpoint_sandbox = "https://sand-api.ecommercebbva.com/v1/";
        private static readonly string user_agent = "Bancomer .NET v1";
        private static readonly Encoding encoding = Encoding.UTF8;
        private Boolean _isProduction = false;

        public int TimeoutSeconds { get; set; }

        public String MerchantId { get; internal set; }

        public String APIEndpoint { get; set; }

        public String APIKey { get; set; }

        public BancomerHttpClient(string api_key, string merchant_id, bool production = false)
        {
            if (String.IsNullOrEmpty(api_endpoint_sandbox))
                throw new ArgumentNullException("api_key");
            if (String.IsNullOrEmpty(merchant_id))
                throw new ArgumentNullException("merchant_id");
            MerchantId = merchant_id;
            APIKey = api_key;
            TimeoutSeconds = 120;
            Production = production;
        }

        public bool Production { 
            get { 
                return _isProduction; 
            } 
            set { 
                APIEndpoint = value ? api_endpoint : api_endpoint_sandbox;
                _isProduction = value;
            } 
        }

        protected virtual WebRequest SetupRequest(string method, string url)
        {
            WebRequest req = (WebRequest)WebRequest.Create(url);
            req.Method = method;
            if (req is HttpWebRequest)
            {
                ((HttpWebRequest)req).UserAgent = user_agent;
            }
            //req.Credentials = credential;
            string authInfo = APIKey + ":";
            authInfo = Convert.ToBase64String(Encoding.Default.GetBytes(authInfo));
            req.Headers["Authorization"] = "Basic " + authInfo;
            req.PreAuthenticate = false;
            req.Timeout = TimeoutSeconds * 1000;
            if (method == "POST" || method == "PUT")
                req.ContentType = "application/json";
            return req;
        }

        protected string GetResponseAsString(WebResponse response)
        {
            using (StreamReader sr = new StreamReader(response.GetResponseStream(), encoding))
            {
                return sr.ReadToEnd();
            }
        }

        public T Post<T>(string endpoint, JsonObject obj)
        {
            var json = DoRequest(endpoint, HttpMethod.POST, obj.ToJson());
            return JsonConvert.DeserializeObject<T>(json);
        }
        // ########## NEW METHOD
        public T Post<T>(string endpoint, List<IParameter> parameters)
        {
            var json = DoRequest(endpoint, HttpMethod.POST, getListAsJson(parameters));
            return JsonConvert.DeserializeObject<T>(json);
        }
        // #########################################################################################
        private String getListAsJson(List<IParameter> parameters)
        {
            var values = new Dictionary<String, object>();
            foreach(IParameter param in parameters)
            {
                var results = typeSingleParameter(param)
                    .Union(typeContainer(param))
                    .ToDictionary(d => d.Key, d => d.Value);
                values = values.Union(results).ToDictionary(d => d.Key, d => d.Value);
            }
            return JsonConvert.SerializeObject(values, Formatting.Indented);
        }
        // ####
        private Dictionary<String, object> typeSingleParameter(IParameter parameter)
        {
            var values = new Dictionary<String, object>();
            if (parameter is SingleParameter)
            {
                SingleParameter singleParameter = (SingleParameter) parameter;
                values.Add(singleParameter.ParameterName, singleParameter.ParameterValue);
            }
            return values;
        }
        // ####
        private Dictionary<String, object> typeContainer(IParameter parameter)
        {
            var finalValues = new Dictionary<String, object>();
            if (parameter is ParameterContainer)
            {
                var values = new Dictionary<String, object>();
                ParameterContainer container = (ParameterContainer) parameter;
                foreach (IParameter param in container.ParameterValues)
                {
                    var results = typeSingleParameter(param)
                        .Union(typeContainer(param))
                        .ToDictionary(d => d.Key, d => d.Value);
                    values = values.Union(results).ToDictionary(d => d.Key, d => d.Value);
                }
                finalValues.Add(container.ParameterName, values);
            }
            return finalValues;
        }
        /*
        private Dictionary<String, object> joinDictionaries(Dictionary<String, object> v1, Dictionary<String, object> v2)
        {
            var values = new Dictionary<String, object>();
        
            values = values.Union(v1).ToDictionary(i => i.Key, i => i.Value);
                foreach (KeyValuePair<String, object> kvp in v2)
                {
                    object value;
                    if (!values.TryGetValue(kvp.Key, out value))
                        values.Add(kvp.Key, kvp.Value);
                    else
                        values[kvp.Key] = kvp.Value;
                }
            return values;
        }*/

        public void Post<T>(string endpoint)
		{
			DoRequest(endpoint, HttpMethod.POST, null);
		}

        public T Get<T>(string endpoint)
        {
            var json = DoRequest(endpoint, HttpMethod.GET, null);
            return JsonConvert.DeserializeObject<T>(json);
        }

        public T Put<T>(string endpoint, JsonObject obj)
        {
            var json = DoRequest(endpoint, HttpMethod.PUT, obj.ToJson());
            return JsonConvert.DeserializeObject<T>(json);
        }

        public void Delete(string endpoint)
        {
            DoRequest(endpoint, HttpMethod.DELETE, null);
        }

        protected virtual string DoRequest(string path, HttpMethod method, string body)
        {
            string result = null;
            string endpoint = APIEndpoint + MerchantId + path;
            Console.WriteLine("Request to: " + endpoint);
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            WebRequest req = SetupRequest(method.ToString(), endpoint);
            if (body != null)
            {
                byte[] bytes = encoding.GetBytes(body.ToString());
                req.ContentLength = bytes.Length;
                using (Stream st = req.GetRequestStream())
                {
                    st.Write(bytes, 0, bytes.Length);
                }
            }

            try
            {
                using (WebResponse resp = (WebResponse)req.GetResponse())
                {
                    result = GetResponseAsString(resp);
                }
            }
            catch (WebException wexc)
            {
                if (wexc.Response != null)
                {
                    string json_error = GetResponseAsString(wexc.Response);
                    HttpStatusCode status_code = HttpStatusCode.BadRequest;
                    HttpWebResponse resp = wexc.Response as HttpWebResponse;
                    if (resp != null)
                        status_code = resp.StatusCode;

                    if ((int)status_code <= 500)
                        throw BancomerException.GetFromJSON(status_code, json_error);
                }
                throw;
            }
            return result;
        }

        public enum HttpMethod
        {
            GET, POST, DELETE, PUT,
        }
    }
}
