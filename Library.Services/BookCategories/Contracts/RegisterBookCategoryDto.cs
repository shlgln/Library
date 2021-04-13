using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Library.Services.BookCategories.Contracts
{
    public class RegisterBookCategoryDto
    {
        [Required]
        public string Title { get; set; }
    }
}
