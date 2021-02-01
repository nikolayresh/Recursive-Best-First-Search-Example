using System.Diagnostics;

namespace InformedSearch.Logic
{
    /// <summary>
    /// Bucket of water
    /// </summary>
    [DebuggerDisplay("{DebugText,nq}")]
    public sealed class Bucket
    {
        private readonly int _capacity;
        private int _volume;

        public Bucket(int capacity)
        {
            _capacity = capacity;
            _volume = 0;
        }

        public Bucket(Bucket bucket)
        {
            _capacity = bucket.GetCapacity();
            _volume = bucket.GetVolume();
        }

        /// <summary>
        /// Returns a boolean value whether this bucket is empty
        /// </summary>
        public bool IsEmpty()
        {
            return Equals(_volume, 0);
        }

        /// <summary>
        /// Returns a boolean value whether this bucket is full (holds max volume)
        /// </summary>
        public bool IsFull()
        {
            return Equals(_volume, _capacity);
        }

        /// <summary>
        /// Fills up this bucket with water til capacity
        /// </summary>
        public void FillUp()
        {
            _volume = _capacity;
        }

        /// <summary>
        /// Clears water from this bucket
        /// </summary>
        public void DumpWater()
        {
            _volume = 0;
        }

        /// <summary>
        /// Returns current volume of this bucket in liters
        /// </summary>
        public int GetVolume()
        {
            return _volume;
        }

        /// <summary>
        /// Returns maximal volume in liters this bucket can hold
        /// </summary>
        public int GetCapacity()
        {
            return _capacity;
        }

        /// <summary>
        /// Adds a liter of water into this bucket
        /// </summary>
        public void IncrementVolume()
        {
            if ((_volume + 1) <= _capacity)
            {
                _volume++;
            }
        }

        /// <summary>
        /// Subtracts a liter of water from this bucket
        /// </summary>
        public void DecrementVolume()
        {
            if ((_volume - 1) >= 0)
            {
                _volume--;
            }
        }

        /// <summary>
        /// Pours out water into another bucket
        /// </summary>
        public void PourWaterInto(Bucket bucket)
        {
            while (!bucket.IsFull() && !IsEmpty())
            {
                bucket.IncrementVolume();
                DecrementVolume();
            }
        }

        public Bucket Clone()
        {
            return new Bucket(this);
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

            var otherBucket = (Bucket) obj;

            return
                _volume == otherBucket._volume &&
                _capacity == otherBucket._capacity;
        }

        public override int GetHashCode()
        {
            return HashUtility.GetHashCode(_volume, _capacity);
        }

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private string DebugText
        {
            get
            {
                return $"(Volume: {_volume}; Capacity: {_capacity})";
            }
        }
    }
}