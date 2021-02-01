using System;

namespace InformedSearch.Logic
{
    public sealed class SimpleHeuristicFunction : IHeuristicFunction
    {
        private readonly Problem _problem;

        public SimpleHeuristicFunction(Problem problem)
        {
            _problem = problem ?? throw new ArgumentNullException(nameof(problem));
        }

        public double Evaluate(Node node)
        {
            var sVolume = node.State.SmallBucket.GetVolume();
            var bVolume = node.State.BigBucket.GetVolume();
            var goalVolume = _problem.GetGoalVolume();

            var result = 0;
            if (sVolume != goalVolume && bVolume != goalVolume)
            {
                result++;
            }

            return result;
        }
    }
}