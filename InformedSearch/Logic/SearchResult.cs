namespace InformedSearch.Logic
{
    public sealed class SearchResult
    {
        public SearchResult(Node goalNode, double fLimit)
        {
            GoalNode = goalNode;
            FLimit = fLimit;
        }

        public Node GoalNode
        {
            get;
        }

        public double FLimit
        {
            get;
        }
    }
}