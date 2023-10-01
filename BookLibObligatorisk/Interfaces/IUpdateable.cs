namespace BookLibObligatorisk.Interfaces
{
    public interface IUpdateable<T> where T : class
    {
        /// <summary>
        /// Method for updating an object. Used to enforce model classes to implement this method.
        /// </summary>
        /// <param name="data"></param>
        /// <returns>Returns updated entity</returns>
        T Update(T data);
    }
}
