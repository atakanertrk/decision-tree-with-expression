using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DecisionTreeWebAPI
{
    public class ExpressionDefinition
    {
        public string ExpressionName { get; set; }
        public int ExecuteOrder { get; set; }
        public string Expression { get; set; }
    }

    public class Variable
    {
        public string Name { get; set; }
        public object Value { get; set; }
        public ValueType ValueType { get; set; }
    }

    public enum ValueType
    {
        Number,
        String,
        Date
    }
}
