namespace InformedSearch.Logic
{
    /// <summary>
    /// A single state of a problem
    /// </summary>
    public sealed class ProblemState
    {
        public ProblemState(int smallBucketCapacity, int bigBucketCapacity)
        {
            SmallBucket = new Bucket(smallBucketCapacity);
            BigBucket = new Bucket(bigBucketCapacity);
        }

        public ProblemState(ProblemState state)
        {
            SmallBucket = state.SmallBucket.Clone();
            BigBucket = state.BigBucket.Clone();
        }

        /// <summary>
        /// Returns the small bucket of water
        /// </summary>
        public Bucket SmallBucket
        {
            get;
        }

        /// <summary>
        /// Returns the big bucket of water
        /// </summary>
        public Bucket BigBucket
        {
            get;
        }

        public override bool Equals(object obj)
        {
            if (obj == null || obj.GetType() != GetType())
            {
                return false;
            }

            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            ProblemState otherState = (ProblemState) obj;

            return
                Equals(SmallBucket, otherState.SmallBucket) &&
                Equals(BigBucket, otherState.BigBucket);
        }

        public override int GetHashCode()
        {
            return HashUtility.GetHashCode(SmallBucket, BigBucket);
        }

        public override string ToString()
        {
            return $"({SmallBucket.GetVolume()},{BigBucket.GetVolume()})";
        }
    }
}