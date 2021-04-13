using Library.Entities;
using Library.Infrastructure.Application;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Library.Services.Books.Contracts
{
    public interface BookRepository : Repository
    {
        void Add(Book newBook);
        Task<Book> FindBookById(int id);
    }
}
