using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Services.Books.Contracts
{
    public class GetBookDto
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public byte MinimumAge { get; set; }
    }
}
