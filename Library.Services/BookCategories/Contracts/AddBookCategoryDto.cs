using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Library.Services.BookCategories.Contracts
{
    public class AddBookCategoryDto
    {
        [Required]
        public string Title { get; set; }
    }
}
