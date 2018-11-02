using LibNavigate.Algorithm;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace LibNavigateTests
{
    [TestClass()]
    public class UtilityAlgorithmTests
    {


        [TestMethod()]
        public void MaxTest()
        {
            int first = 5;

            int second = 6;

            int expected = 6;

            int actual=Algorithm.Max(first, second);

            Assert.IsTrue(expected==actual);
        }

        [TestMethod()]
        public void MaxElementTest()
        {
            int[] data = { 1, 2, 3, 5, 6, 2, 1, 8, 1, 4, 6 };

            int expected = 8;

            using (LibNavigate.Iterator.IInputIterator<int> inputIterator = new LibNavigate.Iterator.Extend.InputIterator<int>(data))
            {
                int actual = Algorithm.MaxElement(inputIterator);

                Assert.IsTrue(expected == actual);
            }
        }

        [TestMethod()]
        public void MinTest()
        {
            int first = 5;

            int second = 6;

            int expected = 5;

            int actual = Algorithm.Min(first, second);

            Assert.IsTrue(expected == actual);
        }

        [TestMethod()]
        public void MinElementTest()
        {
            int[] data = { 3, 4, 5, 8, 1, 8, 23, 54, 12, 65 };

            int expected= 1;

            using (LibNavigate.Iterator.IInputIterator<int> inputIterator = new LibNavigate.Iterator.Extend.InputIterator<int>(data))
            {
                int actual = Algorithm.MinElement(inputIterator);

                Assert.IsTrue(expected == actual);
            }
        }

        [TestMethod()]
        public void MinMaxTest()
        {
            int first = 6;

            int second = 5;

            KeyValuePair<int, int> expected = new KeyValuePair<int, int>(5, 6);

            KeyValuePair<int, int> actual = Algorithm.MinMax(first, second);

            Assert.IsTrue(expected.Key==actual.Key &&
                expected.Value==actual.Value);
        }

        [TestMethod()]
        public void MinMaxElementTest()
        {
            int[] data = { 3, 4, 5, 8, 1, 8, 23, 54, 12, 65 ,
                1, 2, 3, 5, 6, 2, 0, 8, 1, 4, 6 };

            KeyValuePair<int, int> expected = new KeyValuePair<int, int>(0, 65);

            using (LibNavigate.Iterator.IInputIterator<int> inputIterator = new LibNavigate.Iterator.Extend.InputIterator<int>(data))
            {
                KeyValuePair<int, int> actual = Algorithm.MinMaxElement(inputIterator);

                Assert.IsTrue(expected.Key == actual.Key && 
                    expected.Value== actual.Value);
            }
        }

        [TestMethod()]
        public void LexicographicalCompareTest()
        {
            char[] data = { 'a', 'b', 'c', 'd' };

            using (LibNavigate.Iterator.IInputIterator<char> inputIterator1 = new LibNavigate.Iterator.Extend.InputIterator<char>(data))
            {
                using (LibNavigate.Iterator.IInputIterator<char> inputIterator2 = new LibNavigate.Iterator.Extend.InputIterator<char>(data))
                {
                    bool result = Algorithm.LexicographicalCompare(inputIterator1, inputIterator2);

                    Assert.IsFalse(result);
                }
            }
        }

        [TestMethod()]
        public void LexicographicalCompareTest2()
        {
            char[] data1 = { 'd', 'a', 'b', 'c' };

            char[] data2 = { 'c', 'b', 'd', 'a' };


            using (LibNavigate.Iterator.IInputIterator<char> inputIterator1 = new LibNavigate.Iterator.Extend.InputIterator<char>(data1))
            {
                using (LibNavigate.Iterator.IInputIterator<char> inputIterator2 = new LibNavigate.Iterator.Extend.InputIterator<char>(data2))
                {
                    bool result = Algorithm.LexicographicalCompare(inputIterator1, inputIterator2);

                    Assert.IsFalse(result);
                }
            }
        }

        [TestMethod()]
        public void LexicographicalCompareTest3()
        {
            char[] data1 = { 'b', 'd', 'a', 'c' };

            char[] data2 = { 'a', 'd', 'c', 'b' };

            using (LibNavigate.Iterator.IInputIterator<char> inputIterator1 = new LibNavigate.Iterator.Extend.InputIterator<char>(data1))
            {
                using (LibNavigate.Iterator.IInputIterator<char> inputIterator2 = new LibNavigate.Iterator.Extend.InputIterator<char>(data2))
                {
                    bool result = Algorithm.LexicographicalCompare(inputIterator1, inputIterator2);

                    Assert.IsFalse(result);
                }
            }
        }

        [TestMethod()]
        public void LexicographicalCompareTest4()
        {
            char[] data1 = { 'a', 'c', 'd', 'b' };

            char[] data2 = { 'c', 'd', 'a', 'b' };

            using (LibNavigate.Iterator.IInputIterator<char> inputIterator1 = new LibNavigate.Iterator.Extend.InputIterator<char>(data1))
            {
                using (LibNavigate.Iterator.IInputIterator<char> inputIterator2 = new LibNavigate.Iterator.Extend.InputIterator<char>(data2))
                {
                    bool result = Algorithm.LexicographicalCompare(inputIterator1, inputIterator2);

                    Assert.IsTrue(result);
                }
            }
        }



    }
}
