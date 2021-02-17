using System;

namespace InformedSearch.Logic.HeuristicFunctions
{
    public sealed class SimpleHeuristicFunction : IHeuristicFunction
    {
        private readonly Problem _problem;

        public SimpleHeuristicFunction(Problem problem)
        {
            _problem = problem ?? throw new ArgumentNullException(nameof(problem));
        }

        public int Evaluate(Node node)
        { 
            int volumeSmall = node.State.SmallBucket.GetVolume();
            int volumeBig = node.State.BigBucket.GetVolume();
            int volumeGoal = _problem.GetGoalVolume();

            int result = 0;

            if (volumeSmall != volumeGoal)
            {
                result++;
            }

            if (volumeBig != volumeGoal)
            {
                result++;
            }

            return result;
        }
    }
}