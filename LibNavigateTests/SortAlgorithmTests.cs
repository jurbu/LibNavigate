using LibNavigate.Algorithm;
using LibNavigate.Iterator;
using LibNavigate.Iterator.Extend;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace LibNavigateTests
{
    [TestClass()]
    public class SortAlgorithmTests
    {
        [TestMethod()]
        public void IsSortedTest()
        {
            int[] data = { 1, 2, 3, 4, 5 };

            using (IInputIterator<int> inputIterator =new InputIterator<int>(data))
            {
                Assert.IsTrue(Algorithm.IsSorted(inputIterator));
            }
        }

        [TestMethod()]
        public void IsSortedTest2()
        {
            int[] data = { 1, 2, 3, 4, 5, 2 };

            using (IInputIterator<int> inputIterator = new InputIterator<int>(data))
            {
                Assert.IsFalse(Algorithm.IsSorted(inputIterator));
            }
        }

        [TestMethod()]
        public void IsSortedTest3()
        {
            int[] data = { 1, 5, 2, 3, 4, 5 };

            using (IInputIterator<int> inputIterator = new InputIterator<int>(data))
            {
                Assert.IsFalse(Algorithm.IsSorted(inputIterator));
            }
        }

        [TestMethod()]
        public void IsSortedTest4()
        {
            int[] data = { 5, 5, 4, 3, 2, 1 };

            using (IInputIterator<int> inputIterator = new InputIterator<int>(data))
            {
                //test whether the data is sorted in descending order

                Assert.IsTrue(Algorithm.IsSorted(inputIterator, new DescendingComparer()));
            }
        }

        [TestMethod()]
        public void IsSortedUntilTest()
        {
            int[] data = { 1, 2, 3, 4, 5 };

            using (IInputIterator<int> inputIterator = new InputIterator<int>(data))
            {
                Assert.IsTrue(Algorithm.IsSortedUntil(inputIterator, 5));
            }
        }

        [TestMethod()]
        public void IsSortedUntilTest2()
        {
            int[] data = { 1, 2, 5, 7, 5 };

            using (IInputIterator<int> inputIterator = new InputIterator<int>(data))
            {
                Assert.IsTrue(Algorithm.IsSortedUntil(inputIterator, 2));
            }
        }

        [TestMethod()]
        public void IsSortedUntilTest3()
        {
            int[] data = { 6, 2, 5, 7, 5 };

            using (IInputIterator<int> inputIterator = new InputIterator<int>(data))
            {
                Assert.IsFalse(Algorithm.IsSortedUntil(inputIterator, 1));
            }
        }

        [TestMethod()]
        public void SortTest()
        {
            int[] data = { 6, 1, 3, 5, 7, 2, 4 };

            IList<int> lst = new List<int>();

            int expectedCount = 7;

            using (IForwardIterator<int> inputIterator=new ForwardIterator<int>(data))
            {
                Algorithm.Sort(inputIterator);

                using (IOutputIterator<int> outputIterator = new BackInsertIterator<int>(lst))
                {
                    inputIterator.Begin();

                    Algorithm.Copy(inputIterator, outputIterator);
                }

                int actualCount = lst.Count();

                Assert.IsTrue(expectedCount==actualCount);

            }

            bool isCorrectData = (lst[0] == 1 && lst[1] == 2 &&
                lst[2] == 3 && lst[3] == 4 &&
                lst[4] == 5 && lst[5]==6 &&
                lst[6] == 7);

            Assert.IsTrue(isCorrectData);

        }

        [TestMethod()]
        public void SortTest2()
        {
            int[] data = { 6, 1, 3, 5, 7, 2, 4 };

            IList<int> lst = new List<int>();

            int expectedCount = 7;

            using (IForwardIterator<int> inputIterator = new ForwardIterator<int>(data))
            {
                Algorithm.Sort(inputIterator, new DescendingComparer());

                using (IOutputIterator<int> outputIterator = new BackInsertIterator<int>(lst))
                {
                    inputIterator.Begin();

                    Algorithm.Copy(inputIterator, outputIterator);
                }

                int actualCount = lst.Count();

                Assert.IsTrue(expectedCount == actualCount);

            }

            //test whether the data is sorted into descending order

            bool isCorrectData = (lst[0] == 7 && lst[1] == 6 &&
                lst[2] == 5 && lst[3] == 4 &&
                lst[4] == 3 && lst[5] == 2 &&
                lst[6] == 1);

            Assert.IsTrue(isCorrectData);

        }

    }
}
