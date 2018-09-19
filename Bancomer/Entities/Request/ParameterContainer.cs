using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Bancomer.Entities.Request
{
    public class ParameterContainer : IParameter
    {
        public List<IParameter> ParameterValues { set; get; }
        public string ParameterName { set; get; }

        public ParameterContainer(String parameterName, List<IParameter> parameterValues)
        {
            ParameterName = parameterName;
            ParameterValues = parameterValues;
        }

        public ParameterContainer(String container)
        {
            ParameterName = container;
            ParameterValues = new List<IParameter>();
        }

        public ParameterContainer(String name, Dictionary<String, Object> dictionary)
        {
            this.ParameterName = name;
            this.ParameterValues = new List<IParameter>();
            foreach (var item in dictionary)
            {
                if (item.Value is JObject finalObj)
                {
                    var value = finalObj.ToObject<Dictionary<String, Object>>();
                    AddMultiValue(new ParameterContainer(item.Key, value));
                } else
                {
                    AddValue(item.Key, (String)item.Value);
                }
            }
        }

        public SingleParameter GetSingleValue(String value)
        {
            foreach (IParameter param in this.ParameterValues)
            {
                if (param.GetType().Name.Equals("SingleParameter") && param.ParameterName.Equals(value))
                    return (SingleParameter) param;
            }
            return null;
        }

        public ParameterContainer GetContainerValue(String value)
        {
            foreach (IParameter param in this.ParameterValues)
            {
                if (param.GetType().Name.Equals("ParameterContainer") && param.ParameterName.Equals(value))
                    return (ParameterContainer) param;
            }
            return null;
        }

        public void AddValue(String name, String value)
        {
            ParameterValues.Add(new SingleParameter(name, value));
        }

        public void AddMultiValue(ParameterContainer multiValue)
        {
            ParameterValues.Add(multiValue);
        }
    }
}