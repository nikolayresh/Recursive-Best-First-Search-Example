using System;

namespace InformedSearch.Logic
{
    public sealed class Problem
    {
        private readonly ProblemState _initialState;
        private readonly int _smallBucketCapacity;
        private readonly int _bigBucketCapacity;
        private readonly int _goalVolume;

        public Problem(int smallBucketCapacity, int bigBucketCapacity, int goalVolume)
        {
            _smallBucketCapacity = smallBucketCapacity;
            _bigBucketCapacity = bigBucketCapacity;
            _goalVolume = goalVolume;
            _initialState = new ProblemState(smallBucketCapacity, bigBucketCapacity);
        }

        /// <summary>
        /// Returns initial state of a problem with both buckets empty
        /// </summary>
        public ProblemState GetInitialState()
        {
            return _initialState;
        }

        /// <summary>
        /// Returns a boolean value whether this problem has solution
        /// </summary>
        public bool IsSolvable()
        {
            return _goalVolume <= Math.Max(_smallBucketCapacity, _bigBucketCapacity);
        }

        /// <summary>
        /// Returns value of volume to reach
        /// </summary>
        /// <returns></returns>
        public int GetGoalVolume()
        {
            return _goalVolume;
        }

        /// <summary>
        /// Returns a boolean value whether specified state is the goal state
        /// </summary>
        public bool IsGoalState(ProblemState state)
        {
            if (Equals(state.SmallBucket.GetVolume(), _goalVolume))
            {
                return true;
            }

            if (Equals(state.BigBucket.GetVolume(), _goalVolume))
            {
                return true;
            }

            return false;
        }
    }
}