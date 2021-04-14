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
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Library.Tests.Specs.BookCategories.Display
{
    public class DisplayBookOfACategory
    {
        EFDataContext _context;
        EFDataContext _readDataContext;
        BookCategoryRepository _repository;
        BookCategoryService _sut;
        UnitOfWork _unitofwork;
        BookCategory frenchStoryCategory;
        GetCategoryBooksDto expected;
        Book frenchBook;

        public DisplayBookOfACategory()
        {
            var db = new EFInMemoryDatabase();
            _context = db.CreateDataContext<EFDataContext>();
            _readDataContext = db.CreateDataContext<EFDataContext>();
            _repository = new EFBookCategoryRepository(_context);
            _unitofwork = new EFUnitOfWork(_context);
            _sut = new BookCategoryAppService(_repository, _unitofwork);
        }

        //[Given("کتاب‌ با نام درجستجوی زمان از دست‌رفته و نویسنده مارسل پروست و رنج سنی بالای 18 در دسته‌بندی کتاب با نام داستان‌های فرانسوی
        //و یک کتاب دیگر با نام زوربای یونانی و نویسنده نیکوس و رنج سنی بالای 15 سال در دسته‌بندی کتاب با نام داستان‌های یونانی وجود دارد.")]
        private void Given()
        {
            frenchStoryCategory = BookCategoryFactory.GenerateBookCategory();
            frenchBook = new BookBuilder().GenerateAddBookWithBookCategory(frenchStoryCategory).Build();
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
        }

        //[When("دسته‌بندی کتاب با نام داستان‌های فرانسوی را انتخاب می‌کنم")]
        private async void When()
        {
            expected = await  _sut.GetBooksOfCategory(frenchStoryCategory.Id);
        }

        //[Then("باید فقط کتاب با نام درجستجوی زمان از دست‌رفته و نویسنده مارسل پروست و
        //رنج سنی بالای 18 سال در لیست کتاب‌های دسته‌بندی با نام داستان‌های فرانسوی وجود داشته باشد")]
        private void Then()
        {
            expected.Title.Should().Be("داستان های فرانسوی");
            expected.Books.Should().HaveCount(1).
                And.ContainSingle(_ => _.Title == "در جستجوی زمان از دست رفته" 
                                  && _.MinimumAge == 18 && _.Author == "مارسل پروست" );
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
