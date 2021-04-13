using Library.Entities;
using Library.Infrastructure.Application;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Library.Services.BookCategories.Contracts
{
    public interface BookCategoryRepository : Repository
    {
        void Add(BookCategory bookCategory);
        Task<IList<GetCategoryBooksDto>> GetCategoryBooks(int id);
    }
}
