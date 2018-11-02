using LibNavigate.Algorithm;
using LibNavigate.Iterator;
using LibNavigate.Iterator.Extend;
using LibNavigate.Iterator.Helper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace LibNavigateTests
{
    [TestClass()]
    public class ModifyingAlgorithmTests
    {
        [TestMethod()]
        public void CopyTest()
        {
            int[] data = { 1, 2, 3 };

            int expectedCount = 3;

            int actualCount = 0;

            IList<int> lst = new List<int>();

            using (IInputIterator<int> inputIterator=new InputIterator<int>(data))
            {
                using (IOutputIterator<int> outputIterator = new BackInsertIterator<int>(lst))
                {
                    Algorithm.Copy(inputIterator, outputIterator);

                    actualCount = lst.Count();
                }

            }

            Assert.IsTrue(expectedCount == actualCount);

            bool isCorrectData=(lst[0]==1 && lst[1]==2 && lst[2]==3);

            Assert.IsTrue(isCorrectData);
        }

        [TestMethod()]
        public void CopyIfTest()
        {
            int[] data = { 1, 2, 3 };

            int expectedCount = 1;

            int actualCount = 0;

            IList<int> lst = new List<int>();

            using (IInputIterator<int> inputIterator = new InputIterator<int>(data))
            {

                using (IOutputIterator<int> outputIterator = new BackInsertIterator<int>(lst))
                {
                    Algorithm.CopyIf(inputIterator, outputIterator,delegate(int x) 
                    {
                        return x > 2;
                    });

                    actualCount = lst.Count();
                }

            }

            Assert.IsTrue(expectedCount == actualCount);

            bool isCorrectData = (lst[0] == 3);

            Assert.IsTrue(isCorrectData);
        }

        [TestMethod()]
        public void CopyNTest()
        {
            int[] data = { 1, 2, 3, 4, 5, 6 };

            int expectedCount = 2;

            int actualCount = 0;

            IList<int> lst = new List<int>();

            using (IInputIterator<int> inputIterator = new InputIterator<int>(data))
            {
                using (IOutputIterator<int> outputIterator = new BackInsertIterator<int>(lst))
                {
                    Algorithm.CopyN(inputIterator, 2, outputIterator);

                    actualCount = lst.Count();
                }

            }

            Assert.IsTrue(expectedCount == actualCount);

            bool isCorrectData =(lst[0]==1 && lst[1]==2);

            Assert.IsTrue(isCorrectData);

        }

        [TestMethod()]
        public void CopyBackwardTest()
        {
            int[] data = { 1, 2, 3 };

            int expectedCount = 3;

            int actualCount = 0;

            IList<int> lst = new List<int>();

            using (IBidirectionalIterator<int> inputIterator = new BidirectionalIterator<int>(data))
            {
                using (IOutputIterator<int> outputIterator = new BackInsertIterator<int>(lst))
                {
                    Algorithm.CopyBackward(inputIterator, outputIterator);

                    actualCount = lst.Count();
                }

            }

            Assert.IsTrue(expectedCount == actualCount);

            bool isCorrectData = (lst[0] == 3 && lst[1] == 2 && lst[2] == 1);

            Assert.IsTrue(isCorrectData);
        }

        [TestMethod()]
        public void MoveTest()
        {
            int[] data = { 1, 2, 3 };

            IList<int> lst = new List<int>();

            int expectedCount = 3;

            int actualCount = 0;


            using (InputIterator<int> inputIterator = new InputIterator<int>(data))
            {
                using (IOutputIterator<int> outputIterator = new BackInsertIterator<int>(lst))
                {
                    Algorithm.Move(inputIterator, outputIterator);

                    actualCount = lst.Count();

                }

                //whether the data is actually remove
                Assert.IsTrue(inputIterator.Count()==0);

            }


            Assert.IsTrue(expectedCount==actualCount);

            bool isCorrectData = (lst[0] == 1 && lst[1] == 2 && lst[2] == 3);

            Assert.IsTrue(isCorrectData);

        }

        [TestMethod()]
        public void MoveTest2()
        {
            int[] data = { 1, 2, 3 };

            IList<int> lst = new List<int>();

            int expectedCount = 3;

            int actualCount = 0;


            using (BidirectionalIterator<int> inputIterator = new BidirectionalIterator<int>(data))
            {
                using (IOutputIterator<int> outputIterator = new BackInsertIterator<int>(lst))
                {
                    Algorithm.Move(inputIterator, outputIterator);

                    actualCount = lst.Count();

                }

                //whether the data is actually remove
                Assert.IsTrue(inputIterator.Count() == 0);

            }


            Assert.IsTrue(expectedCount == actualCount);

            bool isCorrectData = (lst[0] == 1 && lst[1] == 2 && lst[2] == 3);

            Assert.IsTrue(isCorrectData);

        }

        [TestMethod()]
        public void MoveBackwardTest()
        {
            int[] data = { 1, 2, 3 };

            IList<int> lst = new List<int>();

            int expectedCount = 3;

            int actualCount = 0;


            using (BidirectionalIterator<int> inputIterator = new BidirectionalIterator<int>(data))
            {
                using (IOutputIterator<int> outputIterator = new BackInsertIterator<int>(lst))
                {
                    Algorithm.MoveBackward(inputIterator, outputIterator);

                    actualCount = lst.Count();

                }

                //whether the data is actually remove
                 Assert.IsTrue(inputIterator.Count() == 0);

            }


            Assert.IsTrue(expectedCount == actualCount);

            bool isCorrectData = (lst[0] == 3 && lst[1] == 2 && lst[2] == 1);

            Assert.IsTrue(isCorrectData);

        }

        [TestMethod()]
        public void FillTest()
        {
            using (ForwardIterator<int> inputIterator=new ForwardIterator<int>(3))
            {
                Algorithm.Fill(inputIterator, 5);

                Assert.IsTrue(inputIterator.Count()==3);

                bool isCorrectData = (inputIterator[0] == 5 && 
                    inputIterator[1] == 5 && 
                    inputIterator[2] == 5);

                Assert.IsTrue(isCorrectData);
            }
        }

        [TestMethod()]
        public void TransformTest()
        {
            int[] data = { 1, 2, 3 };

            IList<int> lst = new List<int>();

            int expectedCount = 3;

            int expectedSum = 9;

            int actualCount = 0;

            int actualSum = 0;

            using (IInputIterator<int> inputIterator=new InputIterator<int>(data))
            {
                using (IOutputIterator<int> outputIterator = new BackInsertIterator<int>(lst))
                {
                    Algorithm.Transform(inputIterator, outputIterator, delegate(int x) 
                    {
                        return x + 1;
                    });

                    actualCount = lst.Count();

                    actualSum = lst.Sum();

                }

            }

            Assert.IsTrue(expectedCount == actualCount);
            Assert.IsTrue(expectedSum == actualSum);
        }

        [TestMethod()]
        public void GenerateTest()
        {
            using (ForwardIterator<int> inputIterator = new ForwardIterator<int>(3))
            {
                Algorithm.Generate(inputIterator, delegate
                {
                    return 2;
                });

                Assert.IsTrue(inputIterator.Count() == 3);

                bool isCorrectData = (inputIterator[0] == 2 &&
                    inputIterator[1] == 2 &&
                    inputIterator[2] == 2);

                Assert.IsTrue(isCorrectData);
            }
        }

        [TestMethod()]
        public void GenerateNTest()
        {
            using (ForwardIterator<int> inputIterator = new ForwardIterator<int>(3))
            {
                Algorithm.GenerateN(inputIterator,1, delegate
                {
                    return 2;
                });

                Assert.IsTrue(inputIterator.Count() == 3);

                bool isCorrectData = (inputIterator[0] == 2 &&
                    inputIterator[1] == 0 &&
                    inputIterator[2] == 0);

                Assert.IsTrue(isCorrectData);
            }
        }

        [TestMethod()]
        public void RemoveTest()
        {
            int[] data = { 1, 2, 2, 3 };

            int expectedCount = 2;

            int actualCount = 0;

            using (InputIterator<int> inputIterator=new InputIterator<int>(data))
            {
                Algorithm.Remove(inputIterator, 2);

                actualCount = inputIterator.Count();

                Assert.IsTrue(expectedCount == actualCount);

                bool isCorrectData = (inputIterator[0] == 1 && inputIterator[1] == 3);

                Assert.IsTrue(isCorrectData);

            }
        }

        [TestMethod()]
        public void RemoveIfTest()
        {
            int[] data = { 1, 2, 2, 3 };

            int expectedCount = 2;

            int actualCount = 0;

            using (InputIterator<int> inputIterator = new InputIterator<int>(data))
            {
                Algorithm.RemoveIf(inputIterator, delegate(int x) 
                {
                    return x == 2;
                });

                actualCount = inputIterator.Count();

                Assert.IsTrue(expectedCount == actualCount);

                bool isCorrectData = (inputIterator[0] == 1 && inputIterator[1] == 3);

                Assert.IsTrue(isCorrectData);

            }
        }

        [TestMethod()]
        public void RemoveCopyTest()
        {
            int[] data = { 1, 2, 2, 3 };

            int expectedCount = 3;

            int actualCount = 0;

            using (IInputIterator<int> inputIterator=new InputIterator<int>(data))
            {
                IList<int> lst = new List<int>();

                using (IOutputIterator<int> outputIterator = new BackInsertIterator<int>(lst))
                {
                    Algorithm.RemoveCopy(inputIterator, outputIterator, 3);

                    actualCount = lst.Count();

                    Assert.IsTrue(expectedCount == actualCount);

                    bool isCorrectData = (lst[0]==1 && lst[1]==2 && lst[2]==2);

                    Assert.IsTrue(isCorrectData);
                    

                }
            }

        }

        [TestMethod()]
        public void RemoveCopyIfTest()
        {
            int[] data = { 1, 2, 2, 3 };

            int expectedCount = 3;

            int actualCount = 0;

            using (IInputIterator<int> inputIterator = new InputIterator<int>(data))
            {
                IList<int> lst = new List<int>();

                using (IOutputIterator<int> outputIterator = new BackInsertIterator<int>(lst))
                {
                    Algorithm.RemoveCopyIf(inputIterator, outputIterator,delegate(int x) 
                    {
                        return x == 3;
                    }
                    );

                    actualCount = lst.Count();

                    Assert.IsTrue(expectedCount == actualCount);

                    bool isCorrectData = (lst[0] == 1 && lst[1] == 2 && lst[2] == 2);

                    Assert.IsTrue(isCorrectData);


                }
            }

        }

        [TestMethod()]
        public void ReplaceTest()
        {
            int[] data = { 1, 2, 3 };

            using (ForwardIterator<int> inputIterator = new ForwardIterator<int>(data))
            {
                Algorithm.Replace(inputIterator, 3, 2);

                bool isCorrectData = (inputIterator[0] == 1 &&
                    inputIterator[1] == 2 &&
                    inputIterator[2] == 2);

                Assert.IsTrue(isCorrectData);

            }
        }

        [TestMethod()]
        public void ReplaceIfTest()
        {
            int[] data = { 1, 2, 3 };

            using (ForwardIterator<int> inputIterator = new ForwardIterator<int>(data))
            {
                Algorithm.ReplaceIf(inputIterator, delegate(int x)
                {
                    return x == 3;
                }, 2);

                bool isCorrectData = (inputIterator[0] == 1 &&
                    inputIterator[1] == 2 &&
                    inputIterator[2] == 2);

                Assert.IsTrue(isCorrectData);

            }
        }

        [TestMethod()]
        public void ReplaceCopyTest()
        {
            int[] data = { 1, 2, 3 };

            IList<int> lst = new List<int>();

            using (InputIterator<int> inputIterator=new InputIterator<int>(data))
            {
                using (IOutputIterator<int> outputIterator = new BackInsertIterator<int>(lst))
                {
                    Algorithm.ReplaceCopy(inputIterator, outputIterator, 3, 2);

                    //checking if the input data is not changed

                    bool isCorrectInputData = (inputIterator[0] == 1 && 
                        inputIterator[1] == 2 && 
                        inputIterator[2] == 3);

                    Assert.IsTrue(isCorrectInputData);

                    //checking if the output data is changed

                    bool isCorrectOutputData = (lst[0] == 1 && lst[1] == 2 &&
                        lst[2] == 2);

                    Assert.IsTrue(isCorrectOutputData);
                }
            }
        }


        [TestMethod()]
        public void ReplaceCopyIfTest()
        {
            int[] data = { 1, 2, 3 };

            IList<int> lst = new List<int>();

            using (InputIterator<int> inputIterator = new InputIterator<int>(data))
            {
                using (IOutputIterator<int> outputIterator = new BackInsertIterator<int>(lst))
                {
                    Algorithm.ReplaceCopyIf(inputIterator, outputIterator,delegate(int x)
                    {
                        return x == 3;
                    }, 2);

                    //checking if the input data is not changed

                    bool isCorrectInputData = (inputIterator[0] == 1 &&
                        inputIterator[1] == 2 &&
                        inputIterator[2] == 3);

                    Assert.IsTrue(isCorrectInputData);

                    //checking if the output data is changed

                    bool isCorrectOutputData = (lst[0] == 1 && lst[1] == 2 &&
                        lst[2] == 2);

                    Assert.IsTrue(isCorrectOutputData);
                }
            }
        }

        [TestMethod()]
        public void IterSwapTest()
        {
            using (ForwardIterator<int> inputIterator1 = new ForwardIterator<int>(new int[] { 1,2 }))
            {
                using (ForwardIterator<int> inputIterator2 = new ForwardIterator<int>(new int[] { 3, 4 }))
                {
                    Algorithm.IterSwap(inputIterator1, inputIterator2);

                    Assert.IsTrue(inputIterator1[0]==3 && inputIterator1[1]==2 &&
                        inputIterator2[0]==1 && inputIterator2[1]==4);

                }
            }
        }

        [TestMethod()]
        public void SwapRangeTest()
        {
            using (ForwardIterator<int> inputIterator1 = new ForwardIterator<int>(new int[] { 1, 2 }))
            {
                using (ForwardIterator<int> inputIterator2 = new ForwardIterator<int>(new int[] { 3, 4 }))
                {
                    Algorithm.SwapRange(inputIterator1, inputIterator2);

                    Assert.IsTrue(inputIterator1[0] == 3 && inputIterator1[1] == 4 &&
                           inputIterator2[0] == 1 && inputIterator2[1] == 2);
                }
            }
        }

        [TestMethod()]
        public void ReverseTest()
        {
            int[] data = { 1, 2, 3 };

            using (BidirectionalIterator<int> inputIterator = new BidirectionalIterator<int>(data))
            {
                Algorithm.Reverse(inputIterator);

                Assert.IsTrue(inputIterator[0]==3 &&
                    inputIterator[1]==2 && 
                    inputIterator[2] ==1);
            }
        }

        [TestMethod()]
        public void ReverseCopyTest()
        {
            int[] data = { 1, 2, 3 };

            IList<int> lst = new List<int>();

            using (IBidirectionalIterator<int> inputIterator = new BidirectionalIterator<int>(data))
            {
                using (IOutputIterator<int> outputIterator = new BackInsertIterator<int>(lst))
                {
                    Algorithm.ReverseCopy(inputIterator, outputIterator);
                }

                Assert.IsTrue(lst[0]==3 && lst[1]==2 && lst[2]==1);
            }

        }

        [TestMethod()]
        public void RotateTest()
        {
            int[] data = { 1, 2, 3, 4, 5, 6, 7, 8, 9 };

            IList<int> lst = new List<int>();

            using (IForwardIterator<int> inputIterator=new ForwardIterator<int>(data))
            {
                IShallowClone cloneable = (IShallowClone)inputIterator;

                IForwardIterator<int> cloneObj = cloneable.ShallowClone() as IForwardIterator<int>;
                cloneObj.End();

                ICursor cursor = (ICursor)cloneObj;
                cursor.SetPosition(cursor.GetPosition()-2);

                //set the first element to 8 element position

                Algorithm.Rotate(inputIterator, cursor.GetPosition());

                inputIterator.Begin();

                using (IOutputIterator<int> outputIterator=new BackInsertIterator<int>(lst))
                {
                    Algorithm.Copy(inputIterator, outputIterator);
                }
            }

            bool isCorrectData = (lst[0]==8 && lst[1]==9 &&
                lst[2]==1 && lst[3]==2 &&
                lst[4]==3 && lst[5]==4 &&
                lst[6]==5 && lst[7]==6 &&
                lst[8]==7);

            Assert.IsTrue(isCorrectData);
        }

        [TestMethod()]
        public void RotateTest2()
        {
            int[] data = { 1, 2, 3, 4, 5, 6, 7, 8, 9 };

            IList<int> lst = new List<int>();

            using (IForwardIterator<int> inputIterator = new ForwardIterator<int>(data))
            {
                IShallowClone cloneable = (IShallowClone)inputIterator;

                IForwardIterator<int> cloneObj = cloneable.ShallowClone() as IForwardIterator<int>;
                cloneObj.End();

                ICursor cursor = (ICursor)cloneObj;
                cursor.SetPosition(cursor.GetPosition() - 2);

                //set the first element to 8 element position

                Algorithm.Rotate(inputIterator, cloneObj);

                inputIterator.Begin();

                using (IOutputIterator<int> outputIterator = new BackInsertIterator<int>(lst))
                {
                    Algorithm.Copy(inputIterator, outputIterator);
                }
            }

            bool isCorrectData = (lst[0] == 8 && lst[1] == 9 &&
                lst[2] == 1 && lst[3] == 2 &&
                lst[4] == 3 && lst[5] == 4 &&
                lst[6] == 5 && lst[7] == 6 &&
                lst[8] == 7);

            Assert.IsTrue(isCorrectData);
        }

        [TestMethod()]
        public void RotateCopyTest()
        {
            int[] data = { 1, 2, 3, 4, 5 };

            IList<int> lst = new List<int>();

            using (IInputIterator<int> inputIterator = new InputIterator<int>(data))
            {
                IShallowClone cloneable = (IShallowClone)inputIterator;

                IInputIterator<int> cloneObj = cloneable.ShallowClone() as IInputIterator<int>;
                cloneObj.End();

                ICursor cursor = (ICursor)cloneObj;
                cursor.SetPosition(cursor.GetPosition() - 1);

                //set the first element to 4element position

                using (IOutputIterator<int> outputIterator = new BackInsertIterator<int>(lst))
                {
                    Algorithm.RotateCopy(inputIterator, cursor.GetPosition(), outputIterator);
                }
               
            }

            bool isCorrectData = (lst[0] == 4 && lst[1] == 5 &&
                lst[2] == 1 && lst[3] == 2 &&
                lst[4] == 3);

            Assert.IsTrue(isCorrectData);
        }

        [TestMethod()]
        public void RotateCopyTest2()
        {
            int[] data = { 1, 2, 3, 4, 5 };

            IList<int> lst = new List<int>();

            using (IInputIterator<int> inputIterator = new InputIterator<int>(data))
            {
                IShallowClone cloneable = (IShallowClone)inputIterator;

                IInputIterator<int> cloneObj = cloneable.ShallowClone() as IInputIterator<int>;
                cloneObj.End();

                ICursor cursor = (ICursor)cloneObj;
                cursor.SetPosition(cursor.GetPosition() - 2);

                //set the first element to 4element position

                using (IOutputIterator<int> outputIterator = new BackInsertIterator<int>(lst))
                {
                    Algorithm.RotateCopy(inputIterator, cloneObj, outputIterator);
                }

            }

            bool isCorrectData = (lst[0] == 4 && lst[1] == 5 &&
                lst[2] == 1 && lst[3] == 2 &&
                lst[4] == 3);

            Assert.IsTrue(isCorrectData);
        }

        [TestMethod()]
        public void RandomShuffle()
        {
            int[] data = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 14, 15, 16 };

            using (IForwardIterator<int> inputIterator=new ForwardIterator<int>(data))
            {
                Algorithm.RandomShuffle(inputIterator);

                inputIterator.Begin();

                bool isSorted = Algorithm.IsSorted(inputIterator);

                //we have random the data , therefore it should not be sorted
                //the chance of returning the sorted data is very small

                Assert.IsFalse(isSorted);
            }
        }

        [TestMethod()]
        public void Shuffle()
        {
            int[] data = { 1, 2, 3, 4, 5, 6, 7, 8 };

            int seed = 23;

            IList<int> lst1 = new List<int>();

            IList<int> lst2 = new List<int>();

            using (IForwardIterator<int> inputIterator = new ForwardIterator<int>(data))
            {
                Algorithm.Shuffle(inputIterator, seed);
                inputIterator.Begin();

                bool isSorted = Algorithm.IsSorted(inputIterator);

                //we have random the data , therefore it should not be sorted
                //the chance of returning the sorted data is very small

                Assert.IsFalse(isSorted);

                inputIterator.Begin();

                //copy the random data to list 1

                using (IOutputIterator<int> outputIterator = new BackInsertIterator<int>(lst1))
                {
                    Algorithm.Copy(inputIterator, outputIterator);
                }

                inputIterator.Begin();

                //copy the random data to list 2

                using (IOutputIterator<int> outputIterator = new BackInsertIterator<int>(lst2))
                {
                    Algorithm.Copy(inputIterator, outputIterator);
                }

            }

            using (IForwardIterator<int> inputIterator = new ForwardIterator<int>(data))
            {
                //shuffle another time

                Algorithm.Shuffle(inputIterator, seed);

                //copy the random data to list 2

                using (IOutputIterator<int> outputIterator = new BackInsertIterator<int>(lst1))
                {
                    Algorithm.Copy(inputIterator, outputIterator);
                }

            }

            bool isCorrectData = (lst1[0] == lst2[0] && lst1[1] == lst2[1] &&
                lst1[2] == lst2[2] &&
                lst1[3] == lst2[3] && 
                lst1[4] == lst2[4] && 
                lst1[5] == lst2[5] && 
                lst1[6] == lst2[6] &&
                lst1[7] == lst2[7]);

            //since we use the same seed,both shuffle data should be same

            Assert.IsTrue(isCorrectData);


        }

        [TestMethod()]
        public void UniqueTest()
        {
            string[] data = new string[] 
            {
                "hello",
                "world",
                "road",
                "road",
                "cool",
                "job",
                "road"
            };

            int expectedCount = 6;

            int actualCount = 0;

            using (ForwardIterator<string> inputIterator=new ForwardIterator<string>(data))
            {
                Algorithm.Unique(inputIterator);

                actualCount = inputIterator.Count();

                Assert.IsTrue(expectedCount == actualCount);

                bool isCorrectData = (inputIterator[0]== "hello" && 
                    inputIterator[1]== "world" &&
                    inputIterator[2] == "world" &&
                    inputIterator[3] == "cool" &&
                    inputIterator[4] == "job" &&
                    inputIterator[5] == "road");
            }
        }

        [TestMethod()]
        public void UniqueCopyTest()
        {
            string[] data = new string[]
            {
                "hello",
                "world",
                "road",
                "road",
                "cool",
                "job",
                "road"
            };

            int expectedCount = 6;

            int actualCount = 0;

            IList<string> lst = new List<string>();

            using (ForwardIterator<string> inputIterator = new ForwardIterator<string>(data))
            {
                using (IOutputIterator<string> outputIterator = new BackInsertIterator<string>(lst))
                {
                    Algorithm.UniqueCopy(inputIterator,outputIterator);

                    actualCount = lst.Count();

                    Assert.IsTrue(expectedCount == actualCount);

                    bool isCorrectData = (lst[0] == "hello" &&
                        lst[1] == "world" &&
                        lst[2] == "world" &&
                        lst[3] == "cool" &&
                        lst[4] == "job" &&
                        lst[5] == "road");
                }
            }
        }


    }
}
