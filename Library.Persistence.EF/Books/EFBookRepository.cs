using Library.Entities;
using Library.Services.Books.Contracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Persistence.EF.Books
{
    public class EFBookRepository:BookRepository
    {
        private readonly EFDataContext _context;
        private readonly DbSet<Book> _set;

        public EFBookRepository(EFDataContext context)
        {
            _context = context;
            _set = _context.Books;
        }

        public void Add(Book newBook)
        {
            _set.Add(newBook);
        }
    }
}
