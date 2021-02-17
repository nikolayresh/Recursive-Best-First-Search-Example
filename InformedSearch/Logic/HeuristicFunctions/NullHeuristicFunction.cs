namespace InformedSearch.Logic.HeuristicFunctions
{
    public class NullHeuristicFunction : IHeuristicFunction
    {
        public int Evaluate(Node node)
        {
            return 0;
        }
    }
}