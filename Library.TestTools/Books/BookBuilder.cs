using Library.Entities;
using Library.Services.Books.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.TestTools.Books
{
    public class BookBuilder
    {
        private int _BookCategoryId = 1;
        BookCategory _BookCategory;
        private string _Author = "مارسل پروست";
        private string _Title = "در جستجوی زمان از دست رفته";
        private byte _MinimumAge = 18;
        public BookBuilder GenerateAddProductWithBookCategory(BookCategory bookCategory)
        {
            _BookCategory = bookCategory;
            return this;
        }
        public Book Build()
        {
            return new Book
            {
                BookCategory = _BookCategory,
                BookCategoryId = _BookCategoryId,
                Title = _Title,
                Author = _Author,
                MinimumAge = _MinimumAge
            };
        }
    }
}
