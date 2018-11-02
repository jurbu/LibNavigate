namespace LibNavigate.Iterator.Helper
{

    public interface IPartialClone
    {
        /// <summary>
        /// Deep clone member
        /// which is used in iterator interface
        /// </summary>
        /// <returns></returns>
        object PartialClone();
    }
}
