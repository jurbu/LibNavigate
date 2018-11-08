using LibNavigate.Iterator;
using System;
using System.Collections.Generic;
using static LibNavigate.Function.Function;
using LibNavigate.Iterator.Helper;

namespace LibNavigate.Algorithm
{
    #region NonModifying

    public sealed partial class Algorithm
    {
        /// <summary>
        /// Checks if a predicate is true for all
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="inputIterator"></param>
        /// <param name="func"></param>
        /// <returns></returns>
        public static bool AllOf<T>(IInputIterator<T> inputIterator,
            UnaryPredicate<T> func)
        {
            return FindIfNot(inputIterator, func).IsEnd();
        }


        /// <summary>
        /// Checks if a predicate is true for any
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="inputIterator"></param>
        /// <param name="func"></param>
        public static bool AnyOf<T>(IInputIterator<T> inputIterator,
            UnaryPredicate<T> func)
        {
            return !FindIf(inputIterator, func).IsEnd();
        }

        /// <summary>
        /// Checks if a predicate is true for none
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="inputIterator"></param>
        /// <param name="func"></param>
        public static bool NoneOf<T>(IInputIterator<T> inputIterator,
            UnaryPredicate<T> func)
        {
            return FindIf(inputIterator, func).IsEnd();
        }



        /// <summary>
        /// Perform an operation on all the data of IInputIterator
        /// and return the interface which the data is operate on
        /// </summary>
        /// <typeparam name="Input"></typeparam>
        /// <param name="inputIterator"></param>
        /// <param name="func"></param>
        public static void ForEach<Input>(IInputIterator<Input> inputIterator,
            UnaryVoidFunction<Input> func)
        {
            while (!inputIterator.IsEnd())
            {
                func(inputIterator.Read());

                inputIterator.MoveNext();

            }

        }


        /// <summary>
        /// Return how many search value are found in the IInputIterator
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="inputIterator"></param>
        /// <param name="searchValue"></param>
        /// <returns></returns>
        public static int Count<T>(IInputIterator<T> inputIterator,
            T searchValue) where T:IEquatable<T>
        {
            return Count(inputIterator, searchValue, new Comparer<T>());
        }

        public static int Count<T>(IInputIterator<T> inputIterator, T searchValue,
            IEqualityComparer<T> comparer)
        {
            int count = 0;

            while (!inputIterator.IsEnd())
            {
                if (comparer.Equals(inputIterator.Read(),searchValue))
                {
                    ++count;
                }

                inputIterator.MoveNext();
            }

            return count;
        }


        /// <summary>
        /// Return how many matches are found in the IInputIterator
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="inputIterator"></param>
        /// <param name="unaryPredicate"></param>
        /// <returns></returns>
        public static int CountIf<T>(IInputIterator<T> inputIterator,
            UnaryPredicate<T> func)
        {
            int count = 0;

            while (!inputIterator.IsEnd())
            {
                if (func(inputIterator.Read()))
                {
                    ++count;
                }

                inputIterator.MoveNext();
            }

            return count;
        }

        /// <summary>
        /// Returns the first mismatching pair of elements from two IInputIterator
        /// Example : 
        /// IntInputIterator 1 = {1,2,3,4,5,7,8};
        /// IntInputIterator 2 = {1,2,3,4,6,7,8};
        /// 
        /// The method will return a {5,6} pair since it is the first mismatch
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="inputIterator1"></param>
        /// <param name="inputIterator2"></param>
        /// <returns></returns>
        public static KeyValuePair<T,T> Mismatch<T>(IInputIterator<T> inputIterator1,
            IInputIterator<T> inputIterator2) where T : IEquatable<T>
        {
            return Mismatch(inputIterator1, inputIterator2, new Comparer<T>());
        }

        public static KeyValuePair<T, T> Mismatch<T>(IInputIterator<T> inputIterator1,
            IInputIterator<T> inputIterator2,IEqualityComparer<T> comparer)
        {
            while (!(inputIterator1.IsEnd() || inputIterator2.IsEnd()))
            {
                if (!comparer.Equals(inputIterator1.Read(),inputIterator2.Read()))
                {
                    break;
                }

                inputIterator1.MoveNext();

                inputIterator2.MoveNext();
            }

            return new KeyValuePair<T, T>(inputIterator1.Read(), inputIterator2.Read());

        }



        /// <summary>
        /// Check whether two iterator have the same range and same value
        /// 
        /// Example :
        /// 
        /// IntInputIterator 1 = {1,2,3,4,5,7,8};
        /// IntInputIterator 2 = {1,2,3,4,6,7,8};
        /// 
        /// will return true
        /// 
        /// IntInputIterator 1 = {1,2,3,4,5,7,8};
        /// IntInputIterator 2 = {1,2,3,4,6,7};
        /// 
        /// will return false
        /// 
        ///  IntInputIterator 1 = {1,2,3,4,5,7,8};
        ///  IntInputIterator 2 = {9,10,11,12,13,14,8};
        /// 
        /// will return false
        /// 
        /// IntInputIterator 1 = {1,2,3,4,5,7,8,9,10};
        /// IntInputIterator 2 = {1,2,3,4,5,7,8};
        ///  
        /// will return false
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="inputIterator1"></param>
        /// <param name="inputIterator2"></param>
        /// <returns></returns>
        public static bool Equal<T>(IInputIterator<T> inputIterator1,
            IInputIterator<T> inputIterator2) where T : IEquatable<T>
        {
            return Equal(inputIterator1, inputIterator2, new Comparer<T>());               
        }

        public static bool Equal<T>(IInputIterator<T> inputIterator1,
            IInputIterator<T> inputIterator2,IEqualityComparer<T> comparer)
        {
            while (!(inputIterator1.IsEnd() || inputIterator2.IsEnd()))
            {
                if (!comparer.Equals(inputIterator1.Read(),inputIterator2.Read()))
                {
                    return false;
                }

                inputIterator1.MoveNext();

                inputIterator2.MoveNext();
            }

            return (inputIterator1.IsEnd() && inputIterator2.IsEnd());

        }


        /// <summary>
        /// Finds the first element satisfying specific criteria 
        /// IsEnd() iterator if the element is not found
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="inputIterator"></param>
        /// <param name="searchValue"></param>
        /// <returns></returns>
        public static IInputIterator<T> Find<T>(IInputIterator<T> inputIterator, T searchValue) where T : IEquatable<T>
        {
            return Find(inputIterator, searchValue, new Comparer<T>());
        }

        /// <summary>
        /// Finds the first element satisfying specific criteria 
        /// IsEnd() iterator if the element is not found
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="inputIterator"></param>
        /// <param name="searchValue"></param>
        /// <param name="comparer"></param>
        /// <returns></returns>
        public static IInputIterator<T> Find<T>(IInputIterator<T> inputIterator, T searchValue,
            IEqualityComparer<T> comparer)
        {
            while (!inputIterator.IsEnd())
            {
                if (comparer.Equals(inputIterator.Read(), searchValue))
                {
                    return inputIterator;
                }

                inputIterator.MoveNext();
            }

            return inputIterator;

        }

        /// <summary>
        /// Return the first element which the unaryPredicate return true
        /// IsEnd() iterator if the element is not found
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="inputIterator"></param>
        /// <param name="unaryPredicate"></param>
        /// <returns></returns>
        public static IInputIterator<T> FindIf<T>(IInputIterator<T> inputIterator,
            UnaryPredicate<T> func)
        {
            while (!inputIterator.IsEnd())
            {
                if (func(inputIterator.Read()))
                {
                    return inputIterator;
                }

                inputIterator.MoveNext();
            }

            return inputIterator;
        }

        /// <summary>
        /// Return the first index which the unaryPredicate return false
        /// or IsEnd() iterator if the element is not found
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="inputIterator"></param>
        /// <param name="func"></param>
        /// <returns></returns>
        public static IInputIterator<T> FindIfNot<T>(IInputIterator<T> inputIterator, UnaryPredicate<T> func)
        {
            while (!inputIterator.IsEnd())
            {
                if (!func(inputIterator.Read()))
                {
                    return inputIterator;
                }

                inputIterator.MoveNext();
            }

            return inputIterator;
        }

        /// <summary>
        /// Finds the last sequence of elements in a certain range 
        /// If the inputIterator2 is end, return inputIterator2
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="inputIterator1">Requires IShallowClone</param>
        /// <param name="inputIterator2">Requires ICursor</param>
        /// <returns>-1 if not found and index of the first element of the last range</returns>
        public static IInputIterator<T> FindEnd<T>(IInputIterator<T> inputIterator1,
            IInputIterator<T> inputIterator2) where T:IEquatable<T>
        {
            return FindEnd(inputIterator1, inputIterator2, new Comparer<T>());
        }

