using LibNavigate.Algorithm;
using LibNavigate.Iterator;
using LibNavigate.Iterator.Extend;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using static LibNavigate.Function.Function;

namespace LibNavigateTests
{
    [TestClass()]
    public class PartitionAlgorithmTests
    {

        [TestMethod()]
        public void IsPartitionedTest()
        {
            int[] data = { 2, 4, 6 };

            UnaryPredicate<int> isEven = delegate (int x)
            {
                return x % 2 == 0;
            };

            using (IInputIterator<int> inputIterator = new InputIterator<int>(data))
            {
                Assert.IsTrue(Algorithm.IsPartitioned(inputIterator, isEven));
            }
        }

        [TestMethod()]
        public void IsPartitionedTest2()
        {
            int[] data = { 2, 4, 6, 7 };

            UnaryPredicate<int> isEven = delegate (int x)
            {
                return x % 2 == 0;
            };

            using (IInputIterator<int> inputIterator = new InputIterator<int>(data))
            {
                Assert.IsTrue(Algorithm.IsPartitioned(inputIterator, isEven));
            }
        }

        [TestMethod()]
        public void IsPartitionedTest3()
        {
            int[] data = { 2, 7, 4, 6 };

            UnaryPredicate<int> isEven = delegate (int x)
            {
                return x % 2 == 0;
            };

            using (IInputIterator<int> inputIterator = new InputIterator<int>(data))
            {
                Assert.IsFalse(Algorithm.IsPartitioned(inputIterator, isEven));
            }
        }

        [TestMethod()]
        public void PartitionTest()
        {
            int[] data = { 0, 1, 2, 3 };

            UnaryPredicate<int> isEven = delegate (int x)
            {
                return x % 2 == 0;
            };

            using (ForwardIterator<int> inputIterator=new ForwardIterator<int>(data))
            {
                Algorithm.Partition(inputIterator, isEven);

                bool isCorrectData = (inputIterator[0]==0 && 
                    inputIterator[1]==2 && 
                    inputIterator[2]==1 && 
                    inputIterator[3]==3);

                Assert.IsTrue(isCorrectData);
            }

        }

        [TestMethod()]
        public void PartitionTest2()
        {
            int[] data = { 0, 1, 1, 2, 3 };

            UnaryPredicate<int> isEven = delegate (int x)
            {
                return x % 2 == 0;
            };

            using (ForwardIterator<int> inputIterator = new ForwardIterator<int>(data))
            {
                Algorithm.Partition(inputIterator, isEven);

                bool isCorrectData = (inputIterator[0] == 0 &&
                    inputIterator[1] == 2 &&
                    inputIterator[2] == 1 &&
                    inputIterator[3] == 1 &&
                    inputIterator[4] == 3);

                Assert.IsTrue(isCorrectData);
            }

        }

        [TestMethod()]
        public void PartitionCopyTest()
        {
            int[] data = { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };

            IList<int> trueList = new List<int>();

            IList<int> falseList = new List<int>();

            UnaryPredicate<int> isEven = delegate (int x)
            {
                return x % 2 == 0;
            };

            using (ForwardIterator<int> inputIterator = new ForwardIterator<int>(data))
            {
                using (IOutputIterator<int> trueOutputIterator = new BackInsertIterator<int>(trueList))
                {
                    using (IOutputIterator<int> falseOutputIterator = new BackInsertIterator<int>(falseList))
                    {
                        Algorithm.PartitionCopy(inputIterator, trueOutputIterator,
                            falseOutputIterator, isEven);

                        bool isEvenCorrect = (trueList[0]==0 &&
                            trueList[1]==2 &&
                            trueList[2]==4 &&
                            trueList[3]==6 &&
                            trueList[4]==8);

                        bool isOddCorrect = (falseList[0]==1 &&
                            falseList[1]==3 &&
                            falseList[2]==5 &&
                            falseList[3]==7 &&
                            falseList[4]==9);

                        Assert.IsTrue(isEvenCorrect && isOddCorrect);
                    }
                }
            }
        }




    }
}
