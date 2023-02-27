namespace DecisionTreeWebAPI
{
    public class EvaluateOrderReq
    {
        public List<ExpressionDefinition> ExpressionDefinitions { get; set; }
        public List<Variable> GlobalVariables { get; set; } = new List<Variable>();
    }
}
