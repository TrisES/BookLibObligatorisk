using Microsoft.VisualStudio.TestTools.UnitTesting;
using BookLibObligatorisk;
using BookLibObligatorisk.Interfaces;
using BookLibObligatorisk.Models;
using Microsoft.EntityFrameworkCore;

namespace BookLibObligatorisk.Tests
{
    [TestClass()]
    public class BookRepositoryListTests
    {
        private const bool useDatabase = false;
        private static BooksDbContext? _dbContext;
        private static IBookRepository _repo;

        [ClassInitialize] // kører før første test
        public static void InitOnce(TestContext context)
        {
            if (useDatabase)
            {
                var optionsBuilder = new DbContextOptionsBuilder<BooksDbContext>();
                optionsBuilder.UseSqlServer(Secrets.ConnectionStringSimply);
                _dbContext = new BooksDbContext(optionsBuilder.Options);
                //List<Movie> all = _dbContext.Movies.ToList();
                //_dbContext.RemoveRange(all);

                //_dbContext.SaveChanges();
            }
        }

        [TestInitialize] // kører før hver test
        public void Init()
        {
            if (useDatabase)
            {
                _repo = new BookRepositoryDB(_dbContext);
                _dbContext.Database.ExecuteSqlRaw("TRUNCATE TABLE dbo.Movies");
            }
            else { _repo = new BookRepositoryList(); }

            _repo.Add(new Book() { Title = "Harry Potter", Price = 100 });
            _repo.Add(new Book() { Title = "Engineering Software Products", Price = 300 });
            _repo.Add(new Book() { Title = "Agile Samurai", Price = 500 });
            _repo.Add(new Book() { Title = "Ringenes Herre", Price = 700 });
        }

        [TestMethod()]
        public void GetTest()
        {
            IEnumerable<Book> books = _repo.Get();
            Assert.AreEqual(4, books.Count());
            Assert.AreEqual(books.First().Title, "Harry Potter");

            IEnumerable<Book> sortedBooks = _repo.Get(sortOrder: "title");
            Assert.AreEqual(sortedBooks.First().Title, "Agile Samurai");

            IEnumerable<Book> sortedBooks2 = _repo.Get(sortOrder: "price_desc");
            Assert.AreEqual(sortedBooks2.First().Title, "Ringenes Herre");

            IEnumerable<Book> filteredBooks = _repo.Get(filterMinPrice: 400);
            Assert.AreEqual(2, filteredBooks.Count());
            Assert.AreEqual(filteredBooks.First().Title, "Agile Samurai");
            Assert.AreEqual(filteredBooks.Last().Title, "Ringenes Herre");
        }

        [TestMethod()]
        public void GetByIdTest()
        {
            Assert.IsNotNull(_repo.GetById(1));
            Assert.IsNull(_repo.GetById(100));

            Assert.AreEqual("Harry Potter", _repo.GetById(1).Title);
        }

        [TestMethod()]
        public void AddTest()
        {
            Book b = new Book() { Title = "Test Bog", Price = 555 };
            Assert.AreEqual(5, _repo.Add(b).Id);
            Assert.AreEqual(5, _repo.Get().Count());
        }

        [TestMethod()]
        public void UpdateTest()
        {
            Assert.AreEqual(4, _repo.Get().Count());
            Book book = new Book() { Id = 1, Title = "Test Bog", Price = 555 };
            Assert.IsNull(_repo.Update(100, book));
            Assert.AreEqual(1, _repo.Update(1, book)?.Id);
            Assert.AreEqual(4, _repo.Get().Count());
        }

        [TestMethod()]
        public void DeleteTest()
        {
            Assert.IsNull(_repo.Delete(100));
            Assert.AreEqual(2, _repo.Delete(2)?.Id);
            Assert.AreEqual(3, _repo.Get().Count());
        }
    }
}