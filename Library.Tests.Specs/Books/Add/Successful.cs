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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace Library.Tests.Specs.Books.Add
{
    public class Successful
    {
        EFDataContext _contex;
        EFDataContext _readDataContext;
        BookCategory bookCategory;
        BookService _sut;
        UnitOfWork _unitofwork;
        BookRepository _repository;
        private int actualResult;
        public Successful()
        {
            var db = new EFInMemoryDatabase();
            _contex = db.CreateDataContext<EFDataContext>();
            _readDataContext = db.CreateDataContext<EFDataContext>();
            _unitofwork = new EFUnitOfWork(_contex);
            _repository = new EFBookRepository(_contex);
            _sut = new BookAppService(_unitofwork, _repository);
        }

        //[Given("یک دسته‌بندی به نام داستان‌های فرانسوی در لیست دسته‌بندی کتاب وجود دارد.")]
        private void Given()
        {
            bookCategory = BookCategoryFactory.GenerateBookCategory();
            _contex.Manipulate(_ => _.BookCategories.Add(bookCategory));
        }

        //[When("کتابی با نام در جستجوی زمان از دست‌رفته  و نویسنده مارسل‌پروست و
        //رنج سنی بالای 18سال را در دسته‌بندی داستان‌های فرانسوی، تعریف می‌کنم")]
        private async void When()
        {
            var dto = BookFactory.GenerateRegisterBookDto(bookCategory.Id);
            actualResult = await _sut.Register(dto);
        }

        //[Then("باید یک کتاب با نام درجستجوی زمان از دست‌رفته و نویسنده مارسل‌پروست و
        //رنج سنی بالای 18سال در لیست کتاب‌های دسته‌بندی با نام داستان‌های فرانسوی وجود داشته باشد")]
        private void Then()
        {
            var expected = _readDataContext.Books.Single(_ => _.Id == actualResult);
            expected.BookCategoryId.Should().Be(bookCategory.Id);
            expected.Title.Should().Be("در جستجوی زمان از دست رفته");
            expected.Author.Should().Be("مارسل پروست");
            expected.MinimumAge.Should().Be(18);
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
