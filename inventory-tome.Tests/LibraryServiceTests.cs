using inventory_tome.Core.Models;
using inventory_tome.Core.Repositories.Interfaces;
using inventory_tome.Core.Services.Services;
using Moq;

namespace inventory_tome.Tests
{
    public class LibraryServiceTests
    {
        private readonly Mock<IBookRepository> _books = new();
        private readonly Mock<IMemberRepository> _members = new();
        private readonly Mock<IBorrowRecordRepository> _borrows = new();
        private LibraryService CreateSut() => new LibraryService(_books.Object, _members.Object, _borrows.Object);


        [Fact]
        public void AddBook_ShouldAddWithStatusTrue()
        {
            // arrange
            var sut = CreateSut();
            Book? captured = null;
            _books.Setup(b => b.Add(It.IsAny<Book>()))
                  .Callback<Book>(b => captured = b);

            // act
            sut.AddBook("Clean Code", "Robert C. Martin");

            // assert
            _books.Verify(b => b.Add(It.IsAny<Book>()), Times.Once);
            Assert.NotNull(captured);
            Assert.Equal("Clean Code", captured!.Title);
            Assert.Equal("Robert C. Martin", captured!.Author);
            Assert.True(captured!.Status);
        }

        [Fact]
        public void FindBooks_ShouldReturnMatches_FromRepository()
        {
            // arrange
            string title = "Clean Code";
            var expectedBooks = new List<Book>
            {
                new Book { Id = 1, Author = "The Sukhrob", Status = true, Title = "Clean Code" },
                new Book { Id = 2, Author = "Alisher Navoiy", Status = true, Title = "Clean Code Architecture" }
            };

            _books.Setup(b => b.FindByTitle(title)).Returns(expectedBooks);
            var sut = CreateSut();

            // act
            var result = sut.FindBooks(title).ToList();

            // assert
            Assert.Equal(expectedBooks.Count, result.Count);
            Assert.Equal(expectedBooks[0].Title, result[0].Title);
            Assert.Equal(expectedBooks[1].Title, result[1].Title);
            _books.Verify(b => b.FindByTitle(title), Times.Once);
            _books.VerifyNoOtherCalls();
        }

        [Fact]
        public void GetBookById_ShouldReturnBook_WhenExists()
        {
            //arrange
            int bookId = 1;
            var expectedBook = new Book
            { 
                Id = bookId, 
                Status = true, 
                Title = "Math Problems", 
                Author = "Sukhrob Chief"
            };

            _books.Setup(b => b.GetById(bookId)).Returns(expectedBook);
            var sut = CreateSut();

            //act
            var result = sut.GetBookById(1);

            //assert
            Assert.NotNull(result);
            Assert.Equal(expectedBook.Id, result.Id);
            Assert.Equal(expectedBook.Title, result.Title);
            Assert.Equal(expectedBook.Author, result.Author);
            Assert.Equal(expectedBook.Status, result.Status);
            _books.Verify(b => b.GetById(bookId), Times.Once);
            _books.VerifyNoOtherCalls();
        }

        [Fact]
        public void GetBookById_ShouldReturnNull_WhenNotFound()
        {
            // arrange
            var sut = CreateSut();
            _books.Setup(b => b.GetById(It.IsAny<int>()))
                  .Returns((Book?)null);

            // act
            var result = sut.GetBookById(123);

            // assert
            Assert.Null(result);
            _books.Verify(b => b.GetById(123), Times.Once);
        }


    }
}