        public static IInputIterator<T> FindEnd<T>(IInputIterator<T> inputIterator1,
          IInputIterator<T> inputIterator2, 
          IEqualityComparer<T> comparer)
        {

            if (inputIterator2.IsEnd())
            {
                return inputIterator2;
            }

            //first set the result to the end of input iterator1

            IInputIterator<T> result = GetShallowObject(inputIterator1) as IInputIterator<T>;
            result.End();

            IInputIterator<T> iterator2Begin = GetShallowObject(inputIterator2) as IInputIterator<T>;

            //find the last index of the element in input iterator 2

            int input2Index = (GetLength(iterator2Begin) - 1);

            iterator2Begin.Begin();

            while (true)
            {
                //remember the current position of the input iterator 1
                //before we put it into Search method, search method change
                //the input iterator
                IInputIterator<T> input1Clone = GetShallowObject(inputIterator1) as IInputIterator<T>;

                IInputIterator<T> newResult = Search(inputIterator1, iterator2Begin,comparer);

                //when the Search method result IsEnd() iterator , it mean 
                //the element is not found
                if (newResult.IsEnd())
                {
                    break;
                }
                else
                {
                    //get the last position of the input iterator1 
                    //which doesn't have the element

                    ICursor endRangeCursor = GetCursor(input1Clone);

                    //caluclate the first element index of last range
                    // Note : last element in the range index - input iterator 2 last index

                    int position = Distance(input2Index, endRangeCursor);

                    //get the result cursor in order to change the position

                    ICursor resultCursor = GetCursor(result);

                    //change the position to the first element of the last range

                    resultCursor.SetPosition(position);

                    //move the input iterator to next element

                    inputIterator1.MoveNext();
                }

                iterator2Begin.Begin();
            }

            return result;
        }


        /// <summary>
        /// Find the first element which is in both input iterator
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="inputIterator1"></param>
        /// <param name="inputIterator2"></param>
        /// <param name="output">Found data on success and default otherwise </param>
        /// <returns>true if the data is found or false otherwise</returns>
        public static bool FindFirstOf<T>(IInputIterator<T> inputIterator1,
            IInputIterator<T> inputIterator2,out T output) where T : IEquatable<T>
        {
            return FindFirstOf(inputIterator1, inputIterator2, out output, new Comparer<T>());
        }

        public static bool FindFirstOf<T>(IInputIterator<T> inputIterator1,
          IInputIterator<T> inputIterator2, out T output,IEqualityComparer<T> comparer)
        {
            output = default(T);

            while (!inputIterator1.IsEnd())
            {
                while (!inputIterator2.IsEnd())
                {
                    T tmp = inputIterator1.Read();

                    if (comparer.Equals(tmp,inputIterator2.Read()))
                    {
                        output = tmp;

                        return true;
                    }

                    inputIterator2.MoveNext();
                }

                inputIterator2.Begin();

                inputIterator1.MoveNext();
            }

            return false;
        }




        /// <summary>
        /// Return the adjacent value.
        /// 
        /// Simpler example : Return where iterator[index]==iterator[index+1]
        /// 
        /// Example:
        /// 
        /// CharInputIterator = {xrqweeyt}
        /// 
        /// return e;
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="inputIterator"></param>
        /// <returns>Ajacent value or last value if there is no ajacent value</returns>
        public static T AdjacentFind<T>(IInputIterator<T> inputIterator) where T : IEquatable<T>
        {
            return AdjacentFind(inputIterator, new Comparer<T>());
        }

        public static T AdjacentFind<T>(IInputIterator<T> inputIterator,IEqualityComparer<T> comparer)
        {
            T first = inputIterator.Read();

            inputIterator.MoveNext();

            T next = first;

            while (!inputIterator.IsEnd())
            {
                next = inputIterator.Read();

                if (comparer.Equals(first,next))
                {
                    return first;
                }

                first = next;

                inputIterator.MoveNext();
            }

            return next;
        }



        /// <summary>
        /// 
        /// Return the first value of the IInputIterator1 which is in IInputIterator2.
        /// 
        /// If they don't have the same value,return the IsEnd() of IInputIterator1
        //   
        /// Example :
        /// 
        /// IntInputIterator 1 = {1,2,3,4,5,7,8};
        /// IntInputIterator 2 = {4,5,79,1,23,45};
        /// 
        /// return 1
        /// 
        /// IntInputIterator 1 = {1,2,3,4,5,7,8};
        /// IntInputIterator 2 = {77,78,79,80,81,45};
        /// 
        /// return IsEnd();
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="forwardIterator1">Requires IShallowClone</param>
        /// <param name="forwardIterator2">Requires IShallowClone</param>
        /// <returns></returns>
        public static IInputIterator<T> Search<T>(IInputIterator<T> inputIterator1,
            IInputIterator<T> inputIterator2) where T : IEquatable<T>
        {
            return Search(inputIterator1, inputIterator2, new Comparer<T>());
        }

        
        public static IInputIterator<T> Search<T>(IInputIterator<T> inputIterator1,
          IInputIterator<T> inputIterator2,IEqualityComparer<T> comparer)
        {
            IInputIterator<T> result = inputIterator1;

            IInputIterator<T> input2Clone = GetShallowObject(inputIterator2) as IInputIterator<T>;

            while (!inputIterator1.IsEnd())
            {
                //result = GetShallowObject(inputIterator1) as IInputIterator<T>;

                result = inputIterator1;

                T firstIter = inputIterator1.Read();

                while (!input2Clone.IsEnd())
                {
                    T compareData = input2Clone.Read();

                    if (comparer.Equals(firstIter,compareData))
                    {
                        return inputIterator1;
                    }

                    input2Clone.MoveNext();

                }

                input2Clone = GetShallowObject(inputIterator2) as IInputIterator<T>;

                inputIterator1.MoveNext();

            }

            return result;
        }
        




        /// <summary>
        /// Search the item N time if it is in the sequence
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="inputIterator"></param>
        /// <param name="count">How many item to search</param>
        /// <param name="searchValue">Item to search in the sequence</param>
        /// <returns>First Index found or -1 if the item is not found</returns>
        public static int SearchN<T>(IInputIterator<T> inputIterator, int count,
            T searchValue) where T : IEquatable<T>
        {
            return SearchN(inputIterator, count,searchValue,new Comparer<T>());
        }

        public static int SearchN<T>(IInputIterator<T> inputIterator, int count,
            T searchValue,IEqualityComparer<T> comparer)
        {
            int currentIndex = 0;

            while (!inputIterator.IsEnd())
            {
                int currentCount = currentIndex + 1;

                //maximum length reach
                if (currentCount == count)
                {
                    return -1;
                }

                if (comparer.Equals(inputIterator.Read(),searchValue))
                {
                    return currentIndex;
                }

                ++currentIndex;

                inputIterator.MoveNext();
            }

            //if we reach the end of the list we return -1
            return -1;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="inputIterator"></param>
        /// <param name="count"></param>
        /// <param name="func"></param>
        /// <returns></returns>
        public static int SearchN<T>(IInputIterator<T> inputIterator, int count,
            UnaryPredicate<T> func)
        {
            int currentIndex = 0;

            while (!inputIterator.IsEnd())
            {
                int currentCount = currentIndex + 1;

                //maximum length reach
                if (currentCount == count)
                {
                    return -1;
                }

                if (func(inputIterator.Read()))
                {
                    return currentIndex;
                }

                ++currentIndex;

                inputIterator.MoveNext();
            }

            //if we reach the end of the list we return -1
            return -1;
        }

    }

    #endregion

    #region Modifying 

    public sealed partial class Algorithm
    {
        /// <summary>
        /// Copy data from InputIterator to OutputIterator
        /// Data from InputIterator and OutputIterator have same type
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="fromIterator">Iterator which data will be read from</param>
        /// <param name="toIterator">Iterator which data will be written to</param>
        public static void Copy<T>(IInputIterator<T> fromIterator, IOutputIterator<T> toIterator)
        {
            while (!fromIterator.IsEnd())
            {
                toIterator.Write(fromIterator.Read());

                fromIterator.MoveNext();

                toIterator.MoveNext();
            }

        }


        /// <summary>
        /// Copy data from InputIterator to OutputIterator
        /// Data from InputIterator and OutputIterator have same type
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="fromIterator">Iterator which data will be read from</param>
        /// <param name="toIterator">Iterator which data will be written to</param>
        /// <param name="func"></param>
        public static void CopyIf<T>(IInputIterator<T> fromIterator,
            IOutputIterator<T> toIterator,
            UnaryPredicate<T> func)
        {
            while (!fromIterator.IsEnd())
            {
                if (func(fromIterator.Read()))
                {
                    toIterator.Write(fromIterator.Read());
                }

                fromIterator.MoveNext();

                toIterator.MoveNext();
            }

        }

        /// <summary>
        /// Copies a number of elements to a new location 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="fromIterator"></param>
        /// <param name="count"></param>
        /// <param name="toIterator"></param>
        public static void CopyN<T>(IInputIterator<T> fromIterator, 
            int count,
            IOutputIterator<T> toIterator)
        {
            int currentCount = 0;

            while(!fromIterator.IsEnd())
            {
                if(currentCount==count)
                {
                    return;
                }

                toIterator.Write(fromIterator.Read());

                ++currentCount;

                fromIterator.MoveNext();

                toIterator.MoveNext();
            }
        }

        /// <summary>
        /// Copies elements in backwards order 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="fromIterator"></param>
        /// <param name="toIterator"></param>
        public static void CopyBackward<T>(IBidirectionalIterator<T> fromIterator,
            IOutputIterator<T> toIterator)
        {
            int length=GetLength(fromIterator);

            while(length>0)
            {
                --length;

                fromIterator.MovePrev();

                toIterator.Write(fromIterator.Read());

            }
        }

