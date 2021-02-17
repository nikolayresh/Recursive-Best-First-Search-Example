namespace InformedSearch.Logic
{
    /// <summary>
    /// Abstraction that defines heuristic function of a problem node
    /// </summary>
    public interface IHeuristicFunction
    {
        int Evaluate(Node node);
    }
}