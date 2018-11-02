using LibNavigate.Converter;
using System;

namespace LibNavigate.Iterator.Extend
{
    /// <summary>
    /// Output iterator which write data to console
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public sealed class ConsoleOutputIterator<T> : IOutputIterator<T>
    {
        private readonly IConverter<T, string> converter;

        private readonly bool isNewLine;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="converter"></param>
        /// <param name="isNewLine"></param>
        public ConsoleOutputIterator(IConverter<T, string> converter,bool isNewLine=true)
        {
            if (converter == null)
            {
                throw new ArgumentNullException("converter cannot be null");
            }
            else
            {
                this.converter = converter;
            }

            this.isNewLine = isNewLine;
        }

        public ConsoleOutputIterator(bool isNewLine=true)
        {
            this.isNewLine = isNewLine;
        }

        public void Dispose()
        {
        }

        public void MoveNext()
        {
        }

        public void Write(T data)
        {
            string msg = null;

            if (converter != null)
            {
                msg = converter.Convert(data);
            }
            else
            {
                msg = data.ToString();
            }
            
            if(isNewLine)
            {
                Console.WriteLine(msg);
            }
            else
            {
                Console.Write(msg);
            }
        }
    }
}
