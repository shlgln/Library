using Library.Infrastructure.Application;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Library.Services.Books.Contracts
{
    public interface BookService : Service
    {
        Task<int> Register(RegisterBookDto dto);
        Task Edit(int id, EditBookDto dto);
    }
}
