namespace DecisionTreeWebAPI
{
    public class EvaluationStoppedException : Exception
    {
        public EvaluationStoppedException()
        {
        }

        public EvaluationStoppedException(string message)
            : base(message)
        {
        }

        public EvaluationStoppedException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
