using LibNavigate.Converter;
using System;

namespace LibNavigate.Iterator.Extend
{
    /// <summary>
    ///  Output Iterator which write data to file
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public sealed class FileOutputIterator<T> : IOutputIterator<T>
    {
        private System.IO.StreamWriter writer;

        private string fileName;

        private IConverter<T, string> converter;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="converter">Converter which provide data to write to</param>
        /// <param name="fileName">File name we wanted to write to</param>
        public FileOutputIterator(IConverter<T,string> converter, string fileName)
        {
            Init(fileName, converter);
        }

        /// <summary>
        /// Use a default file name.
        /// Default file name = fs{HourMinuteSecond}
        /// <param name="converter">Converter which provide data to write to</param>
        /// </summary>
        public FileOutputIterator(IConverter<T,string> converter)
        {
            Init(GetDefaultFileName(), converter);
        }

        /// <summary>
        /// Use deault converter which read Object.ToString()
        /// </summary>
        /// <param name="fileName">File name we wanted to write to</param>
        public FileOutputIterator(string fileName):this(new DefaultConverter(),fileName)
        {

        }

        /// <summary>
        /// Default file name = fs{HourMinuteSecond}
        /// Use deault converter which read Object.ToString()
        /// </summary>
        public FileOutputIterator()
        {
            Init(GetDefaultFileName(), new DefaultConverter());
        }
   
        private void Init(string fileName,IConverter<T,string> converter)
        {
            if(string.IsNullOrWhiteSpace(fileName))
            {
                throw new ArgumentException("invalid file name");
            }

            this.fileName = fileName;

            if (converter == null)
            {
                throw new ArgumentNullException("converter cannot be null");
            }
            else
            {
                this.converter = converter;
            }

            CreateWriter();
        }

        private void CreateWriter()
        {
            writer = System.IO.File.AppendText(fileName);
        }

        private string GetDefaultFileName()
        {
            DateTime current = DateTime.Now;

            return string.Format("fs{0}{1}{2}", current.Hour, current.Minute, current.Second);
        }

        public void Dispose()
        {
            if (writer != null)
            {
                writer.Flush();
                writer.Dispose();
            }
        }

        public void MoveNext()
        {
            //no-op
        }

        public void Write(T data)
        {
            writer.WriteLine(converter.Convert(data));
        }

        private class DefaultConverter : IConverter<T, string>
        {
            public string Convert(T data)
            {
                return data.ToString();
            }
        }
    }
}
