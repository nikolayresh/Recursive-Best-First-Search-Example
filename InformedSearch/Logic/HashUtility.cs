namespace InformedSearch.Logic
{
    /// <summary>
    /// Utility that calculates hash code of specified items
    /// </summary>
    internal static class HashUtility
    {
        private const int SeedPrime = 17;
        private const int NextPrime = 41;

        public static int GetHashCode(params object[] items)
        {
            unchecked
            {
                int hash = SeedPrime;

                if (items != null && items.Length > 0)
                {
                    for (int i = 0; i < items.Length; i++)
                    {
                        object item = items[i];
                        hash = hash * NextPrime + (item?.GetHashCode() ?? 0);
                    }
                }

                return hash;
            }
        }
    }
}