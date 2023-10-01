namespace BookLibObligatorisk.Interfaces
{
    public interface IValidateable
    {
        /// <summary>
        /// Validates the object. Throws exception(s) if the object is not valid.
        /// </summary>
        void Validate();
    }
}
