using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Xml.Linq;

namespace DecisionTreeWebAPI
{
    public class EvaluateOrderCsharpTreeReq
    {
        public List<ExpressionDefinition> ExpressionDefinitions { get; set; }
        public List<string> Imports { get; set; }
        public bool IsLastNode { get; set; }
        public EvaluateOrderCsharpTreeReq LeftNode { get; set; }
        public EvaluateOrderCsharpTreeReq RightNode { get; set; }
    }
}
