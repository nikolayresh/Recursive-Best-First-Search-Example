using System;
using System.Collections.Generic;
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

            Node root = new Node(problem.GetInitialState());
            SearchResult sr = Rbfs(problem, root, Infinity);

            return sr.GoalNode;
        }

        /// <summary>
        /// Implementation of the RBFS algorithm
        /// </summary>
        private SearchResult Rbfs(Problem problem, Node node, double fLimit)
        {
            if (problem.IsGoalState(node.State))
            {
                return new SearchResult(node, fLimit);
            }

            HashSet<Node> successorNodes = node.Expand();
            if (successorNodes.Count == 0)
            {
                return new SearchResult(null, Infinity);
            }

            double nodeF = EvaluateFValue(node);
            double[] fList = new double[successorNodes.Count];

            for (int i = 0; i < successorNodes.Count; i++)
            {
                Node nextSuccessor = successorNodes.ElementAt(i);
                fList[i] = Math.Max(EvaluateFValue(nextSuccessor), nodeF);
            }

            while (true)
            {
                int bestIndex = GetBestFValueIndex(fList);
                Node bestSuccessor = successorNodes.ElementAt(bestIndex);

                if (fList[bestIndex] > fLimit)
                {
                    return new SearchResult(null, fList[bestIndex]);
                }

                int altIndex = GetAltFValueIndex(fList, bestIndex);

                // RECURSIVE CALL
                SearchResult sr = Rbfs(problem, bestSuccessor, Math.Min(fLimit, fList[altIndex]));
                fList[bestIndex] = sr.FLimit;

                if (sr.GoalNode != null)
                {
                    return sr;
                }
            }
        }

        /// <summary>
        /// Calculates F-value component of a specified node
        /// </summary>
        private double EvaluateFValue(Node node)
        {
            return node.GetLevel() + _heuristicFn.Evaluate(node);
        }

        private static int GetBestFValueIndex(double[] fList)
        {
            int index = 0;
            double minValue = Infinity;

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
            int index = bestIndex;
            double minValue = Infinity;

            for (int i = 0; i < fList.Length; i++)
            {
                if (i != bestIndex && fList[i] < minValue)
                {
                    minValue = fList[i];
                    index = i;
                }
            }

            return index;
        }
    }
}