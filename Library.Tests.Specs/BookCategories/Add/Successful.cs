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

namespace Library.Tests.Specs.BookCategories.Add
{
    public class Successful
    {
        EFDataContext _context;
        EFDataContext _readDataContext;
        BookCategoryRepository _repository;
        BookCategoryService _sut;
        UnitOfWork _unitofwork;
        private int bookId;
        public Successful()
        {
            var db = new EFInMemoryDatabase();
            _context = db.CreateDataContext<EFDataContext>();
            _readDataContext = db.CreateDataContext<EFDataContext>();
            _repository = new EFBookCategoryRepository(_context);
            _unitofwork = new EFUnitOfWork(_context);
            _sut = new BookCategoryAppService(_repository, _unitofwork);
        }

        private void Given()
        {
        }

        //[When("من یک دسته‌بندی کتاب با نام داستان‌های فرانسوی تعریف می‌کنم")]
        private async Task When()
        {
            var dto = BookCategoryFactory.GenerateBookCategoryDto("داستان های فرانسوی");
            bookId = await _sut.Register(dto);
        }

        //[Then("باید یک دسته‌بندی کتاب با نام داستان‌های فرانسوی در لیست دسته‌بندی کتاب وجود داشته‌باشد")]
        private void Then()
        {
            var expected = _readDataContext.BookCategories.Single(_ => _.Id == bookId);
            expected.Id.Should().Be(bookId);
        }

        [Fact]
        public void Run()
        {
            Given();
            When();
            Then();
        }
    }
}
