using Library.Infrastructure.Domain;
using System;

namespace Library.Entities
{
    public class Entrust : Entity<int>
    {
        public int BookId { get; set; }
        public int MemberId { get; set; }
        public DateTime ReturnDate { get; set; }
        public Book Book  { get; set; }
        public Member Member { get; set; }
    }
}
