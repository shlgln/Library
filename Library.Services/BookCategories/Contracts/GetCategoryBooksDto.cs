using Library.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Services.BookCategories.Contracts
{
    public class GetCategoryBooksDto
    {
        public string Title { get; set; }
        public IList<string> Books { get; set; }
    }
}
