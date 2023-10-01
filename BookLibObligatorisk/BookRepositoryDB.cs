using BookLibObligatorisk;
using BookLibObligatorisk.Base;
using BookLibObligatorisk.Interfaces;
using BookLibObligatorisk.Models;
using Microsoft.EntityFrameworkCore;

namespace BookLibObligatorisk
{
    /// <summary>
    /// A repository for books using a database. Inherits from RepositoryBaseDB.
    /// </summary>
    public class BookRepositoryDB : RepositoryBaseDB<Book>, IBookRepository
    {
        // Constructor
        public BookRepositoryDB(DbContext dbContext) : base(dbContext)
        {

        }

        public IEnumerable<Book> Get(double? filterMinPrice = null, double? filterMaxPrice = null, string? titleIncludes = null, string? sortOrder = null)
        {
            // Filters
            IQueryable<Book> query = _dbContext.Set<Book>().AsQueryable();
            if (filterMinPrice != null)
            {
                query = query.Where(b => b.Price >= filterMinPrice);
            }

            if (filterMaxPrice != null)
            {
                query = query.Where(b => b.Price <= filterMaxPrice);
            }
            if (titleIncludes != null)
            {
                query = query.Where(b => b.Title.Contains(titleIncludes));
            }

            // Sort
            if (sortOrder != null)
            {
                switch(sortOrder)
                {
                    // vælger at sortere efter id hvis der ikke er angivet en gyldig sortering
                    // (man kunne også vælge at smide en exception eller gøre ingen ting)
                    default:
                    case "id":
                    case "id_asc":
                        query = query.OrderBy(b => b.Id);
                        break;
                    case "id_desc":
                        query = query.OrderByDescending(b => b.Id);
                        break;
                    case "title":
                    case "title_asc":
                        query = query.OrderBy(b => b.Title);
                        break;
                    case "title_desc":
                        query = query.OrderByDescending(b => b.Title);
                        break;
                    case "price":
                    case "price_asc":
                        query = query.OrderBy(b => b.Price);
                        break;
                    case "price_desc":
                        query = query.OrderByDescending(b => b.Price);
                        break;
                }
            }

            // return
            return query;
        }
    }
}
