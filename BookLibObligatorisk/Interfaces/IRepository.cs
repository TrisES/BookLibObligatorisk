namespace BookLibObligatorisk.Interfaces
{
    /// <summary>
    /// Generic interface for a repository.
    /// </summary>
    /// <typeparam name="T">The entity type that the repository works with.</typeparam>
    public interface IRepository<T> where T : class, IHasId, IValidateable, IUpdateable<T>
    {
        /// <summary>
        /// Gets a list of all entities in the repository.
        /// </summary>
        /// <returns>List of all entities in the repository.</returns>
        IEnumerable<T> Get();

        /// <summary>
        /// Gets the entity with the specified ID from the repository.
        /// </summary>
        /// <param name="id">The ID of the entity to get.</param>
        /// <returns>The entity with the specified ID, or null if it is not found.</returns>
        T? GetById(int id);

        /// <summary>
        /// Adds a new entity to the repository.
        /// </summary>
        /// <param name="entity">The entity to add.</param>
        /// <returns>The added entity, or null if it couldn't be added</returns>
        T Add(T entity);

        /// <summary>
        /// Deletes the entity with the specified ID from the repository.
        /// </summary>
        /// <param name="id">The ID of the entity to delete.</param>
        /// <returns>The deleted entity, or null if it is not found.</returns>
        T? Delete(int id);

        /// <summary>
        /// Updates the entity with the specified ID in the repository.
        /// </summary>
        /// <param name="id">The ID of the entity to update.</param>
        /// <param name="entity">The updated entity data.</param>
        /// <returns>The updated entity, or null if it is not found.</returns>
        T? Update(int id, T entity);
    }
}