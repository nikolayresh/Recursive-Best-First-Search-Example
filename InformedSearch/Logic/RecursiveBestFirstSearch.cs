using System;
using System.Linq;

namespace InformedSearch.Logic
{
    /// <summary>
    /// Class that defines algorithm of Recursive Best-First Search
    /// </summary>
    public sealed class RecursiveBestFirstSearch
    {
        private const uint Infinity = uint.MaxValue;

        private readonly IHeuristicFunction _heuristicFn;

        public RecursiveBestFirstSearch(IHeuristicFunction heuristicFn)
        {
            _heuristicFn = heuristicFn ?? throw new ArgumentNullException(nameof(heuristicFn));
        }

        /// <summary>
        /// Returns a goal node if search succeeded, otherwise returns null in case of failure
        /// </summary>
        public Node ExecuteSearch(Problem problem)
        {
            if (problem == null)
            {
                throw new ArgumentNullException(nameof(problem));
            }

            if (!problem.IsSolvable())
            {
                // problem has no solution
                return null;
            }

            var root = new Node(problem.GetInitialState());
            var sr = Rbfs(problem, root, Infinity);

            return sr.GoalNode;
        }

        private SearchResult Rbfs(Problem problem, Node node, double fLimit)
        {
            if (problem.IsGoalState(node.State))
            {
                return new SearchResult(node, fLimit);
            }

            var successors = node.Expand();

            if (successors.Count == 0)
            {
                return new SearchResult(null, Infinity);
            }

            var nodeF = EvaluateFValue(node);
            var fList = new double[successors.Count];

            for (var i = 0; i < successors.Count; i++)
            {
                var nextSuccessor = successors.ElementAt(i);
                fList[i] = Math.Max(EvaluateFValue(nextSuccessor), nodeF);
            }

            while (true)
            {
                var bestIndex = GetBestFValueIndex(fList);
                var bestSuccessor = successors.ElementAt(bestIndex);

                if (fList[bestIndex] > fLimit)
                {
                    return new SearchResult(null, fList[bestIndex]);
                }

                var altIndex = GetAltFValueIndex(fList, bestIndex);
                var sr = Rbfs(problem, bestSuccessor, Math.Min(fLimit, fList[altIndex]));

                fList[bestIndex] = sr.FLimit;

                if (sr.GoalNode != null)
                {
                    return sr;
                }
            }
        }

        private double EvaluateFValue(Node node)
        {
            return node.GetLevel() + _heuristicFn.Evaluate(node);
        }

        private static int GetBestFValueIndex(double[] fList)
        {
            var index = 0;
            var minValue = (double) Infinity;

            for (int i = 0; i < fList.Length; i++)
            {
                if (fList[i] < minValue)
                {
                    minValue = fList[i];
                    index = i;
                }
            }

            return index;
        }

        private static int GetAltFValueIndex(double[] fList, int bestIndex)
        {
            var index = bestIndex;
            var minValue = (double) Infinity;

            for (int i = 0; i < fList.Length; i++)
            {
                if (fList[i] < minValue && i != bestIndex)
                {
                    minValue = fList[i];
                    index = i;
                }
            }

            return index;
        }
    }
}