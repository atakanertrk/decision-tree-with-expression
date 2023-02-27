namespace DecisionTreeWebAPI
{
    public static class CustomMethods
    {
        public static decimal Sum(params decimal[] args)
        {
            decimal sum = 0;

            foreach (decimal i in args)
                sum += i;

            return sum;
        }

        public static decimal Condition(bool condition, decimal whileTrue, decimal whileFlase)
        {
            if (condition)
            {
                return whileTrue;
            }
            return whileFlase;
        }

        public static DateTime GetDate()
        {
            return DateTime.Now;
        }

        public static void Stop()
        {
            throw new EvaluationStoppedException();
        }

    }
}
