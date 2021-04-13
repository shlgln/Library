using FluentAssertions;
using Library.Entities;
using Library.Infrastructure.Application;
using Library.Infrastructure.Test;
using Library.Persistence.EF;
using Library.Persistence.EF.Books;
using Library.Services.Books;
using Library.Services.Books.Contracts;
using Library.TestTools.BookCategoreis;
using Library.TestTools.Books;
using System.Linq;
using Xunit;

namespace Library.Tests.Specs.Books.Edit
{
    public class Successful
    {
        EFDataContext _contex;
        EFDataContext _readDataContext;
        BookCategory newbookCategory;
        BookService _sut;
        UnitOfWork _unitofwork;
        BookRepository _repository;
        Book book;
        public Successful()
        {
            var db = new EFInMemoryDatabase();
            _contex = db.CreateDataContext<EFDataContext>();
            _readDataContext = db.CreateDataContext<EFDataContext>();
            _unitofwork = new EFUnitOfWork(_contex);
            _repository = new EFBookRepository(_contex);
            _sut = new BookAppService(_unitofwork, _repository);
        }

        //[Given("یک دسته‌بندی کتاب با نام داستان‌های یونانی و یک کتاب با نام درجستجوی زمان از دست‌رفته و
        //نویسنده مارسل پروست و رنج سنی بالای 18 سال در لیست کتاب‌های دسته‌بندی داستان‌های فرانسوی وجود دارد.")]
        private void Given()
        {
            newbookCategory = BookCategoryFactory.GenerateBookCategory("داستان‌های یونانی");
            _contex.Manipulate(_ => _.BookCategories.Add(newbookCategory));

            var bookCategory = BookCategoryFactory.GenerateBookCategory("داستان‌های فرانسوی");
            book = new BookBuilder().GenerateAddProductWithBookCategory(bookCategory).Build();
            _contex.Manipulate(_ => _.Books.Add(book));
        }

        //[When("مشخصات کتاب با نام درجستجوی زمان از دست‌رفته و نویسنده مارسل پروست و رنج سنی بالای 18 سال و
        //دسته‌بندی داستان‌های فرانسوی با مشخصات کتاب را به نام زوربای یونانی و نویسنده نیکوس و
        //رنج سنی بالای 15 سال و دسته‌بندی داستان‌های یونانی تغییر می‌دهم")]
        private async void When()
        {
            var dto = BookFactory.GenerateEditBookDto(newbookCategory.Id);
            await _sut.Edit(book.Id, dto);
        }

        //[Then("باید تنها یک کتاب با نام زوربای یونانی و
        //نویسنده نیکوس و رنج سنی بالای 15 سال در دسته‌بندی داستان‌های یونانی وجود داشته‌باشد")]

        private void Then()
        {
            var expected = _readDataContext.Books.Single(_ => _.Id == book.Id);
            expected.BookCategoryId.Should().Be(newbookCategory.Id);
            expected.Title.Should().Be("زوربای یونانی");
            expected.Author.Should().Be("نیکوس");
            expected.MinimumAge.Should().Be(15);
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
