using Microsoft.VisualStudio.TestTools.UnitTesting;
using BookLibObligatorisk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookLibObligatorisk.Interfaces;
using BookLibObligatorisk.Models;
using Microsoft.EntityFrameworkCore;

namespace BookLibObligatorisk.Tests
{
    [TestClass()]
    public class BookRepositoryDBTests
    {
        private const bool useDatabase = true;
        private static BooksDbContext? _dbContext;
        private static IBookRepository _repo;

        [ClassInitialize]
        public static void InitOnce(TestContext context)
        {
            if (useDatabase)
            {
                var optionsBuilder = new DbContextOptionsBuilder<BooksDbContext>();
                optionsBuilder.UseSqlServer(Secrets.ConnectionStringSimply);
                _dbContext = new BooksDbContext(optionsBuilder.Options);
                // clean database table: remove all rows
                _dbContext.Database.ExecuteSqlRaw("TRUNCATE TABLE dbo.Books");
                _repo = new BookRepositoryDB(_dbContext);
            }
            else
            // use in-memory database (en liste)
            {
                _repo = new BookRepositoryList();
            }
        }

        [TestMethod()]
        public void AddTest()
        {
            _repo.Add(new Book { Title = "Agile Samurai", Price = 100 });
            Book esp = _repo.Add(new Book() { Title = "Engineering Software Products", Price = 200 });
            Assert.IsTrue(esp.Id >= 0);
            Assert.AreEqual(200, esp.Price);
            Assert.AreEqual("Engineering Software Products", esp.Title);

            int nrOfBooks = _repo.Get().Count();
            Assert.AreEqual(2, nrOfBooks);

            Assert.ThrowsException<ArgumentNullException>(
                () => _repo.Add(new Book() { Title = null, Price = 100 }));
            Assert.ThrowsException<ArgumentException>(
                () => _repo.Add(new Book() { Title = "", Price = 100 }));
            Assert.ThrowsException<ArgumentOutOfRangeException>(
                () => _repo.Add(new Book() { Title = "Agile Samurai", Price = Book.MinPrice })); // not included
        }

        [TestMethod()]
        public void GetTest()
        {
            _repo.Add(new Book { Title = "Agile Samurai", Price = 100 });
            _repo.Add(new Book() { Title = "Engineering Software Products", Price = 200 });
            IEnumerable<Book> books = _repo.Get(sortOrder: "title").ToList();
            Assert.AreEqual(books.First().Title, "Agile Samurai");

            books = _repo.Get(sortOrder: "price_desc");
            Assert.AreEqual(books.First().Title, "Engineering Software Products");

            books = _repo.Get(titleIncludes: "Samurai");
            Assert.AreEqual(3, books.Count());
            Assert.AreEqual(books.First().Title, "Agile Samurai");

            books = _repo.Get(filterMinPrice: 150);
            Assert.AreEqual(books.First().Title, "Engineering Software Products");

            books = _repo.Get(filterMaxPrice: 150);
            Assert.AreEqual(books.First().Title, "Agile Samurai");

            _repo.Add(new Book() { Title = "Harry Potter", Price = 150 });
            books = _repo.Get(filterMinPrice: 125, filterMaxPrice: 175);
            Assert.AreEqual(books.First().Title, "Harry Potter");
            Assert.AreEqual(1, books.Count());
        }

        [TestMethod()]
        public void GetByIdTest()
        {
            Book b1 = _repo.Add(new Book { Title = "Agile Samurai", Price = 100 });
            Book b2 = _repo.Add(new Book() { Title = "Engineering Software Products", Price = 200 });
            Book? b3 = _repo.GetById(b2.Id);
            Assert.AreEqual(b2, b3);
            Assert.AreNotEqual(b1, b3);
            Assert.AreEqual(b3.Title, "Engineering Software Products");
            Assert.AreEqual(b3.Price, 200);

            Assert.IsNull(_repo.GetById(-1));
        }

        [TestMethod()]
        public void UpdateTest()
        {
            Book b = _repo.Add(new Book { Title = "Alfabethuset", Price = 250 });
            Book? book = _repo.Update(b.Id, new Book { Title = "Bilfakta 2", Price = 555 });
            Assert.IsNotNull(book);
            Book? book2 = _repo.GetById(b.Id);
            Assert.AreEqual(book2.Title, "Bilfakta 2");

            Assert.IsNull(_repo.Update(-1, new Book() { Title = "ABC", Price = 123 }));
            Assert.ThrowsException<ArgumentNullException>(
                               () => _repo.Update(b.Id, new Book() { Title = null, Price = 100 }));
            Assert.ThrowsException<ArgumentException>(
                               () => _repo.Update(b.Id, new Book() { Title = "", Price = 100 }));
            Assert.ThrowsException<ArgumentOutOfRangeException>(
                               () => _repo.Update(b.Id, new Book() { Title = "Agile Samurai", Price = Book.MinPrice })); // not included
        }

        [TestMethod()]
        public void DeleteTest()
        {
            Book b = _repo.Add(new Book { Title = "Ringenes Herre", Price = 400 });
            Book? book = _repo.Delete(b.Id);
            Assert.IsNotNull(book);
            Assert.AreEqual(book.Title, "Ringenes Herre");
            Assert.AreEqual(book.Price, 400);

            Book? book2 = _repo.Delete(b.Id);
            Assert.IsNull(book2);
        }
    }
}