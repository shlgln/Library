using Library.Infrastructure.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Entities
{
    public class BookCategory: Entity<int>
    {
        public BookCategory()
        {
            Books = new  HashSet<Book>();
        }
        public string Title { get; set; }
        public HashSet<Book> Books { get; set; }
    }
}
