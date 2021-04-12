using Library.Entities;
using Library.Services.BookCategories.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.TestTools.BookCategoreis
{
    public static class BookCategoryFactory
    {
        public static AddBookCategoryDto GenerateBookCategoryDto(string title = "dummy")
        {
            return new AddBookCategoryDto { Title = title };
        }

        public static BookCategory GenerateBookCategory(string title = "داستان های فرانسوی")
        {
            return new BookCategory { Title = title };
        }
    }
}
