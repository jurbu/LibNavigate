using LibNavigate.Iterator.Helper;
using LibNavigate.Converter;
using System;

namespace LibNavigate.Iterator.Extend
{
    /// <summary>
    ///  Iterator which read data from the file
    ///  and convert to other data.
    ///  If you don't want to convert into the specific type 
    ///  consider using SimpleFileInputIterator which return string
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public sealed class FileInputIterator<T> : IInputIterator<T>,IRemoveable,
        IPartialClone,IShallowClone,ICursor
    {
        private int currentIndex;

        private string[] lines;

        private readonly IConverter<string,T> converter;

        public FileInputIterator(string fileName, IConverter<string, T> converter)
        {
            Begin();

            if (converter == null)
            {
                throw new ArgumentNullException("converter cannot be null");
            }
            else
            {
                this.converter = converter;
            }

            lines = System.IO.File.ReadAllLines(fileName);

        }

        private FileInputIterator(FileInputIterator<T> obj,bool isDeepClone=true)
        {
            if (isDeepClone)
            {
                this.lines = obj.lines.DeepClone();
            }
            else
            {
                this.lines = obj.lines;
            }

            this.currentIndex = obj.currentIndex;

            this.converter = obj.converter;
        }

        public IConverter<string, T> GetConverter()
        {
            return converter;
        }

        public T Read()
        {
            return converter.Convert(lines[currentIndex]);
        }

        public void MoveNext()
        {
            ++currentIndex;
        }

        public bool IsEnd()
        {
            return (currentIndex >= lines.Length);
        }

        public void Dispose()
        {
        }

        public void Begin()
        {
            currentIndex = 0;
        }

        public void Remove()
        {
            lines = lines.RemoveAt(currentIndex);

            //-1 index because a single data is remove
            --currentIndex;
        }

        public void End()
        {
            currentIndex = lines.Length;
        }

        public object ShallowClone()
        {
            return new FileInputIterator<T>(this,false);
        }

        public object PartialClone()
        {
            return new FileInputIterator<T>(this);
        }

        public int GetPosition()
        {
            return currentIndex;
        }

        public void SetPosition(int position)
        {
            this.currentIndex = position;
        }
    }
}
