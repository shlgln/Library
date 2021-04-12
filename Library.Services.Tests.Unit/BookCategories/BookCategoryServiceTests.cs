using FluentAssertions;
using Library.Infrastructure.Application;
using Library.Infrastructure.Test;
using Library.Persistence.EF;
using Library.Persistence.EF.BookCategories;
using Library.Services.BookCategories;
using Library.Services.BookCategories.Contracts;
using Library.TestTools.BookCategoreis;
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
    }
}
