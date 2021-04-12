using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Library.Services.Books.Contracts
{
    public class AddBookDto
    {
        [Required]
        public int BookCategoryId { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Author { get; set; }
        [Required]
        public byte MinimumAge { get; set; }

    }
}
