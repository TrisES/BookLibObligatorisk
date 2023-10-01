using BookLibObligatorisk.Interfaces;
using BookLibObligatorisk.Models;

namespace BookLibObligatorisk.Interfaces
{
    /// <summary>
    /// Represents a more specific repository for books.
    /// </summary>
    public interface IBookRepository : IRepository<Book>
    {
        /// <summary>
        /// Gets all books that match the given filters, sorted by the given sort order (all optional).
        /// </summary>
        /// <param name="filterMinPrice">The minimum price to filter by.</param>
        /// <param name="filterMaxPrice">The maximum price to filter by.</param>
        /// <param name="sortOrder">The sort order defined by a string with format: "propertyName_order", e.g. "title_asc" or "price_desc".</param>
        /// <param name="titleIncludes">The string to filter titles by whether they include it or not.</param>
        /// <returns>An IEnumerable with <see cref="Book"/>s.</returns>
        IEnumerable<Book> Get(double? filterMinPrice = null, double? filterMaxPrice = null, string? titleIncludes = null, string? sortOrder = null);
    }
}