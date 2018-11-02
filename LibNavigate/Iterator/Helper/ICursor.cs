namespace LibNavigate.Iterator.Helper
{
    public interface ICursor
    {
        /// <summary>
        /// The position/index of the internal data structure
        /// </summary>
        /// <returns></returns>
        int GetPosition();

        /// <summary>
        /// Set the position/index of the internal data structure
        /// </summary>
        /// <param name="position"></param>
        void SetPosition(int position);
    }
}
