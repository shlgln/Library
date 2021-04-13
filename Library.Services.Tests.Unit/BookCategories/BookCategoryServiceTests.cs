using FluentAssertions;
using Library.Entities;
using Library.Infrastructure.Application;
using Library.Infrastructure.Test;
using Library.Persistence.EF;
using Library.Persistence.EF.BookCategories;
using Library.Services.BookCategories;
using Library.Services.BookCategories.Contracts;
using Library.TestTools.BookCategoreis;
using Library.TestTools.Books;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Library.Services.Tests.Unit.BookCategories
{

    public class BookCategoryServiceTests
    {
        EFDataContext _context;
        EFDataContext _readDataContext;
        BookCategoryRepository _repository;
        BookCategoryService _sut;
        UnitOfWork _unitofwork;
        public BookCategoryServiceTests()
        {
            var db = new EFInMemoryDatabase();
            _context = db.CreateDataContext<EFDataContext>();
            _readDataContext = db.CreateDataContext<EFDataContext>();
            _repository = new EFBookCategoryRepository(_context);
            _unitofwork = new EFUnitOfWork(_context);
            _sut = new BookCategoryAppService(_repository, _unitofwork);
        }

        [Fact]
        public async Task Register_registers_a_bookCategory_properly()
        {
            var dto = BookCategoryFactory.GenerateBookCategoryDto();

            var actual = await _sut.Register(dto);

            var expected = _readDataContext.BookCategories.Single(_ => _.Id == actual);

            expected.Title.Should().Be(dto.Title);

        }

        [Fact]
        public async Task GetBooks_retrieves_books_of_a_bookCategory()
        {
            var frenchStoryCategory = BookCategoryFactory.GenerateBookCategory();
            var frenchBook = new BookBuilder().GenerateAddProductWithBookCategory(frenchStoryCategory).Build();
            _context.Manipulate(_ => _.Books.Add(frenchBook));

            var greekStoryCategory = BookCategoryFactory.GenerateBookCategory("داستان های یونانی");
            var greekBook = new Book
            {
                BookCategory = greekStoryCategory,
                BookCategoryId = greekStoryCategory.Id,
                Title = "زوربای یونانی",
                Author = "نیکوس",
                MinimumAge = 15
            };
            _context.Manipulate(_ => _.Books.Add(greekBook));

            var expected = await _sut.GetBooksOfCategory(frenchStoryCategory.Id);

            expected.Should().HaveCount(1);

        }
    }
}
