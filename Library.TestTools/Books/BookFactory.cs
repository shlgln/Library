using Library.Services.Books.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.TestTools.Books
{
    public static class BookFactory
    {
        public static RegisterBookDto GenerateRegisterBookDto(int bookCategoryId)
        {
            return new RegisterBookDto
            {
                BookCategoryId = bookCategoryId,
                Title = "در جستجوی زمان از دست رفته",
                Author = "مارسل پروست",
                MinimumAge = 18
            };
        }
        public static EditBookDto GenerateEditBookDto(int bookCategoryId)
        {
            return new EditBookDto
            {
                BookCategoryId = bookCategoryId,
                Title = "زوربای یونانی",
                Author = "نیکوس",
                MinimumAge = 15
            };
        }
    }
}
