using LibNavigate.Algorithm;
using LibNavigate.Iterator;
using LibNavigate.Iterator.Extend;
using LibNavigate.Iterator.Helper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace LibNavigateTests
{
    [TestClass()]
    public class AlgorithmTests
    {
        [TestMethod()]
        public void AllOfTest()
        {
            int[] data = { 1, 2, 3 };

            using (IInputIterator<int> inputIterator = new InputIterator<int>(data))
            {
                Assert.IsTrue(Algorithm.AllOf(inputIterator, delegate(int x)
                {
                    return x > 0;
                }
                ));
            }
        }

        [TestMethod()]
        public void AnyOfTest()
        {
            int[] data = { -1, -2, 0, 1, -5 };

            using (IInputIterator<int> inputIterator = new InputIterator<int>(data))
            {
                Assert.IsTrue(Algorithm.AnyOf(inputIterator, delegate(int x) 
                {
                    return x>0;
                }));
            }
        }

        [TestMethod()]
        public void NoneOfTest()
        {
            int[] data = { -1, -2, 0, -1, -5 };

            using (IInputIterator<int> inputIterator = new InputIterator<int>(data))
            {
                Assert.IsTrue(Algorithm.NoneOf(inputIterator, delegate (int x) 
                {
                    return x > 0;
                }));
            }
        }

        [TestMethod()]
        public void ForEachTest()
        {
            int sum = 0;

            int[] data = { 1, 2, 3 };

            using (IInputIterator<int> inputIterator = new InputIterator<int>(data))
            {
                Algorithm.ForEach(inputIterator, delegate (int x)
                 {
                     sum += x;
                 });
            }

            Assert.IsTrue(sum==6);
        }

        [TestMethod()]
        public void CountTest()
        {
            int expectedCount = 4;

            int actualCount = 0;

            int[] data = {1,2,3,1,1,4,1 };

            using (IInputIterator<int> inputIterator=new InputIterator<int>(data))
            {
                actualCount= Algorithm.Count(inputIterator, 1);
            }

            Assert.IsTrue(expectedCount == actualCount);
        }

        [TestMethod()]
        public void CountIfTest()
        {
            int expectedCount = 4;

            int actualCount = 0;

            int[] data = { 1, 2, 3, 1, 1, 4, 1 };

            using (IInputIterator<int> inputIterator = new InputIterator<int>(data))
            {
                actualCount = Algorithm.CountIf(inputIterator, delegate (int x)
                {
                    return (x == 1);
                }
                );
            }

            Assert.IsTrue(expectedCount == actualCount);

        }

        [TestMethod()]
        public void MismatchTest()
        {
            int[] data1= { 1, 2, 3, 4, 5, 7, 8 };

            int[] data2 = { 1, 2, 3, 4, 6, 7, 8 };

            KeyValuePair<int, int> value=new KeyValuePair<int, int>();

            using (IInputIterator<int> inputIterator1=new InputIterator<int>(data1))
            {
                using (IInputIterator<int> inputIterator2 = new InputIterator<int>(data2))
                {
                    value = Algorithm.Mismatch(inputIterator1, inputIterator2);
                }
            }

            Assert.IsTrue(value.Key == 5 && value.Value == 6);
        }

        [TestMethod()]
        public void EqualTest()
        {
            int[] data1 = { 1, 2, 3, 4, 5, 7, 8 };

            int[] data2 = { 1, 2, 3, 4, 5, 7, 8 };

            using (IInputIterator<int> inputIterator1 = new InputIterator<int>(data1))
            {
                using (IInputIterator<int> inputIterator2 = new InputIterator<int>(data2))
                {
                    Assert.IsTrue(Algorithm.Equal(inputIterator1, inputIterator2));
                }
            }    
        }

        [TestMethod()]
        public void EqualTest2()
        {
            int[] data1 = { 1, 2, 3, 4, 5, 7};

            int[] data2 = { 1, 2, 3, 4, 5, 7, 8 };

            using (IInputIterator<int> inputIterator1 = new InputIterator<int>(data1))
            {
                using (IInputIterator<int> inputIterator2 = new InputIterator<int>(data2))
                {
                    Assert.IsFalse(Algorithm.Equal(inputIterator1, inputIterator2));
                }
            }
        }

        [TestMethod()]
        public void EqualTest3()
        {
            int[] data1 = { 1, 2, 3, 4, 5, 7, 8 };

            int[] data2 = { 1, 2, 3, 4, 5, 7};

            using (IInputIterator<int> inputIterator1 = new InputIterator<int>(data1))
            {
                using (IInputIterator<int> inputIterator2 = new InputIterator<int>(data2))
                {
                    Assert.IsFalse(Algorithm.Equal(inputIterator1, inputIterator2));

                }
            }
        }

        [TestMethod()]
        public void FindTest()
        {
            int[] data1 = { 1, 2, 3, 4, 5, 7, 9, 8 };

            using (IInputIterator<int> inputIterator = new InputIterator<int>(data1))
            {
                bool isEnd = Algorithm.Find(inputIterator, 9).IsEnd();

                Assert.IsFalse(isEnd);
            }
        }

        [TestMethod()]
        public void FindIfTest()
        {
            int[] data1 = { 1, 2, 3, 4, 5, 7, 9, 8 };

            using (IInputIterator<int> inputIterator = new InputIterator<int>(data1))
            {
                bool result= Algorithm.FindIf(inputIterator, delegate (int x)
                 {
                     return x == 9;
                 }).IsEnd();

                Assert.IsFalse(result);
            }
        }

        [TestMethod()]
        public void FindIfNotTest()
        {
            int[] data1 = { 1, 2, 3, 4, 5, 7, 9, 8,-1,9 };

            using (IInputIterator<int> inputIterator = new InputIterator<int>(data1))
            {
                Algorithm.FindIfNot(inputIterator, delegate (int x)
                 {
                     return x > 0;
                 });

                ICursor cursor = (ICursor)inputIterator;

                int index = cursor.GetPosition();

                Assert.IsTrue(index == 8);
            }
        }

        [TestMethod()]
        public void FindEndTest()
        {

            int[] data1 = { 1, 2, 3, 4, 1, 2, 3, 4};

            int[] data2 = { 1, 2, 3 };

            int expectedIndex = 4;

            int actualIndex = -1;

            using (IInputIterator<int> inputIterator1=new InputIterator<int>(data1))
            {
                using (IInputIterator<int> inputIterator2 = new InputIterator<int>(data2))
                {
                    IInputIterator<int> tmp = Algorithm.FindEnd(inputIterator1, inputIterator2);

                    ICursor cursor = (ICursor)tmp;

                    actualIndex = cursor.GetPosition();
                }
            }

            Assert.IsTrue(expectedIndex == actualIndex);            

        }

        [TestMethod()]
        public void FindFirstOfTest()
        {
            
            int[] data1 = { 1, 2, 3, 4, 5, 6 };

            int[] data2 = { 7, 8, 9, 10, 3, 11 };

            int expectedData = 3;

            int actualData = 0;

            bool result = false;

            using (IInputIterator<int> inputIterator1 = new InputIterator<int>(data1))
            {
                using (IInputIterator<int> inputIterator2 =new InputIterator<int>(data2))
                {
                    result= Algorithm.FindFirstOf(inputIterator1, inputIterator2, out actualData);
                }
            }

            Assert.IsTrue((result==true) && (expectedData == actualData));

        }

        [TestMethod()]
        public void AdjacentFindTest()
        {
            char[] data = {'x','r','q','w','e','e','y','t'};

            char expected = 'e';

            char actual = '?';

            using (IInputIterator<char> inputIterator =new InputIterator<char>(data))
            {
                actual= Algorithm.AdjacentFind(inputIterator);
            }

            Assert.IsTrue(expected == actual);
        }

        [TestMethod()]
        public void AdjacentFindTest2()
        {
            char[] data = { 'x', 'r', 'q', 'w', 'e', 'v', 'y', 't' };

            char expected = 't';

            char actual = '?';

            using (IInputIterator<char> inputIterator = new InputIterator<char>(data))
            {
                actual = Algorithm.AdjacentFind(inputIterator);
            }

            Assert.IsTrue(expected == actual);
        }

        [TestMethod()]
        public void SearchTest()
        {
            
            int[] data1 = { 1, 2, 3, 4, 5, 7, 8 };

            int[] data2 = { 4, 5, 79, 1, 23, 45 };

            int expected = 1;

            int actual = -1;

            using (IInputIterator<int> inputIterator1 = new InputIterator<int>(data1))
            {
                using (IInputIterator<int> inputIterator2 = new InputIterator<int>(data2))
                {
                    actual= Algorithm.Search(inputIterator1, inputIterator2).Read();
                }
            }

            Assert.IsTrue(expected == actual);
        }

        [TestMethod()]
        public void SearchTest2()
        {

            int[] data1 = { 1, 2, 3, 4, 5, 7, 8 };

            int[] data2 = { 77, 78, 79, 80, 81, 45 };

            using (IInputIterator<int> inputIterator1 = new InputIterator<int>(data1))
            {
                using (IInputIterator<int> inputIterator2 = new InputIterator<int>(data2))
                {
                    bool result = Algorithm.Search(inputIterator1, inputIterator2).IsEnd();

                    Assert.IsTrue(result);
                }
            }

        }

        [TestMethod()]
        public void SearchNTest()
        {
            int[] data = { 1, 2, 3, 4, 5 };

            int expectedIndex = 2;

            int actualIndex = 0;

            using (IInputIterator<int> inputIterator =new InputIterator<int>(data))
            {
                actualIndex = Algorithm.SearchN(inputIterator, 4, 3);
            }

            Assert.IsTrue(expectedIndex == actualIndex);
        }

        [TestMethod()]
        public void SearchNTest2()
        {
            int[] data = { 1, 2, 3, 4, 5 };

            int expectedIndex = -1;

            int actualIndex = 0;

            using (IInputIterator<int> inputIterator = new InputIterator<int>(data))
            {
                actualIndex = Algorithm.SearchN(inputIterator, 2, 3);
            }

            Assert.IsTrue(expectedIndex == actualIndex);
        }
    }
}
