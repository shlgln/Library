using Library.Infrastructure.Domain;
using System.Collections.Generic;

namespace Library.Entities
{
    public class Book: Entity<int>
    {
        public Book()
        {
            Entrusts = new HashSet<Entrust>();
        }
        public int BookCategoryId { get; set; }
        public string  Title { get; set; }
        public string Author { get; set; }
        public byte MinimumAge { get; set; }
        public BookCategory BookCategory { get; set; }
        public HashSet<Entrust> Entrusts { get; set; }

    }
}
