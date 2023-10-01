using BookLibObligatorisk.Base;
using BookLibObligatorisk.Interfaces;
using BookLibObligatorisk.Models;

namespace BookLibObligatorisk
{
    /// <summary>
    /// A repository for books, using a List. Inherits from RepositoryBaseList.
    /// </summary>
    public class BookRepositoryList : RepositoryBaseList<Book>, IBookRepository
    {
        // Constructor
        public BookRepositoryList() : base() 
        {
            //_list.Add(new Book() { Id = _nextId++, Title = "The Hobbit", Price = 100 });
            //_list.Add(new Book() { Id = _nextId++, Title = "The Lord of the Rings", Price = 200 });
        }

        public IEnumerable<Book> Get(double? filterMinPrice = null, double? filterMaxPrice = null, string? titleIncludes = null, string? sortOrder = null)
        {
            // Filters
            IEnumerable<Book> resultList = new List<Book>(_list); // copy list
            if (filterMinPrice != null)
            {
                resultList = resultList.Where(b => b.Price >= filterMinPrice);
            }
            if (filterMaxPrice != null)
            {
                resultList = resultList.Where(b => b.Price <= filterMaxPrice);
            }
            if (titleIncludes != null)
            {
                resultList = resultList.Where(b => b.Title.Contains(titleIncludes));
            }

            // Sort
            if (sortOrder != null)
            {
                switch (sortOrder)
                {
                    // vælger at sortere efter id hvis der ikke er angivet en gyldig sortering
                    // (man kunne også vælge at smide en exception eller gøre ingen ting)
                    default:
                    case "id":
                    case "id_asc":
                        resultList = resultList.OrderBy(b => b.Id);
                        break;
                    case "id_desc":
                        resultList = resultList.OrderByDescending(b => b.Id);
                        break;
                    case "title":
                    case "title_asc":
                        resultList = resultList.OrderBy(b => b.Title);
                        break;
                    case "title_desc":
                        resultList = resultList.OrderByDescending(b => b.Title);
                        break;
                    case "price":
                    case "price_asc":
                        resultList = resultList.OrderBy(b => b.Price);
                        break;
                    case "price_desc":
                        resultList = resultList.OrderByDescending(b => b.Price);
                        break;
                }
            }

            // Return
            return resultList;
        }
    }
}
