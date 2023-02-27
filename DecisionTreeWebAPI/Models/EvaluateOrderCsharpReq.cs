namespace DecisionTreeWebAPI
{
    public class EvaluateOrderCsharpReq
    {
        public List<ExpressionDefinition> ExpressionDefinitions { get; set; }
        public List<string> Imports { get; set; }
        public List<string> Execute { get; set; }
        public string OutputExpression { get; set; }
    }
}
