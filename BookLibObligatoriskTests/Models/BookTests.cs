using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BookLibObligatorisk.Models.Tests
{
    [TestClass()]
    public class BookTests
    {
        [TestMethod()]
        public void BookConstructorTest()
        {
            // Arrange
            Book b1 = new Book(1, "Agile Samurai", 100.0);
            Book b2 = new Book();
            // Act
            // Assert
            Assert.AreEqual(1, b1.Id);
            Assert.AreEqual("Agile Samurai", b1.Title);
            Assert.AreEqual(100.0, b1.Price, 0.001);
            Assert.AreEqual(0, b2.Id);
            Assert.AreEqual(null, b2.Title);
            Assert.AreEqual(0.0, b2.Price, 0.001);
        }

        [TestMethod()]
        public void ToStringTest()
        {
            // Arrange
            Book b1 = new Book(1, "Agile Samurai", 100.0);
            string expected = "Id: 1, Title: Agile Samurai, Price: 100";
            // Act
            string actual = b1.ToString();
            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void ValidateTitleTest()
        {
            // Arrange
            Book validTitle = new Book(1, "Agile Samurai", 100.0);
            Book titleIsNull = new Book(1, null, 100.0);
            Book titleEmpty = new Book(1, "", 100.0);
            Book titleTooShort = new Book(1, "Ag", 100.0);
            Book titleTrickyTooShort = new Book(1, "  Ag  ", 100.0);

            // Assert
            Assert.ThrowsException<ArgumentNullException>(() => titleIsNull.ValidateTitle());
            Assert.ThrowsException<ArgumentException>(() => titleEmpty.ValidateTitle());
            Assert.ThrowsException<ArgumentException>(() => titleTooShort.ValidateTitle());
            Assert.ThrowsException<ArgumentException>(() => titleTrickyTooShort.ValidateTitle());
        }

        [TestMethod()]
        public void ValidatePriceTest()
        {
            // Price, tal, 0 < price <= 1200
            // Arrange
            Book validPrice = new Book(1, "Agile Samurai", 100.0);
            Book priceLowerBoundry = new Book(1, "Agile Samurai", Book.MinPrice); // NOT inclusive
            Book priceLowerBoundryMinus = new Book(1, "Agile Samurai", Book.MinPrice - Book.PricePrecision);
            Book priceLowerBoundryPlus = new Book(1, "Agile Samurai", Book.MinPrice + Book.PricePrecision);

            Book priceUpperBoundry = new Book(1, "Agile Samurai", Book.MaxPrice); // inclusive
            Book priceUpperBoundryMinus = new Book(1, "Agile Samurai", Book.MaxPrice - Book.PricePrecision);
            Book priceUpperBoundryPlus = new Book(1, "Agile Samurai", Book.MaxPrice + Book.PricePrecision);

            // Assert valid price
            validPrice.ValidatePrice();

            // Assert lower boundry
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => priceLowerBoundry.ValidatePrice());
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => priceLowerBoundryMinus.ValidatePrice());
            priceLowerBoundryPlus.ValidatePrice();

            // Assert upper boundry
            priceUpperBoundry.ValidatePrice();
            priceUpperBoundryMinus.ValidatePrice();
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => priceUpperBoundryPlus.ValidatePrice());
        }

        [TestMethod()]
        public void ValidateTest()
        {
            // Arrange
            Book validBook = new Book(1, "Agile Samurai", 100.0);
            Book invalidTitle = new Book(1, "Ag", 100.0);
            Book invalidPrice = new Book(1, "Agile Samurai", Book.MinPrice - Book.PricePrecision);

            // Assert valid book
            validBook.Validate();

            // Assert invalid title
            Assert.ThrowsException<ArgumentException>(() => invalidTitle.Validate());

            // Assert invalid price
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => invalidPrice.Validate());
        }
    }
}