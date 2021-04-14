using Library.Entities;
using Library.Services.Books.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Services.BookCategories.Contracts
{
    public class GetCategoryBooksDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public IList<GetBookDto> Books { get; set; }
    }
}
