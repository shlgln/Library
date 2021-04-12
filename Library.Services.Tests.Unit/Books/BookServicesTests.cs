using FluentAssertions;
using Library.Entities;
using Library.Infrastructure.Application;
using Library.Infrastructure.Test;
using Library.Persistence.EF;
using Library.Persistence.EF.Books;
using Library.Services.Books;
using Library.Services.Books.Contracts;
using Library.TestTools.BookCategoreis;
using System.Linq;
using Xunit;

namespace Library.Services.Tests.Unit.Books
{
    public class BookServicesTests
    {
        EFDataContext _contex;
        EFDataContext _readDataContext;
        BookService _sut;
        UnitOfWork _unitofwork;
        BookRepository _repository;
        public BookServicesTests()
        {
            var db = new EFInMemoryDatabase();
            _contex = db.CreateDataContext<EFDataContext>();
            _readDataContext = db.CreateDataContext<EFDataContext>();
            _unitofwork = new EFUnitOfWork(_contex);
            _repository = new EFBookRepository(_contex);
            _sut = new BookAppService(_unitofwork, _repository);
        }
        
        [Fact]
        public async void Register_registers_a_book_properly()
        {
            var bookCategory = BookCategoryFactory.GenerateBookCategory("dummy");
            _contex.Manipulate(_ => _.BookCategories.Add(bookCategory));
            var dto = new AddBookDto
            {
                BookCategoryId = bookCategory.Id,
                Title = "در جستجوی زمان از دست رفته",
                Author = "مارسل پروست",
                MinimumAge = 18
            };

            var bookId = await _sut.Register(dto);

            var expected = _readDataContext.Books.Single(_ => _.Id == bookId);
            expected.BookCategoryId.Should().Be(bookCategory.Id);
            expected.Title.Should().Be("در جستجوی زمان از دست رفته");
            expected.Author.Should().Be("مارسل پروست");
            expected.MinimumAge.Should().Be(18);
        }
    }
}
