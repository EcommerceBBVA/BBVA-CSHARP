using System;

namespace Bbva.Entities.Request
{
    public class SingleParameter : IParameter
    {
        public String ParameterValue { set; get; }
        public String ParameterName { set; get; }

        public SingleParameter(String name, String value)
        {
            ParameterName = name;
            ParameterValue = value;
        }
    }
}
