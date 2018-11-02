using LibNavigate.Algorithm;
using LibNavigate.Iterator;
using LibNavigate.Iterator.Extend;
using LibNavigate.Iterator.Helper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LibNavigateTests
{
    [TestClass()]
    public class SortedRangeAlgorithmTests
    {
        [TestMethod()]
        public void LowerBoundTest()
        {
            int[] data = { 1, 1, 2, 3, 3, 3, 3, 4, 4, 4, 5, 5, 6 };

            int expected = 4;

            int expectedIndex = 7;

            using (IInputIterator<int> inputIterator = new InputIterator<int>(data))
            {
                int actual = Algorithm.LowerBound(inputIterator, 4).Read();

                int actualIndex = ((ICursor)inputIterator).GetPosition();

                Assert.IsTrue(expected == actual);

                string str = actualIndex.ToString();

                Console.WriteLine(str);

                Assert.IsTrue(expectedIndex == actualIndex);
                
            }
        }

        [TestMethod()]
        public void UpperBoundTest()
        {
            int[] data = { 1, 1, 2, 3, 3, 3, 3, 4, 4, 4, 5, 5, 6 };

            int expected = 5;

            int expectedIndex = 10;

            using (IInputIterator<int> inputIterator = new InputIterator<int>(data))
            {
                int actual = Algorithm.UpperBound(inputIterator, 4).Read();

                int actualIndex = ((ICursor)inputIterator).GetPosition();

                Assert.IsTrue(expected == actual);
                Assert.IsTrue(expectedIndex == actualIndex);

            }
        }

        [TestMethod()]
        public void BoundTest()
        {
            int[] data = { 1, 1, 2, 3, 3, 3, 3, 4, 4, 4, 5, 5, 6 };

            IList<int> lst = new List<int>();

            using (IInputIterator<int> inputIterator = new InputIterator<int>(data))
            {
                ICursor cursor = inputIterator as ICursor;

                Algorithm.LowerBound(inputIterator, 4);

                int lowerBoundIndex = cursor.GetPosition();

                inputIterator.Begin();

                Algorithm.UpperBound(inputIterator, 4);

                int upperBoundIndex = cursor.GetPosition();

                int count = upperBoundIndex - lowerBoundIndex;

                using (IInputIterator<int> inputIterator2 =new RangeInputIterator<int>(data, lowerBoundIndex, count))
                {
                    using (IOutputIterator<int> outputIterator = new BackInsertIterator<int>(lst))
                    {
                        Algorithm.Copy(inputIterator2, outputIterator);
                    }
                }

            }

            Assert.IsTrue(lst.Count() == 3);
            Assert.IsTrue(lst[0]==4 && 
                lst[1]==4 &&
                lst[2]==4);
        }

        [TestMethod()]
        public void BinarySearchTest()
        {
            int[] data = { 1, 2, 3, 4, 5 };

            bool expected = true;

            using (IInputIterator<int> inputIterator =new InputIterator<int>(data))
            {
                bool actual = Algorithm.BinarySearch(inputIterator, 1);

                Assert.IsTrue(expected == actual);
            }
        }

        [TestMethod()]
        public void EqualRangeTest()
        {
            int[] data = { 1, 1, 2, 3, 3, 3, 3, 4, 4, 4, 5, 5, 6 };

            IList<int> lst = new List<int>();

            using (IInputIterator<int> inputIterator =new InputIterator<int>(data))
            {
                KeyValuePair<int, int> result = Algorithm.EqualRange(inputIterator, 4);

                int lowerBound = result.Key;

                int upperBound = result.Value;

                int count = upperBound - lowerBound;

                using (IInputIterator<int> inputIterator2 =new RangeInputIterator<int>(data, lowerBound, count))
                {
                    using (IOutputIterator<int> outputIterator = new BackInsertIterator<int>(lst))
                    {
                        Algorithm.Copy(inputIterator2, outputIterator);

                    }
                }
            }

            Assert.IsTrue(lst.Count() == 3);
            Assert.IsTrue(lst[0] == 4 &&
                lst[1] == 4 &&
                lst[2] == 4);
        }


        [TestMethod()]
        public void MergeTest()
        {
            int[] data = { 0, 1, 3, 4, 4 };

            int[] data2 = { 0, 2, 2, 3, 6 };

            int expectedCount = 10;

            IList<int> lst = new List<int>();

            using (IInputIterator<int> inputIterator1 =new InputIterator<int>(data))
            {
                using (IInputIterator<int> inputIterator2 = new InputIterator<int>(data2))
                {
                    using (IOutputIterator<int> outputIterator = new BackInsertIterator<int>(lst))
                    {
                        Algorithm.Merge(inputIterator1, inputIterator2, outputIterator);
                    }
                }
            }

            int actualCount = lst.Count();

            Assert.IsTrue(expectedCount == actualCount);

            bool isCorrectData = (lst[0]==0 &&  lst[1]==0 &&
                lst[2]==1 &&   lst[3]==2 && 
                lst[4]==2 && lst[5]==3 &&
                lst[6]==3 && lst[7]==4 &&
                lst[8]==4 && lst[9]==6);

            Assert.IsTrue(isCorrectData);
        }



        [TestMethod()]
        public void IncludesTest()
        {
            char[] data = {'a','b','c','f','h','x' };

            char[] data2 = {'a','b','c' };

            bool expected = true;

            using (IInputIterator<char> inputIterator = new InputIterator<char>(data))
            {
                using (IInputIterator<char> inputIterator2 = new InputIterator<char>(data2))
                {
                    bool actual = Algorithm.Includes(inputIterator, inputIterator2);

                    Assert.IsTrue(expected == actual);
                }
            }
        }

        [TestMethod()]
        public void IncludesTest2()
        {
            char[] data = { 'a', 'b', 'c', 'f', 'h', 'x' };

            char[] data2 = { 'a', 'c' };

            bool expected = true;

            using (IInputIterator<char> inputIterator = new InputIterator<char>(data))
            {
                using (IInputIterator<char> inputIterator2 = new InputIterator<char>(data2))
                {
                    bool actual = Algorithm.Includes(inputIterator, inputIterator2);

                    Assert.IsTrue(expected == actual);
                }
            }
        }

        [TestMethod()]
        public void IncludesTest3()
        {
            char[] data = { 'a', 'b', 'c', 'f', 'h', 'x' };

            char[] data2 = { 'g' };

            using (IInputIterator<char> inputIterator = new InputIterator<char>(data))
            {
                using (IInputIterator<char> inputIterator2 = new InputIterator<char>(data2))
                {
                    bool result = Algorithm.Includes(inputIterator, inputIterator2);

                    Assert.IsFalse(result);
                }
            }
        }

        [TestMethod()]
        public void IncludesTest4()
        {
            char[] data = { 'a', 'b', 'c', 'f', 'h', 'x' };

            char[] data2 = { 'a', 'c', 'g' };

            using (IInputIterator<char> inputIterator = new InputIterator<char>(data))
            {
                using (IInputIterator<char> inputIterator2 = new InputIterator<char>(data2))
                {
                    bool result = Algorithm.Includes(inputIterator, inputIterator2);

                    Assert.IsFalse(result);
                }
            }
        }

        [TestMethod()]
        public void SetDifferenceTest()
        {
            int[] data1 = { 1, 2, 5, 5, 5, 9 };

            int[] data2 = { 2, 5, 7 };

            int expectedCount = 4;

            IList<int> lst = new List<int>();

            using (IInputIterator<int> inputIterator1 =new InputIterator<int>(data1))
            {
                using (IInputIterator<int> inputIterator2 =new InputIterator<int>(data2))
                {
                    using (IOutputIterator<int> outputIterator = new BackInsertIterator<int>(lst))
                    {
                        Algorithm.SetDifference(inputIterator1, inputIterator2, outputIterator);
                    }
                }
            }


            int actualCount = lst.Count();

            Assert.IsTrue(expectedCount == actualCount);

            bool isCorrectData = (lst[0] == 1 && lst[1] == 5 &&
                lst[2] == 5 && lst[3] == 9);

            Assert.IsTrue(isCorrectData);

        }

        [TestMethod()]
        public void SetIntersectionTest()
        {
            int[] data1 = { 1, 2, 3, 4, 5, 6, 7, 8 };

            int[] data2 = { 5, 7, 9, 10 };

            int expectedCount = 2;

            IList<int> lst = new List<int>();

            using (IInputIterator<int> inputIterator1 = new InputIterator<int>(data1))
            {
                using (IInputIterator<int> inputIterator2 = new InputIterator<int>(data2))
                {
                    using (IOutputIterator<int> outputIterator = new BackInsertIterator<int>(lst))
                    {
                        Algorithm.SetIntersection(inputIterator1, inputIterator2, outputIterator);
                    }
                }
            }

            int actualCount = lst.Count();

            Assert.IsTrue(expectedCount == actualCount);
            Assert.IsTrue(lst[0]==5 && lst[1]==7);

        }

        [TestMethod()]
        public void SetSymmetricDifference()
        {
            int[] data1 = { 1, 2, 3, 4, 5, 6, 7, 8 };

            int[] data2 = { 5, 7, 9, 10 };

            int expectedCount = 8;

            IList<int> lst = new List<int>();

            using (IInputIterator<int> inputIterator1 = new InputIterator<int>(data1))
            {
                using (IInputIterator<int> inputIterator2 = new InputIterator<int>(data2))
                {
                    using (IOutputIterator<int> outputIterator = new BackInsertIterator<int>(lst))
                    {
                        Algorithm.SetSymmetricDifference(inputIterator1, inputIterator2, outputIterator);
                    }
                }
            }

            int actualCount = lst.Count();

            Assert.IsTrue(expectedCount == actualCount);

            bool isCorrectData = (lst[0]==1 && lst[1]==2 &&
                lst[2]==3 && lst[3]==4 &&
                lst[4]==6 && lst[5]==8 &&
                lst[6]==9 && lst[7]==10);

            Assert.IsTrue(isCorrectData);
        }

        [TestMethod()]
        public void SetUnionTest()
        {
            int[] data = { 1, 2, 3, 4, 5 };

            int[] data2 = { 3, 4, 5, 6, 7 };

            int expectedCount = 7;

            IList<int> lst = new List<int>();

            using (IInputIterator<int> inputIterator1 = new InputIterator<int>(data))
            {
                using (IInputIterator<int> inputIterator2 = new InputIterator<int>(data2))
                {
                    using (IOutputIterator<int> outputIterator = new BackInsertIterator<int>(lst))
                    {
                        Algorithm.SetUnion(inputIterator1, inputIterator2, outputIterator);
                    }
                }
            }

            int actualCount = lst.Count();

            Assert.IsTrue(expectedCount == actualCount);

            bool isCorrectData = (lst[0]==1 && lst[1]==2 &&
                lst[2]==3 && lst[3]==4 &&
                lst[4]==5 && lst[5]==6 &&
                lst[6]==7);

            Assert.IsTrue(isCorrectData);


        }

        [TestMethod()]
        public void SetUnionTest2()
        {
            int[] data = { 1, 2, 3, 4, 5, 5, 5 };

            int[] data2 = { 3, 4, 5, 6, 7 };

            int expectedCount = 9;

            IList<int> lst = new List<int>();

            using (IInputIterator<int> inputIterator1 = new InputIterator<int>(data))
            {
                using (IInputIterator<int> inputIterator2 = new InputIterator<int>(data2))
                {
                    using (IOutputIterator<int> outputIterator = new BackInsertIterator<int>(lst))
                    {
                        Algorithm.SetUnion(inputIterator1, inputIterator2, outputIterator);
                    }
                }
            }

            int actualCount = lst.Count();

            Assert.IsTrue(expectedCount == actualCount);

            bool isCorrectData = (lst[0] == 1 && lst[1] == 2 &&
                lst[2] == 3 && lst[3] == 4 &&
                lst[4] == 5 && lst[5] == 5 &&
                lst[6] == 5 && lst[7] == 6 &&
                lst[8] == 7);

            Assert.IsTrue(isCorrectData);


        }


    }
}