        /*
        /// <summary>
        /// Remove an element from the input iterator
        /// when the output iterator use it
        /// 
        /// In C# there is no concept of "move" , therefore the replacing the element is used
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="fromIterator"></param>
        /// <param name="toIterator"></param>
        /// <param name="defaultElement">Element to replace the move element in input iterator when used</param>
        public static void Move<T>(IForwardIterator<T> fromIterator,
            IOutputIterator<T> toIterator,
            T defaultElement = default(T))
        {
                
            while(!fromIterator.IsEnd())
            {
                toIterator.Write(fromIterator.Read());

                fromIterator.Write(defaultElement);
                fromIterator.MoveNext();

                toIterator.MoveNext();
            }
            

        }

        /// <summary>
        /// Moves a range of elements to a new location in backwards order 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="fromIterator"></param>
        /// <param name="toIterator"></param>
        /// <param name="defaultElement"></param>
        public static void MoveBackward<T>(IBidirectionalIterator<T> fromIterator,
            IOutputIterator<T> toIterator,
            T defaultElement = default(T))
        {
            int length = GetLength(fromIterator);

            while(length>0)
            {
                toIterator.Write(fromIterator.Read());

                fromIterator.Write(defaultElement);
                fromIterator.MovePrev();

                toIterator.MoveNext();

                --length;
            }
        }

        */

        /// <summary>
        ///  Move an element from the input iterator
        ///  and transfer(remove when used) it to the output iterator
        ///  
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="fromIterator">Requires IRemoveable</param>
        /// <param name="toIterator"></param>
        /// <returns>Forward iterator with data remove</returns>
        public static void Move<T>(IInputIterator<T> fromIterator,
            IOutputIterator<T> toIterator)
        {
            IRemoveable removeable = GetRemoveable(fromIterator);

            while (!fromIterator.IsEnd())
            {
                toIterator.Write(fromIterator.Read());

                removeable.Remove();
                fromIterator.MoveNext();

                toIterator.MoveNext();
            }
        }

        /// <summary>
        /// Move an element from the input iterator
        /// and transfer(remove when used) it to the output iterator 
        /// in backwards order 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="fromIterator">Requires IRemoveable</param>
        /// <param name="toIterator"></param>
        public static void MoveBackward<T>(IBidirectionalIterator<T> fromIterator,
            IOutputIterator<T> toIterator)
        {
            IRemoveable removeable = GetRemoveable(fromIterator);

            int length = GetLength(fromIterator);

            while (length > 0)
            {
                fromIterator.MovePrev();

                toIterator.Write(fromIterator.Read());

                removeable.Remove();

                toIterator.MoveNext();

                --length;
            }

        }



        /// <summary>
        /// Assign a given element to the iterator
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="iterator"></param>
        /// <param name="data"></param>
        public static void Fill<T>(IForwardIterator<T> inputIterator, T data)
        {
            while(!inputIterator.IsEnd())
            {
                inputIterator.Write(data);

                inputIterator.MoveNext();
            }
        }

        /// <summary>
        /// Assign a given element to the iterator N time
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="iterator"></param>
        /// <param name="data"></param>
        public static void FillN<T>(IForwardIterator<T> inputIterator, int count,T data)
        {
            int currentCount = 0;

            while(count>currentCount)
            {
                inputIterator.Write(data);
                inputIterator.MoveNext();

                ++currentCount;

            }

        }

        /// <summary>
        /// Applies a function to a input and write it
        /// with output iterator
        /// </summary>
        /// <typeparam name="Input"></typeparam>
        /// <typeparam name="Output"></typeparam>
        /// <param name="inputIterator"></param>
        /// <param name="outputIterator"></param>
        /// <param name="func"></param>
        public static void Transform<Input,Output>(IInputIterator<Input> inputIterator,
            IOutputIterator<Output> outputIterator,
            UnaryFunction<Input,Output> func)
        {
            while(!inputIterator.IsEnd())
            {
                outputIterator.Write(func(inputIterator.Read()));

                inputIterator.MoveNext();
                outputIterator.MoveNext();
            }
        }

        /// <summary>
        /// Assign the result of the function 
        /// </summary>
        public static void Generate<T>(IForwardIterator<T> inputIterator,
            GenerateFunction<T> func)
        {
            while(!inputIterator.IsEnd())
            {
                inputIterator.Write(func());
                inputIterator.MoveNext();
            }
        }

        /// <summary>
        /// Assign the result of the function  N time
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="inputIterator"></param>
        /// <param name="count"></param>
        /// <param name="func"></param>
        public static void GenerateN<T>(IForwardIterator<T> inputIterator,int count,
         GenerateFunction<T> func)
        {
            int currentCount = 0;

            while (count > currentCount)
            {
                inputIterator.Write(func());
                inputIterator.MoveNext();

                ++currentCount;

            }
        }

        /// <summary>
        /// Remove an element which have the value
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="inputIterator">Requires IRemoveable</param>
        /// <param name="value"></param>
        /// 
        public static void Remove<T>(IInputIterator<T> inputIterator,
            T value) where T : IEquatable<T>
        {
            Remove(inputIterator, value, new Comparer<T>());
        }

