using System.Collections.Generic;

namespace LibNavigateTests
{
    public sealed class DescendingComparer : IComparer<int>
    {
        public int Compare(int x, int y)
        {
            if (y < x)
            {
                return -1;
            }
            else if (y > x)
            {
                return 1;
            }

            return 0;
        }
    }
}
