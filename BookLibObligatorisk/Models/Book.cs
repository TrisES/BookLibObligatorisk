using BookLibObligatorisk.Interfaces;

namespace BookLibObligatorisk.Models
{
    /// <summary>
    /// Represents a simple book model.
    /// </summary>
    public class Book : IHasId, IValidateable, IUpdateable<Book>
    {
        #region Forretningregler
        /// <summary>
        /// Minimum length of book title, not including whitespace.
        /// </summary>
        public const int MinTitleLength = 3; // NOT including trailing or leading whitespace
        /// <summary>
        /// Minimum price for a book. Not inclusive.
        /// </summary>
        public const int MinPrice = 0; // NOT inclusive
        /// <summary>
        /// Maximum price for a book. Inclusive.
        ///</summary>
        public const int MaxPrice = 1200; // inclusive
        /// <summary>
        /// Precision for book prices.
        /// </summary>
        public const double PricePrecision = 0.001;
        #endregion

        #region Properties
        /// <summary>
        /// Unique identifier of a book.
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Title of the book.
        /// </summary>
        public string? Title { get; set; }
        /// <summary>
        /// Price of the book.
        /// </summary>
        public double Price { get; set; }
        #endregion

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="Book"/> class with specified book id, title and price.
        /// </summary>
        /// <param name="id">Unique identifier of a book.</param>
        /// <param name="title">Title of the book.</param>
        /// <param name="price">Price of the book.</param>
        public Book(int id, string? title, double price)
        {
            Id = id;
            Title = title;
            Price = price;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Book"/> class.
        /// </summary>
        public Book()
        {

        }
        #endregion

        #region Methods
        /// <summary>
        /// Returns a string that represents the book.
        /// </summary>
        /// <returns>A string that represents the book.</returns>
        public override string ToString()
        {
            return $"{nameof(Id)}: {Id}, {nameof(Title)}: {Title}, {nameof(Price)}: {Price}";
        }

        /// <summary>
        /// Validates the book title.
        /// </summary>
        /// <exception cref="ArgumentNullException">Thrown when the Title value is null</exception>
        /// <exception cref="ArgumentException">Thrown when the Title length (not including whitespace) is shorter than the minimum title length  defined in the `MinTitleLength` constant.</exception>
        public void ValidateTitle()
        {
            if (Title == null)
            {
                throw new ArgumentNullException(nameof(Title), "Title cannot be null");
            }
            if (Title.Trim().Length < MinTitleLength)
            {
                throw new ArgumentException($"Title must be at least {MinTitleLength} characters long (not including leading and trailing whitespace)", nameof(Title));
            }
        }

        /// <summary>
        /// Validates the book price.
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when the price is less than the minimum price defined in the <c>MinPrice</c> constant, or if it is greater than the maximum price defined in the 'MaxPrice' constant.</exception>
        public void ValidatePrice()
        {
            // Price, tal, 0 < price <= 1200
            if (Price <= MinPrice || Price > MaxPrice)
            {
                throw new ArgumentOutOfRangeException(nameof(Price), $"Price must be between ]{MinPrice};{MaxPrice}]");
            }
        }

        /// <summary>
        /// Validates the book.
        /// </summary>
        /// <exception cref="ArgumentNullException">Thrown when the Title value is null</exception>"
        /// <exception cref="ArgumentException">Thrown when the Title length (not including whitespace) is shorter than the minimum title length defined in the `MinTitleLength` constant.</exception>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when the price is less than the minimum price defined in the <c>MinPrice</c> constant, or if it is greater than the maximum price defined in the 'MaxPrice' constant.</exception>
        public void Validate()
        {
            ValidateTitle();
            ValidatePrice();
        }

        public Book Update(Book data)
        {
            data.Validate();
            Title = data.Title;
            Price = data.Price;
            return this;
        }
        #endregion
    }
}
