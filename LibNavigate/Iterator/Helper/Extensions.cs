using System;

namespace LibNavigate.Iterator.Helper
{
    public static class Extensions
    {
        /// <summary>
        /// Remove an element from an array 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public static T[] RemoveAt<T>(this T[] source, int index)
        {
            T[] dest = new T[source.Length - 1];

            if (index > 0)
            {
                Array.Copy(source, 0, dest, 0, index);
            }

            if (index < source.Length - 1)
            {
                Array.Copy(source, index + 1, dest, index, source.Length - index - 1);
            }

            return dest;
        }

        /// <summary>
        /// Deep clone an array
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <returns></returns>
        public static T[] DeepClone<T>(this T[] source)
        {
            T[] dest = new T[source.Length];

            for(int i=0;i<source.Length;i++)
            {
                dest[i] = source[i];
            }

            return dest;
        }

    }
}
