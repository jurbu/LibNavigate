using LibNavigate.Iterator.Helper;

namespace LibNavigate.Iterator.Extend
{
    /// <summary>
    /// Input iterator which read all 
    /// the line from the file
    /// </summary>
    public sealed class SimpleFileInputIterator : IInputIterator<string>,
        IPartialClone,IShallowClone,IRemoveable,ICursor
    {
        private int currentIndex;

        private string[] lines;

        public SimpleFileInputIterator(string fileName)
        {
            Begin();

            lines = System.IO.File.ReadAllLines(fileName);
        }

        private SimpleFileInputIterator(SimpleFileInputIterator obj,bool isDeepClone=true)
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
        }

        public void Begin()
        {
            this.currentIndex = 0;
        }

        public void Dispose()
        {
            //no-op
        }

        public void End()
        {
            currentIndex = lines.Length;
        }

        public bool IsEnd()
        {
            return (currentIndex >= lines.Length);
        }

        public void MoveNext()
        {
            ++currentIndex;
        }

        public object PartialClone()
        {
            return new SimpleFileInputIterator(this);
        }

        public int GetPosition()
        {
            return currentIndex;
        }

        public string Read()
        {
            return lines[currentIndex];
        }

        public void Remove()
        {
            lines = lines.RemoveAt(currentIndex);

            //-1 index because a single data is remove
            --currentIndex;
        }

        public object ShallowClone()
        {
            return new SimpleFileInputIterator(this,false);
        }

        public void SetPosition(int position)
        {
            currentIndex = position;
        }
    }
}