        public static void Remove<T>(IInputIterator<T> inputIterator,
         T value,IEqualityComparer<T> comparer)
        {
            IRemoveable removeable = GetRemoveable(inputIterator);

            while (!inputIterator.IsEnd())
            {
                if (comparer.Equals(value,inputIterator.Read()))
                {
                    removeable.Remove();
                }

                inputIterator.MoveNext();
            }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="inputIterator">Requires IRemoveable</param>
        /// <param name="func"></param>
        public static void RemoveIf<T>(IInputIterator<T> inputIterator,
            UnaryPredicate<T> func)
        {
            IRemoveable removeable = GetRemoveable(inputIterator);

            while (!inputIterator.IsEnd())
            {
                if (func(inputIterator.Read()))
                {
                    removeable.Remove();
                }

                inputIterator.MoveNext();
            }
            
        }

        /// <summary>
        /// Copy the data other than the data specific
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="inputIterator"></param>
        /// <param name="outputIterator"></param>
        /// <param name="value"></param>
        public static void RemoveCopy<T>(IInputIterator<T> inputIterator,
            IOutputIterator<T> outputIterator,T value) where T : IEquatable<T>
        {
            RemoveCopy(inputIterator, outputIterator, value, new Comparer<T>());
        }

        public static void RemoveCopy<T>(IInputIterator<T> inputIterator,
         IOutputIterator<T> outputIterator, T value,
         IEqualityComparer<T> comparer)
        {
            while (!inputIterator.IsEnd())
            {
                if (!comparer.Equals(value,inputIterator.Read()))
                {
                    outputIterator.Write(inputIterator.Read());
                }

                inputIterator.MoveNext();

                outputIterator.MoveNext();
            }
        }

        /// <summary>
        /// Copy the data other than the data specific
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="inputIterator"></param>
        /// <param name="outputIterator"></param>
        /// <param name="func"></param>
        public static void RemoveCopyIf<T>(IInputIterator<T> inputIterator,
            IOutputIterator<T> outputIterator,
            UnaryPredicate<T> func)
        {
            while (!inputIterator.IsEnd())
            {
                if (!func(inputIterator.Read()))
                {
                    outputIterator.Write(inputIterator.Read());
                }

                inputIterator.MoveNext();

                outputIterator.MoveNext();
            }
        }

        /// <summary>
        ///  Replaces all values satisfying specific criteria with another value 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="inputIterator"></param>
        /// <param name="oldValue"></param>
        /// <param name="newValue"></param>
        public static void Replace<T>(IForwardIterator<T> inputIterator,
            T oldValue,T newValue) where T : IEquatable<T>
        {
            Replace(inputIterator, oldValue, newValue, new Comparer<T>());
        }

        public static void Replace<T>(IForwardIterator<T> inputIterator,
          T oldValue, T newValue,IEqualityComparer<T> comparer)
        {
            while (!inputIterator.IsEnd())
            {
                if (comparer.Equals(oldValue,inputIterator.Read()))
                {
                    inputIterator.Write(newValue);
                }

                inputIterator.MoveNext();
            }
        }

        /// <summary>
        ///  Replaces all values satisfying specific criteria with another value 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="inputIterator"></param>
        /// <param name="func"></param>
        /// <param name="newValue"></param>
        public static void ReplaceIf<T>(IForwardIterator<T> inputIterator,
           UnaryPredicate<T> func, T newValue)
        {
            while (!inputIterator.IsEnd())
            {
                if (func(inputIterator.Read()))
                {
                    inputIterator.Write(newValue);
                }

                inputIterator.MoveNext();
            }
        }

        /// <summary>
        /// Copy the data into the output iterator.
        /// If the input data is the same as oldValue,
        /// it replace the output data to newValue.
        /// 
        /// Note : The input iterator value is not changed
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="inputIterator"></param>
        /// <param name="outputIterator"></param>
        /// <param name="oldValue"></param>
        /// <param name="newValue"></param>
        public static void ReplaceCopy<T>(IInputIterator<T> inputIterator,
            IOutputIterator<T> outputIterator,
            T oldValue, T newValue) where T : IEquatable<T>
        {
            ReplaceCopy(inputIterator, outputIterator, oldValue,
                newValue, new Comparer<T>());
        }

        public static void ReplaceCopy<T>(IInputIterator<T> inputIterator,
         IOutputIterator<T> outputIterator,
         T oldValue, T newValue,IEqualityComparer<T> comparer)
        {
            while (!inputIterator.IsEnd())
            {
                T currentValue = inputIterator.Read();

                T outputValue = comparer.Equals(oldValue,currentValue) ? newValue : currentValue;

                outputIterator.Write(outputValue);

                inputIterator.MoveNext();

                outputIterator.MoveNext();
            }
        }

        /// <summary>
        /// Copy the data into the output iterator
        /// If the input data satisfy  specific criteria
        /// it replace the output data to newValue.
        /// 
        /// Note : The input iterator value is not changed
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="inputIterator"></param>
        /// <param name="outputIterator"></param>
        /// <param name="func"></param>
        /// <param name="newValue"></param>
        public static void ReplaceCopyIf<T>(IInputIterator<T> inputIterator,
             IOutputIterator<T> outputIterator,
             UnaryPredicate<T> func,T newValue)
        {
            while (!inputIterator.IsEnd())
            {

                T currentValue = inputIterator.Read();

                T outputValue = func(currentValue) ? newValue : currentValue;

                outputIterator.Write(outputValue);

                inputIterator.MoveNext();

                outputIterator.MoveNext();
            }
        }

        /// <summary>
        /// Swap a current element pointed by two iterators
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="inputIterator1"></param>
        /// <param name="inputIterator2"></param>
        public static void IterSwap<T>(IForwardIterator<T> inputIterator1,
            IForwardIterator<T> inputIterator2)
        {
            T tmp = inputIterator1.Read();

            inputIterator1.Write(inputIterator2.Read());

            inputIterator2.Write(tmp);
        }

        /// <summary>
        /// Swap the element
        /// The input iterator range must have same size 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="inputIterator1"></param>
        /// <param name="inputIterator2"></param>
        public static void SwapRange<T>(IForwardIterator<T> inputIterator1,
            IForwardIterator<T> inputIterator2)
        {
            while(!inputIterator1.IsEnd())
            {
                IterSwap(inputIterator1, inputIterator2);

                inputIterator1.MoveNext();

                inputIterator2.MoveNext();
            }
        }

        /// <summary>
        /// Reverses the order of elements in a range 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="inputIterator">Requires IPartialClone</param>
        public static void Reverse<T>(IBidirectionalIterator<T> inputIterator)
        {
            using (IBidirectionalIterator<T> cloneObj = GetPartialObject(inputIterator) as IBidirectionalIterator<T>)
            {
                int length = GetLength(cloneObj);

                cloneObj.MovePrev();

                while (!inputIterator.IsEnd() && length>0)
                {
                    //it swap with reverse due to cloneObj is pointing to 
                    //last element due to GetLength() method

                    IterSwap(inputIterator, cloneObj);

                    inputIterator.MoveNext();

                    cloneObj.MovePrev();

                    --length;

                }
            }
        }

        /// <summary>
        /// Copy the data into output iterator in reverse order
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="inputIterator">Requires IPartialClone</param>
        /// <param name="outputIterator"></param>
        public static void ReverseCopy<T>(IBidirectionalIterator<T> inputIterator,
            IOutputIterator<T> outputIterator)
        {
            //we don't call Revese method method we want
            //to copy data into the output iterator while
            //data is being reversed

            using (IBidirectionalIterator<T> cloneObj = GetPartialObject(inputIterator) as IBidirectionalIterator<T>)
            {
                int length = GetLength(cloneObj);

                cloneObj.MovePrev();

                while (!inputIterator.IsEnd() && length > 0)
                {
                    IterSwap(inputIterator, cloneObj);

                    outputIterator.Write(inputIterator.Read());
                    outputIterator.MoveNext();

                    inputIterator.MoveNext();

                    cloneObj.MovePrev();

                    --length;

                }
            }

        }

        /// <summary>
        /// Rotates the order of elements in a range 
        /// Put the element starting from the firstElement until end
        /// to the start of the iterator, then copy the remaining list
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="inputIterator">Require IShallowClone,ICursor</param>
        /// <param name="firstElementIndex"></param>
        public static void Rotate<T>(IForwardIterator<T> inputIterator,int firstElementIndex)
        {
            IForwardIterator<T> cloneBegin = GetShallowObject(inputIterator) as IForwardIterator<T>;

            Next(cloneBegin, firstElementIndex);

            Rotate(inputIterator, cloneBegin);

        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="inputIterator">Require IShallowClone,ICursor</param>
        /// <param name="rotateBegin">Require IShallowClone,ICursor</param>
        public static void Rotate<T>(IForwardIterator<T> inputIterator, IForwardIterator<T> rotateBegin)
        {
            ICursor inputCursor = GetCursor(inputIterator);

            ICursor cloneBeginCursor = GetCursor(rotateBegin);

            if (inputCursor.GetPosition() == cloneBeginCursor.GetPosition())
            {
                return;
            }

            if (rotateBegin.IsEnd())
            {
                return;
            }

            IForwardIterator<T> nextClone = GetShallowObject(rotateBegin) as IForwardIterator<T>;

            ICursor nextCursor = GetCursor(nextClone);

            do
            {
                IterSwap(inputIterator, nextClone);

                inputIterator.MoveNext();

                nextClone.MoveNext();

                if (inputCursor.GetPosition() == cloneBeginCursor.GetPosition())
                {
                    cloneBeginCursor.SetPosition(nextCursor.GetPosition());
                }
            }
            while (!nextClone.IsEnd());

            nextCursor.SetPosition(cloneBeginCursor.GetPosition());

            while (!nextClone.IsEnd())
            {
                IterSwap(inputIterator, nextClone);

                inputIterator.MoveNext();

                nextClone.MoveNext();

                if (inputCursor.GetPosition() == cloneBeginCursor.GetPosition())
                {
                    cloneBeginCursor.SetPosition(nextCursor.GetPosition());
                }
                else if (nextClone.IsEnd())
                {
                    nextCursor.SetPosition(cloneBeginCursor.GetPosition());
                }
            }

        }

        /// <summary>
        /// Rotates the order of elements in a range and copy into output iterator
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="inputIterator">Require IShallowClone,ICursor</param>
        /// <param name="firstElementIndex"></param>
        /// <param name="outputIterator"></param>
        public static void RotateCopy<T>(IInputIterator<T> inputIterator,
            int firstElementIndex,
            IOutputIterator<T> outputIterator)
        {
            IInputIterator<T> rotateBeginClone = GetShallowObject(inputIterator) as IInputIterator<T>;

            ICursor middleCursor = GetCursor(rotateBeginClone);

            Next(rotateBeginClone, (firstElementIndex - 1));

            RotateCopy(inputIterator, rotateBeginClone, outputIterator);

        }

        public static void RotateCopy<T>(IInputIterator<T> inputIterator,
            IInputIterator<T> rotateBegin,
            IOutputIterator<T> outputIterator)
        {
            ICursor middleCursor = GetCursor(rotateBegin);

            int middlePosition = middleCursor.GetPosition();

            Copy(rotateBegin, outputIterator);

            middleCursor.SetPosition(middlePosition);

            ICursor inputCursor = GetCursor(inputIterator);

            int count = Distance(inputCursor, middleCursor);

            while (count > 0)
            {
                outputIterator.Write(inputIterator.Read());
                outputIterator.MoveNext();

                inputIterator.MoveNext();

                --count;
            }

        }

        public static void RandomShuffle<T>(IForwardIterator<T> inputIterator)
        {
            UnaryFunction<int, int> func = delegate (int maxLength) 
            {
                return new Random().Next(0, maxLength - 1);
            };

            RandomShuffle(inputIterator, func);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="inputIterator">Requires IShallowClone,ICursor</param>
        /// <param name="func">Which take max length and return index position</param>
        public static void RandomShuffle<T>(IForwardIterator<T> inputIterator, 
            UnaryFunction<int,int> func)
        {
            IForwardIterator<T> cloneObj = GetShallowObject(inputIterator) as IForwardIterator<T>;

            ICursor cloneCursor = GetCursor(cloneObj);

            int maxLength = GetLength(inputIterator);

            inputIterator.Begin();

            while(!inputIterator.IsEnd())
            {
                cloneCursor.SetPosition(func(maxLength));
                
                IterSwap(inputIterator, cloneObj);

                inputIterator.MoveNext();
            }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="inputIterator">Requires IShallowClone,ICursor</param>
        /// <param name="seed"></param>
        public static void Shuffle<T>(IForwardIterator<T> inputIterator,int seed)
        {
            UnaryFunction<int, int> func = delegate (int maxLength)
            {
                return new Random(seed).Next(0, maxLength - 1);
            };

            RandomShuffle(inputIterator, func);
        }

        /// <summary>
        ///  Removes consecutive duplicate elements in a range 
        ///  
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="inputIterator">Requires IPartialClone,IRemoveable </param>
        public static void Unique<T>(IForwardIterator<T> inputIterator) 
            where T : IEquatable<T>
        {
            Unique(inputIterator, new Comparer<T>());
        }

        public static void Unique<T>(IForwardIterator<T> inputIterator,IEqualityComparer<T> comparer)
        {
            if (inputIterator.IsEnd())
            {
                return;
            }

            IRemoveable removeable = GetRemoveable(inputIterator);

            using (IForwardIterator<T> cloneObj = GetPartialObject(inputIterator) as IForwardIterator<T>)
            {
                //in order to be +1 than the input iterator

                cloneObj.MoveNext();

                while (!(inputIterator.IsEnd() || cloneObj.IsEnd()))
                {
                    T currentValue = inputIterator.Read();

                    T nextValue = cloneObj.Read();

                    if (comparer.Equals(currentValue,nextValue))
                    {
                        //remove the current value

                        removeable.Remove();
                    }

                    inputIterator.MoveNext();

                    cloneObj.MoveNext();
                }
            }
        }


        /// <summary>
        /// Copy a data to an output iterator except consecutive duplicate elements
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="inputIterator">Requires IPartialClone</param>
        /// <param name="outputIterator"></param>
        public static void UniqueCopy<T>(IForwardIterator<T> inputIterator,
            IOutputIterator<T> outputIterator) where T : IEquatable<T>
        {
            UniqueCopy(inputIterator, outputIterator, new Comparer<T>());
        }

        public static void UniqueCopy<T>(IForwardIterator<T> inputIterator,
           IOutputIterator<T> outputIterator,IEqualityComparer<T> comparer)
        {
            if (inputIterator.IsEnd())
            {
                return;
            }

            using (IForwardIterator<T> cloneObj = GetPartialObject(inputIterator) as IForwardIterator<T>)
            {
                //in order to be +1 than the input iterator
                cloneObj.MoveNext();

                T currentValue = default(T);

                T nextValue = default(T);

                while (!(inputIterator.IsEnd() || cloneObj.IsEnd()))
                {
                    currentValue = inputIterator.Read();

                    nextValue = cloneObj.Read();

                    if (!comparer.Equals(currentValue,nextValue))
                    {
                        outputIterator.Write(currentValue);
                    }

                    inputIterator.MoveNext();

                    cloneObj.MoveNext();

                    outputIterator.MoveNext();
                }

                //check whether there is still
                //element in input iterator
                if (!inputIterator.IsEnd())
                {
                    // last element and last-1 element are not same

                    if (!comparer.Equals(currentValue,nextValue))
                    {
                        //now we write the last element

                        outputIterator.Write(nextValue);
                    }
                }



            }
        }


    }

    #endregion

    #region Partition

    public sealed partial class Algorithm
    {

        /// <summary>
        /// Check whether all the element which satisfy
        /// the predict appear before all that don't.
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="inputIterator"></param>
        /// <param name="func"></param>
        /// <returns>True=if the element are partion and the list is empty
        ///          False= if the list is empty or the the element are not partion   
        /// </returns>
        public static bool IsPartitioned<T>(IInputIterator<T> inputIterator,
            UnaryPredicate<T> func)
        {
            while (!inputIterator.IsEnd())
            {
                if (!func(inputIterator.Read()))
                {
                    break;
                }

                inputIterator.MoveNext();
            }

            while (!inputIterator.IsEnd())
            {
                if (func(inputIterator.Read()))
                {
                    return false;
                }

                inputIterator.MoveNext();
            }

            return true;
        }

        /// <summary>
        /// Divide a range of element into two groups
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="inputIterator">Requires IShallowClone</param>
        /// <param name="func"></param>
        public static void Partition<T>(IForwardIterator<T> inputIterator,
            UnaryPredicate<T> func)
        {
            //FindIfNot method put the input iterator to 
            //the first element which does not satisfy the predicate

            if (FindIfNot(inputIterator, func).IsEnd())
            {
                inputIterator.Begin();

                return;
            }

            IForwardIterator<T> cloneObj = GetShallowObject(inputIterator) as IForwardIterator<T>;

            cloneObj.MoveNext();

            while (!cloneObj.IsEnd())
            {
                if (func(cloneObj.Read()))
                {
                    IterSwap(inputIterator, cloneObj);

                    inputIterator.MoveNext();
                }

                cloneObj.MoveNext();
            }
        }

        /// <summary>
        /// Divide a range of element into two groups for n time
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="inputIterator"></param>
        /// <param name="func"></param>
        /// <param name="count"></param>
        public static void Partition<T>(IForwardIterator<T> inputIterator, int count,
            UnaryPredicate<T> func)
        {
            if(count<=0)
            {
                return;
            }

            //FindIfNot method put the input iterator to 
            //the first element which does not satisfy the predicate

            if (FindIfNot(inputIterator, func).IsEnd())
            {
                inputIterator.Begin();

                return;
            }

            IForwardIterator<T> cloneObj = GetShallowObject(inputIterator) as IForwardIterator<T>;

            cloneObj.MoveNext();

            while (!cloneObj.IsEnd() && count>0)
            {
                if (func(cloneObj.Read()))
                {
                    IterSwap(inputIterator, cloneObj);

                    inputIterator.MoveNext();
                }

                cloneObj.MoveNext();

                --count;
            }
        }

        public static void PartitionCopy<T>(IForwardIterator<T> inputIterator,
            IOutputIterator<T> trueOutputIterator,
            IOutputIterator<T> falseOutputIterator,
            UnaryPredicate<T> func)
        {
            while(!inputIterator.IsEnd())
            {
                T currentData = inputIterator.Read();

                if(func(currentData))
                {
                    trueOutputIterator.Write(currentData);
                    trueOutputIterator.MoveNext();
                }
                else
                {
                    falseOutputIterator.Write(currentData);
                    falseOutputIterator.MoveNext();
                }

                inputIterator.MoveNext();
            }
        }


    }


    #endregion

    #region Sorting

    public sealed partial class Algorithm
    {
        /// <summary>
        /// Check whether a range is sorted into ascending order 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="inputIterator"></param>
        /// <returns>True=is sorted or end of range , False=not sorted</returns>
        public static bool IsSorted<T>(IInputIterator<T> inputIterator) where T:IComparable<T>
        {
            T prev = inputIterator.Read();

            while(!inputIterator.IsEnd())
            {
                inputIterator.MoveNext();

                if(inputIterator.IsEnd())
                {
                    break;
                }

                T current = inputIterator.Read();

                if(current.CompareTo(prev) <0)
                {
                    return false;
                }

                prev = current;
            }

            return true;
        }

        public static bool IsSorted<T>(IInputIterator<T> inputIterator,IComparer<T> comparer)
        {
            T prev = inputIterator.Read();

            while (!inputIterator.IsEnd())
            {
                inputIterator.MoveNext();

                if (inputIterator.IsEnd())
                {
                    break;
                }

                T current = inputIterator.Read();

                if (comparer.Compare(current,prev)< 0)
                {
                    return false;
                }

                prev = current;
            }

            return true;
        }

        public static bool IsSortedUntil<T>(IInputIterator<T> inputIterator,int count) where T : IComparable<T>
        {
            T prev = inputIterator.Read();

            while (!inputIterator.IsEnd() && count>0)
            {
                inputIterator.MoveNext();

                if (inputIterator.IsEnd())
                {
                    break;
                }

                T current = inputIterator.Read();

                if (current.CompareTo(prev) < 0)
                {
                    return false;
                }

                prev = current;

                --count;
            }

            return true;
        }

        public static bool IsSortedUntil<T>(IInputIterator<T> inputIterator, int count,
            IComparer<T> comparer)
        {
            T prev = inputIterator.Read();

            count -= 1;

            while (!inputIterator.IsEnd() && count > 0)
            {
                inputIterator.MoveNext();

                if (inputIterator.IsEnd())
                {
                    break;
                }

                T current = inputIterator.Read();

                if (comparer.Compare(current,prev) < 0)
                {
                    return false;
                }

                prev = current;
            }

            return true;
        }


        /// <summary>
        /// Sort by ascending order using quick sort
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="inputIterator">Requires ICursor,IShallowClone</param>
        public static void Sort<T>(IForwardIterator<T> inputIterator) where T : IComparable<T>
        {
            IForwardIterator<T> endIterator = GetShallowObject(inputIterator) as IForwardIterator<T>;
            endIterator.End();

            QuickSort(inputIterator, endIterator);
        }


        public static void Sort<T>(IForwardIterator<T> inputIterator,IComparer<T> comparer)
        {

            IForwardIterator<T> endIterator = GetShallowObject(inputIterator) as IForwardIterator<T>;
            endIterator.End();

            QuickSort(inputIterator, endIterator,comparer);
        }



        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="beginIterator">Requires ICursor,IShallowClone</param>
        /// <param name="endIterator">Requires ICursor,IShallowClone
        /// The index should be set at End and it should be the shallow copy of the 
        /// beginIterator</param>
        private static void QuickSort<T>(IForwardIterator<T> beginIterator,
           IForwardIterator<T> endIterator) where T : IComparable<T>
        {
            ICursor beginCursor = GetCursor(beginIterator);

            ICursor endCursor = GetCursor(endIterator);

            int count = Distance(beginCursor, endCursor);

            bool isEnd = (count <= 1);

            if (isEnd)
            {
                return;
            }

            T pivotValue = beginIterator.Read();

            IForwardIterator<T> pivot = GetShallowObject(beginIterator) as IForwardIterator<T>;

            Partition(pivot, count, delegate (T value)
            {
                return (value.CompareTo(pivotValue) <= 0);
            });

            ICursor pivotCursor = GetCursor(pivot);

            //put the cursor to the -1 index of the pivot element

            pivotCursor.SetPosition(pivotCursor.GetPosition() - 1);

            //We must -1 before swap , otherwise we will get stackoverflow
            //since the method don't exit
            IterSwap(beginIterator, pivot);

            //Sort the left side of the partion element

            QuickSort(beginIterator, pivot);

            //put the cursor to the pivot element

            pivotCursor.SetPosition(pivotCursor.GetPosition() + 1);

            //Sort the right side of the partion element

            QuickSort(pivot, endIterator);

        }


        private static void QuickSort<T>(IForwardIterator<T> beginIterator,
        IForwardIterator<T> endIterator,IComparer<T> comparer)
        {
            ICursor beginCursor = GetCursor(beginIterator);

            ICursor endCursor = GetCursor(endIterator);

            int count = Distance(beginCursor, endCursor);

            bool isEnd = (count <= 1);

            if (isEnd)
            {
                return;
            }

            T pivotValue = beginIterator.Read();

            IForwardIterator<T> pivot = GetShallowObject(beginIterator) as IForwardIterator<T>;

            Partition(pivot, count, delegate (T value)
            {
                return (comparer.Compare(value, pivotValue) <= 0);
            });

            ICursor pivotCursor = GetCursor(pivot);

            //put the cursor to the -1 index of the pivot element

            pivotCursor.SetPosition(pivotCursor.GetPosition() - 1);

            //We must -1 before swap , otherwise we will get stackoverflow
            //since the method don't exit
            IterSwap(beginIterator, pivot);

            //Sort the left side of the partion element

            QuickSort(beginIterator, pivot,comparer);

            //put the cursor to the pivot element

            pivotCursor.SetPosition(pivotCursor.GetPosition() + 1);

            //Sort the right side of the partion element

            QuickSort(pivot, endIterator,comparer);

        }


    }
    #endregion

    #region SortedRange

    public sealed partial class Algorithm
    {
        /// <summary>
        /// Return first element not less than the given value
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="inputIterator">Requires sorted range,ICursor,IShallowClone</param>
        /// <returns></returns>
        public static IInputIterator<T> LowerBound<T>(IInputIterator<T> inputIterator,T value) where T:IComparable<T>
        {
            ICursor cursor = GetCursor(inputIterator);

            IInputIterator<T> result = GetShallowObject(inputIterator) as IInputIterator<T>;

            int index = -1;

            int count = GetLength(inputIterator);

            inputIterator.Begin();

            while (count > 0)
            {
                int step = count / 2;

                T currentValue = Next(inputIterator, step);

                if(currentValue.CompareTo(value)<0)
                {
                    inputIterator.MoveNext();

                    index = cursor.GetPosition();

                    result = GetShallowObject(inputIterator) as IInputIterator<T>;

                    count -= step + 1;
                }
                else
                {
                    count = step;
                }

            }

            if(index>=0)
            {
                cursor.SetPosition(index);

                result = GetShallowObject(inputIterator) as IInputIterator<T>;
            }

            return result;
        }

        public static IInputIterator<T> LowerBound<T>(IInputIterator<T> inputIterator, T value,
            IComparer<T> comparer)
        {
            ICursor cursor = GetCursor(inputIterator);

            IInputIterator<T> result = GetShallowObject(inputIterator) as IInputIterator<T>;

            int index = -1;

            int count = GetLength(inputIterator);

            inputIterator.Begin();

            while (count > 0)
            {
                int step = count / 2;

                T currentValue = Next(inputIterator, step);

                if (comparer.Compare(currentValue,value) < 0)
                {
                    inputIterator.MoveNext();

                    index = cursor.GetPosition();

                    result = GetShallowObject(inputIterator) as IInputIterator<T>;

                    count -= step + 1;
                }
                else
                {
                    count = step;
                }

            }

            if (index >= 0)
            {
                cursor.SetPosition(index);

                result = GetShallowObject(inputIterator) as IInputIterator<T>;
            }

            return result;
        }

        /// <summary>
        /// Returns an iterator to the first element greater than a certain value 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="inputIterator">Requires sorted range,ICursor</param>
        /// <returns></returns>
        public static IInputIterator<T> UpperBound<T>(IInputIterator<T> inputIterator, T value) where T : IComparable<T>
        {
            ICursor cursor = GetCursor(inputIterator);

            IInputIterator<T> result = GetShallowObject(inputIterator) as IInputIterator<T>;

            int index = -1;

            int count = GetLength(inputIterator);

            inputIterator.Begin();

            while (count > 0)
            {
                int step = count / 2;

                T currentValue = Next(inputIterator, step);

                //working one (!(value.CompareTo(currentValue) < 0))

                if (currentValue.CompareTo(value)>0)
                {
                    index = cursor.GetPosition();

                    result = GetShallowObject(inputIterator) as IInputIterator<T>;

                    count -= step + 1;
                }
                else
                {
                    count = step;
                }

            }

            if (index >= 0)
            {
                cursor.SetPosition(index);

                result = GetShallowObject(inputIterator) as IInputIterator<T>;
            }

            return result;
        }

        public static IInputIterator<T> UpperBound<T>(IInputIterator<T> inputIterator, T value,
            IComparer<T> comparer)
        {
            ICursor cursor = GetCursor(inputIterator);

            IInputIterator<T> result = GetShallowObject(inputIterator) as IInputIterator<T>;

            int index = -1;

            int count = GetLength(inputIterator);

            inputIterator.Begin();

            while (count > 0)
            {
                int step = count / 2;

                T currentValue = Next(inputIterator, step);

                //working one (!(value.CompareTo(currentValue) < 0))

                if (comparer.Compare(currentValue,value) > 0)
                {
                    index = cursor.GetPosition();

                    result = GetShallowObject(inputIterator) as IInputIterator<T>;

                    count -= step + 1;
                }
                else
                {
                    count = step;
                }

            }

            if (index >= 0)
            {
                cursor.SetPosition(index);

                result = GetShallowObject(inputIterator) as IInputIterator<T>;
            }

            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="inputIterator">Requires sorted range,ICursor</param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool BinarySearch<T>(IInputIterator<T> inputIterator,T value) where T : IComparable<T>
        {
            IInputIterator<T> result=LowerBound(inputIterator, value);

            return (!result.IsEnd() && value.CompareTo(result.Read()) == 0); 
        }

        public static bool BinarySearch<T>(IInputIterator<T> inputIterator, T value,IComparer<T> comparer)
        {
            IInputIterator<T> result = LowerBound(inputIterator, value,comparer);

            return (!result.IsEnd() && comparer.Compare(value,result.Read()) == 0);
        }

        /// <summary>
        /// Return lower bound index as key and 
        /// upper bound index as value
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="inputIterator">Requires sorted range,ICursor</param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static KeyValuePair<int,int> EqualRange<T>(IInputIterator<T> inputIterator, T value) where T : IComparable<T>
        {
            ICursor cursor = GetCursor(inputIterator);

            LowerBound(inputIterator, value);

            int lowerBoundIndex = cursor.GetPosition();

            inputIterator.Begin();

            UpperBound(inputIterator, value);

            int upperBoundIndex = cursor.GetPosition();

            return new KeyValuePair<int,int>(lowerBoundIndex, upperBoundIndex);
        }

        public static KeyValuePair<int, int> EqualRange<T>(IInputIterator<T> inputIterator, T value,
            IComparer<T> comparer)
        {
            ICursor cursor = GetCursor(inputIterator);

            LowerBound(inputIterator, value,comparer);

            int lowerBoundIndex = cursor.GetPosition();

            inputIterator.Begin();

            UpperBound(inputIterator, value,comparer);

            int upperBoundIndex = cursor.GetPosition();

            return new KeyValuePair<int, int>(lowerBoundIndex, upperBoundIndex);
        }

        /// <summary>
        /// Merge two iterators by ascending order and
        /// output it
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="inputIterator1">Requires sorted range</param>
        /// <param name="inputIterator2">Requires sorted range</param>
        /// <param name="outputIterator"></param>
        public static void Merge<T>(IInputIterator<T> inputIterator1,
            IInputIterator<T> inputIterator2,
            IOutputIterator<T> outputIterator) where T : IComparable<T>
        {
            //if one iterator is empty ,copy the other iterator

            if(inputIterator1.IsEnd())
            {
                Copy(inputIterator2, outputIterator);

                return;
            }

            if (inputIterator2.IsEnd())
            {
                Copy(inputIterator1, outputIterator);

                return;
            }

            while(!(inputIterator1.IsEnd() || inputIterator2.IsEnd()))
            {

                T input1 = inputIterator1.Read();

                T input2 = inputIterator2.Read();

                if(input2.CompareTo(input1)<0)
                {
                    outputIterator.Write(input2);

                    inputIterator2.MoveNext();
                }
                else
                {
                    outputIterator.Write(input1);

                    inputIterator1.MoveNext();
                }

                outputIterator.MoveNext();

            }

            //copy the remaining iterator

            if (inputIterator1.IsEnd())
            {
                Copy(inputIterator2, outputIterator);

                return;
            }

            if (inputIterator2.IsEnd())
            {
                Copy(inputIterator1, outputIterator);

                return;
            }

        }

        public static void Merge<T>(IInputIterator<T> inputIterator1,
        IInputIterator<T> inputIterator2,
        IOutputIterator<T> outputIterator,
        IComparer<T> comparer)
        {
            //if one iterator is empty ,copy the other iterator

            if (inputIterator1.IsEnd())
            {
                Copy(inputIterator2, outputIterator);

                return;
            }

            if (inputIterator2.IsEnd())
            {
                Copy(inputIterator1, outputIterator);

                return;
            }

            while (!(inputIterator1.IsEnd() || inputIterator2.IsEnd()))
            {

                T input1 = inputIterator1.Read();

                T input2 = inputIterator2.Read();

                if (comparer.Compare(input2,input1) < 0)
                {
                    outputIterator.Write(input2);

                    inputIterator2.MoveNext();
                }
                else
                {
                    outputIterator.Write(input1);

                    inputIterator1.MoveNext();
                }

                outputIterator.MoveNext();

            }

            //copy the remaining iterator

            if (inputIterator1.IsEnd())
            {
                Copy(inputIterator2, outputIterator);

                return;
            }

            if (inputIterator2.IsEnd())
            {
                Copy(inputIterator1, outputIterator);

                return;
            }

        }

        public static void InplaceMerge()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Check if input iterator 2 is the subset of input iterator 1
        /// Returns true if one set is a subset of another 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="inputIterator1">Requires sorted range</param>
        /// <param name="inputIterator2">Requires sorted range</param>
        public static bool Includes<T>(IInputIterator<T> inputIterator1,
            IInputIterator<T> inputIterator2) where T : IComparable<T>
        {
            while(!inputIterator2.IsEnd())
            {
                if (inputIterator1.IsEnd())
                {
                    return false;
                }

                T input1 = inputIterator1.Read();

                T input2 = inputIterator2.Read();

                if(input2.CompareTo(input1)<0)
                {
                    return false;
                }
                else if (input2.CompareTo(input1)==0)
                {
                    inputIterator2.MoveNext();
                }
                
                inputIterator1.MoveNext();
            }

            return true;
        }

        /// <summary>
        /// Check whether input iterator2 is the subset of input iterator1
        /// All the element in input iterator2 must be present in input iterator1
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="inputIterator1">Requires sorted range<</param>
        /// <param name="inputIterator2">Requires sorted range<</param>
        /// <param name="comparer"></param>
        /// <returns></returns>
        public static bool Includes<T>(IInputIterator<T> inputIterator1,
        IInputIterator<T> inputIterator2,
        IComparer<T> comparer)
        {
            while (!inputIterator2.IsEnd())
            {
                if (inputIterator1.IsEnd())
                {
                    return false;
                }

                T input1 = inputIterator1.Read();

                T input2 = inputIterator2.Read();

                if (comparer.Compare(input2,input1) < 0)
                {
                    return false;
                }
                else if (comparer.Compare(input2,input1) == 0)
                {
                    inputIterator2.MoveNext();
                }

                inputIterator1.MoveNext();
            }

            return true;
        }

        /// <summary>
        /// Computes the difference between two sets 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="inputIterator1">Requires sorted range<</param>
        /// <param name="inputIterator2">Requires sorted range<</param>
        /// <param name="outputIterator"></param>
        public static void SetDifference<T>(IInputIterator<T> inputIterator1,
            IInputIterator<T> inputIterator2,
            IOutputIterator<T> outputIterator) where T:IComparable<T>
        {
            
            if(inputIterator2.IsEnd())
            {
                Copy(inputIterator1, outputIterator);

                return;
            }

            while(!inputIterator1.IsEnd() && !inputIterator2.IsEnd())
            {
                T input1 = inputIterator1.Read();

                T input2 = inputIterator2.Read();

                if(input1.CompareTo(input2)<0)
                {
                    outputIterator.Write(input1);
                    outputIterator.MoveNext();

                    inputIterator1.MoveNext();
                }
                else
                {
                    if (input1.CompareTo(input2) == 0)
                    {
                        inputIterator1.MoveNext();
                    }

                    inputIterator2.MoveNext();
                }
            }

            //copy the remaining list 

            if (!inputIterator1.IsEnd())
            {
                Copy(inputIterator1, outputIterator);
            }
        }

        public static void SetDifference<T>(IInputIterator<T> inputIterator1,
        IInputIterator<T> inputIterator2,
        IOutputIterator<T> outputIterator,
        IComparer<T> comparer)
        {

            if (inputIterator2.IsEnd())
            {
                Copy(inputIterator1, outputIterator);

                return;
            }

            while (!inputIterator1.IsEnd() && !inputIterator2.IsEnd())
            {
                T input1 = inputIterator1.Read();

                T input2 = inputIterator2.Read();

                if (comparer.Compare(input1,input2)< 0)
                {
                    outputIterator.Write(input1);
                    outputIterator.MoveNext();

                    inputIterator1.MoveNext();
                }
                else
                {
                    if (comparer.Compare(input1,input2) == 0)
                    {
                        inputIterator1.MoveNext();
                    }

                    inputIterator2.MoveNext();
                }

            }

            //copy the remaining list 

            if (!inputIterator1.IsEnd())
            {
                Copy(inputIterator1, outputIterator);
            }
        }

        /// <summary>
        /// Output the element which is both present in two iterators
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="inputIterator1">Requires sorted range</param>
        /// <param name="inputIterator2">Requires sorted range</param>
        /// <param name="outputIterator"></param>
        public static void SetIntersection<T>(IInputIterator<T> inputIterator1,
            IInputIterator<T> inputIterator2,
            IOutputIterator<T> outputIterator) where T : IComparable<T>
        {
            while (!inputIterator1.IsEnd() && !inputIterator2.IsEnd())
            {
                T input1 = inputIterator1.Read();

                T input2 = inputIterator2.Read();

                if(input1.CompareTo(input2)<0)
                {
                    inputIterator1.MoveNext();
                }
                else
                {
                    if (input1.CompareTo(input2) == 0)
                    {
                        outputIterator.Write(input1);
                        outputIterator.MoveNext();

                        inputIterator1.MoveNext();
                    }

                    inputIterator2.MoveNext();
                }
            }
        }

        public static void SetIntersection<T>(IInputIterator<T> inputIterator1,
            IInputIterator<T> inputIterator2,
            IOutputIterator<T> outputIterator,
            IComparer<T> comparer)
        {
            while (!inputIterator1.IsEnd() && !inputIterator2.IsEnd())
            {
                T input1 = inputIterator1.Read();

                T input2 = inputIterator2.Read();

                if (comparer.Compare(input1,input2) < 0)
                {
                    inputIterator1.MoveNext();
                }
                else
                {
                    if (comparer.Compare(input1,input2) == 0)
                    {
                        outputIterator.Write(input1);
                        outputIterator.MoveNext();

                        inputIterator1.MoveNext();
                    }

                    inputIterator2.MoveNext();
                }
            }
        }


        /// <summary>
        /// Create a unique set from both iterators 
        /// The first iterator element are present first
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="inputIterator1"></param>
        /// <param name="inputIterator2"></param>
        /// <param name="outputIterator"></param>
        public static void SetSymmetricDifference<T>(IInputIterator<T> inputIterator1,
            IInputIterator<T> inputIterator2,
            IOutputIterator<T> outputIterator) where T : IComparable<T>
        {
            if(inputIterator2.IsEnd())
            {
                Copy(inputIterator1, outputIterator);
            }

            while (!inputIterator1.IsEnd() && !inputIterator2.IsEnd())
            {
                T input1 = inputIterator1.Read();

                T input2 = inputIterator2.Read();

                if(input1.CompareTo(input2)<0)
                {
                    outputIterator.Write(input1);
                    outputIterator.MoveNext();

                    inputIterator1.MoveNext();

                }
                else
                {
                    if(input2.CompareTo(input1)<0)
                    {
                        outputIterator.Write(input2);
                        outputIterator.MoveNext();
                    }
                    else
                    {
                        inputIterator1.MoveNext();
                    }

                    inputIterator2.MoveNext();
                }
            }

            if (!inputIterator2.IsEnd())
            {
                Copy(inputIterator2, outputIterator);
            }

        }

        public static void SetSymmetricDifference<T>(IInputIterator<T> inputIterator1,
            IInputIterator<T> inputIterator2,
            IOutputIterator<T> outputIterator,
            IComparer<T> comparer)
        {
            if (inputIterator2.IsEnd())
            {
                Copy(inputIterator1, outputIterator);
            }

            while (!inputIterator1.IsEnd() && !inputIterator2.IsEnd())
            {
                T input1 = inputIterator1.Read();

                T input2 = inputIterator2.Read();

                if (comparer.Compare(input1,input2) < 0)
                {
                    outputIterator.Write(input1);
                    outputIterator.MoveNext();

                    inputIterator1.MoveNext();

                }
                else
                {
                    if (comparer.Compare(input2,input1) < 0)
                    {
                        outputIterator.Write(input2);
                        outputIterator.MoveNext();
                    }
                    else
                    {
                        inputIterator1.MoveNext();
                    }

                    inputIterator2.MoveNext();
                }
            }

            if (!inputIterator2.IsEnd())
            {
                Copy(inputIterator2, outputIterator);
            }
        }

        /// <summary>
        /// Create a unique set from both iterators 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="inputIterator1"></param>
        /// <param name="inputIterator2"></param>
        /// <param name="outputIterator"></param>
        public static void SetUnion<T>(IInputIterator<T> inputIterator1,
            IInputIterator<T> inputIterator2,
            IOutputIterator<T> outputIterator) where T : IComparable<T>
        {
            //if one iterator is empty ,copy the other iterator

            if (inputIterator1.IsEnd())
            {
                Copy(inputIterator2, outputIterator);

                return;
            }

            if (inputIterator2.IsEnd())
            {
                Copy(inputIterator1, outputIterator);

                return;
            }

            while (!(inputIterator1.IsEnd() || inputIterator2.IsEnd()))
            {

                T input1 = inputIterator1.Read();

                T input2 = inputIterator2.Read();

                if (input2.CompareTo(input1) < 0)
                {
                    outputIterator.Write(input2);

                    inputIterator2.MoveNext();
                }
                else
                {
                    outputIterator.Write(input1);

                    if(input2.CompareTo(input1) == 0)
                    {
                        inputIterator2.MoveNext();
                    }

                    inputIterator1.MoveNext();
                }

                outputIterator.MoveNext();

            }

            //copy the remaining iterator

            if (inputIterator1.IsEnd())
            {
                Copy(inputIterator2, outputIterator);

                return;
            }

            if (inputIterator2.IsEnd())
            {
                Copy(inputIterator1, outputIterator);

                return;
            }
        }

        public static void SetUnion<T>(IInputIterator<T> inputIterator1,
          IInputIterator<T> inputIterator2,
          IOutputIterator<T> outputIterator,
          IComparer<T> comparer)
        {
            //if one iterator is empty ,copy the other iterator

            if (inputIterator1.IsEnd())
            {
                Copy(inputIterator2, outputIterator);

                return;
            }

            if (inputIterator2.IsEnd())
            {
                Copy(inputIterator1, outputIterator);

                return;
            }

            while (!(inputIterator1.IsEnd() || inputIterator2.IsEnd()))
            {

                T input1 = inputIterator1.Read();

                T input2 = inputIterator2.Read();

                if (comparer.Compare(input2,input1) < 0)
                {
                    outputIterator.Write(input2);

                    inputIterator2.MoveNext();
                }
                else
                {
                    outputIterator.Write(input1);

                    if (comparer.Compare(input2,input1) == 0)
                    {
                        inputIterator2.MoveNext();
                    }

                    inputIterator1.MoveNext();
                }

                outputIterator.MoveNext();

            }

            //copy the remaining iterator

            if (inputIterator1.IsEnd())
            {
                Copy(inputIterator2, outputIterator);

                return;
            }

            if (inputIterator2.IsEnd())
            {
                Copy(inputIterator1, outputIterator);

                return;
            }
        }
    }
    #endregion

    #region Utility

    public sealed partial class Algorithm
    {

        public static void Clamp()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Find which value is max
        /// If both value are same, return the second value
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="first"></param>
        /// <param name="second"></param>
        /// <returns></returns>
        public static T Max<T>(T first,T second) where T : IComparable<T>
        {            
            if(first.CompareTo(second)>0)
            {
                return first;
            }
            else
            {
                return second;
            }        
        }

        public static T Max<T>(T first, T second,IComparer<T> comparer)
        {
            if (comparer.Compare(first, second) > 0)
            {
                return first;
            }
            else
            {
                return second;
            }
        }

        /// <summary>
        /// Find the maximum element in the range
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="inputIterator"></param>
        /// <returns></returns>
        public static T MaxElement<T>(IInputIterator<T> inputIterator) where T: IComparable<T>
        {
            T max = inputIterator.Read();

            inputIterator.MoveNext();

            while (!inputIterator.IsEnd())
            {
                max = Max(max, inputIterator.Read());

                inputIterator.MoveNext();
            }

            return max;
        }

        public static T MaxElement<T>(IInputIterator<T> inputIterator,IComparer<T> comparer)
        {
            T max = inputIterator.Read();

            inputIterator.MoveNext();

            while (!inputIterator.IsEnd())
            {
                max = Max(max, inputIterator.Read(),comparer);

                inputIterator.MoveNext();
            }

            return max;
        }

        /// <summary>
        /// Find which value is min
        /// If both value are same , return the first value
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="first"></param>
        /// <param name="second"></param>
        /// <returns></returns>
        public static T Min<T>(T first, T second) 
            where T : IComparable<T>
        {
            if (first.CompareTo(second) > 0)
            {
                return second;
            }
            else
            {
                return first;
            }
        }

        public static T Min<T>(T first, T second,IComparer<T> comparer)
        {
            if (comparer.Compare(first,second) > 0)
            {
                return second;
            }
            else
            {
                return first;
            }
        }

        /// <summary>
        /// Find the minimum element in the range
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="inputIterator"></param>
        /// <returns></returns>
        public static T MinElement<T>(IInputIterator<T> inputIterator)
            where T : IComparable<T>
        {
            T min = inputIterator.Read();

            inputIterator.MoveNext();

            while (!inputIterator.IsEnd())
            {
                min = Min(min, inputIterator.Read());

                inputIterator.MoveNext();
            }

            return min;
        }

        public static T MinElement<T>(IInputIterator<T> inputIterator,IComparer<T> comparer)
        {
            T min = inputIterator.Read();

            inputIterator.MoveNext();

            while (!inputIterator.IsEnd())
            {
                min = Min(min, inputIterator.Read(),comparer);

                inputIterator.MoveNext();
            }

            return min;
        }

        /// <summary>
        /// Find the min and max value
        /// For same value, the second value is used as key
        /// and the value first is used as value
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="first"></param>
        /// <param name="second"></param>
        /// <returns>Min value as Key and Max value as value</returns>
        public static KeyValuePair<T,T> MinMax<T>(T first,T second) 
            where T : IComparable<T>
        {
            if(first.CompareTo(second)>0)
            {
                return new KeyValuePair<T, T>(second, first);
            }
            else
            {
                return new KeyValuePair<T, T>(first, second);
            }
        }

        public static KeyValuePair<T, T> MinMax<T>(T first, T second,IComparer<T> comparer)
        {
            if (comparer.Compare(first,second) > 0)
            {
                return new KeyValuePair<T, T>(second, first);
            }
            else
            {
                return new KeyValuePair<T, T>(first, second);
            }
        }

        /// <summary>
        /// Find the min and max element.
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="inputIterator"></param>
        /// <returns>Min value as Key and Max value as Value</returns>
        public static KeyValuePair<T,T> MinMaxElement<T>(IInputIterator<T> inputIterator)
         where T : IComparable<T>
        {
            T min = inputIterator.Read();

            T max = min;

            inputIterator.MoveNext();

            while (!inputIterator.IsEnd())
            {
                T current = inputIterator.Read();

                min = Min(min, current);

                max = Max(max, current);

                inputIterator.MoveNext();
            }

            return new KeyValuePair<T,T>(min, max);
        }

        public static KeyValuePair<T, T> MinMaxElement<T>(IInputIterator<T> inputIterator,
            IComparer<T> comparer)
        {
            T min = inputIterator.Read();

            T max = min;

            inputIterator.MoveNext();

            while (!inputIterator.IsEnd())
            {
                T current = inputIterator.Read();

                min = Min(min, current,comparer);

                max = Max(max, current,comparer);

                inputIterator.MoveNext();
            }

            return new KeyValuePair<T, T>(min, max);
        }

        public static bool LexicographicalCompare<T>(IInputIterator<T> inputIterator1,
            IInputIterator<T> inputIterator2) where T : IComparable<T>
        {

            while(!(inputIterator1.IsEnd() || inputIterator2.IsEnd()))
            {
                T input1 = inputIterator1.Read();

                T input2 = inputIterator2.Read();

                if(input1.CompareTo(input2)<0)
                {
                    return true;
                }
                else if(input2.CompareTo(input1)<0)
                {
                    return false;
                }

                inputIterator1.MoveNext();

                inputIterator2.MoveNext();
            }

            return (inputIterator1.IsEnd() && !inputIterator2.IsEnd());
        }

        /// <summary>
        ///  Returns true if one range is lexicographically less than another 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="inputIterator1"></param>
        /// <param name="inputIterator2"></param>
        /// <param name="comparer"></param>
        /// <returns></returns>
        public static bool LexicographicalCompare<T>(IInputIterator<T> inputIterator1,
           IInputIterator<T> inputIterator2,IComparer<T> comparer)
        {

            while (!(inputIterator1.IsEnd() || inputIterator2.IsEnd()))
            {
                T input1 = inputIterator1.Read();

                T input2 = inputIterator2.Read();

                if (comparer.Compare(input1,input2) < 0)
                {
                    return true;
                }
                else if (comparer.Compare(input2,input1) < 0)
                {
                    return false;
                }

                inputIterator1.MoveNext();

                inputIterator2.MoveNext();
            }

            return (inputIterator1.IsEnd() && !inputIterator2.IsEnd());
        }

    }

    #endregion

    #region Private 

    public sealed partial class Algorithm
    {
        /// <summary>
        /// Get the number of element in the iterator
        /// 
        /// This method put the iterator to last position+1
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="inputIterator">Note:InputIterator is not clone</param>
        /// <returns></returns>
        public static int GetLength<T>(IInputIterator<T> inputIterator)
        {
            int currentCount = 0;

            while (!inputIterator.IsEnd())
            {
                ++currentCount;

                inputIterator.MoveNext();
            }

            return currentCount;
        }

        /// <summary>
        /// Return the element at the next N
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="inputIterator"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static T Next<T>(IInputIterator<T> inputIterator,int count=1)
        {
            for(int i=0;i<count;i++)
            {
                inputIterator.MoveNext();
            }

            return inputIterator.Read();
        }

        private static IPartialClone GetPartialCloneable(object obj)
        {
            IPartialClone cloneable = obj as IPartialClone;

            if(cloneable==null)
            {
                throw new ArgumentException("Iterator don't implements IPartialClone interface");
            }

            return cloneable;
        }

        private static object GetPartialObject(object obj)
        {
            return GetPartialCloneable(obj).PartialClone();
        }

        private static IShallowClone GetShallowCloneable(object obj)
        {
            IShallowClone cloneable = obj as IShallowClone;

            if (cloneable == null)
            {
                throw new ArgumentException("Iterator don't implements IShallowClone interface");
            }

            return cloneable;
        }

        private static object GetShallowObject(object obj)
        {
            return GetShallowCloneable(obj).ShallowClone();
        }

        private static IRemoveable GetRemoveable(object obj)
        {
            IRemoveable removeable = obj as IRemoveable;

            if (removeable == null)
            {
                throw new ArgumentException("Iterator don't implements IRemoveable interface");
            }

            return removeable;
        }

        private static ICursor GetCursor(object obj)
        {
            ICursor cursor = obj as ICursor;

            if (cursor == null)
            {
                throw new ArgumentException("Iterator don't implements ICursor interface");
            }

            return cursor;
        }

        /// <summary>
        /// Find the distance between two cursor
        /// </summary>
        /// <param name="beginCursor"></param>
        /// <param name="endCursor"></param>
        /// <returns></returns>
        public static int Distance(ICursor beginCursor, ICursor endCursor)
        {
            return Distance(beginCursor.GetPosition(), endCursor.GetPosition());
        }

        public static int Distance(int beginIndex,int endIndex)
        {
            return endIndex - beginIndex;
        }

        public static int Distance(int beginIndex,ICursor endCursor)
        {
            return Distance(beginIndex, endCursor.GetPosition());
        }

        public static int Distance(ICursor beginCursor,int endIndex)
        {
            return Distance(beginCursor.GetPosition(), endIndex);
        }

    }

    

    #endregion


}
