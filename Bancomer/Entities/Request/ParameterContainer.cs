using System;
using System.Collections.Generic;

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

        public void addValue(String name, String value)
        {
            ParameterValues.Add(new SingleParameter(name, value));
        }

        public void addMultiValue(ParameterContainer multiValue)
        {
            ParameterValues.Add(multiValue);
        }
    }
}