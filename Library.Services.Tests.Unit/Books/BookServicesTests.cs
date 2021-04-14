using FluentAssertions;
using Library.Entities;
using Library.Infrastructure.Application;
using Library.Infrastructure.Test;
using Library.Persistence.EF;
using Library.Persistence.EF.Books;
using Library.Services.Books;
using Library.Services.Books.Contracts;
using Library.Services.Books.Exceptions;
using Library.TestTools.BookCategoreis;
using Library.TestTools.Books;
using System;
using System.Linq;
using System.Threading.Tasks;
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
            var dto = BookFactory.GenerateRegisterBookDto(bookCategory.Id);

            var bookId = await _sut.Register(dto);

            var expected = _readDataContext.Books.Single(_ => _.Id == bookId);
            expected.BookCategoryId.Should().Be(bookCategory.Id);
            expected.Title.Should().Be("در جستجوی زمان از دست رفته");
            expected.Author.Should().Be("مارسل پروست");
            expected.MinimumAge.Should().Be(18);
        }

        [Fact]
        public async void Edit_edit_a_book_specification_properly()
        {
            var newbookCategory = BookCategoryFactory.GenerateBookCategory("داستان‌های یونانی");
            _contex.Manipulate(_ => _.BookCategories.Add(newbookCategory));

            var bookCategory = BookCategoryFactory.GenerateBookCategory("داستان‌های فرانسوی");
            var book = new BookBuilder().GenerateAddBookWithBookCategory(bookCategory).Build();
            _contex.Manipulate(_ => _.Books.Add(book));

            var dto = BookFactory.GenerateEditBookDto(newbookCategory.Id);
            await _sut.Edit(book.Id, dto);

            var expected = _readDataContext.Books.Single(_ => _.Id == book.Id);
            expected.BookCategoryId.Should().Be(newbookCategory.Id);
            expected.Title.Should().Be("زوربای یونانی");
            expected.Author.Should().Be("نیکوس");
            expected.MinimumAge.Should().Be(15);
        }

        [Fact]
        public async void Edit_throws_exception_when_notFound_bookById()
        {
            var newbookCategory = BookCategoryFactory.GenerateBookCategory("داستان‌های یونانی");
            _contex.Manipulate(_ => _.BookCategories.Add(newbookCategory));

            var bookCategory = BookCategoryFactory.GenerateBookCategory("داستان‌های فرانسوی");
            var book = new BookBuilder().GenerateAddBookWithBookCategory(bookCategory).Build();
            _contex.Manipulate(_ => _.Books.Add(book));

            var dto = BookFactory.GenerateEditBookDto(newbookCategory.Id);
            Func<Task> expected = () => _sut.Edit(4, dto);

            expected.Should().ThrowExactly<NotFoundBookByIdException>();
        }
    }
}
