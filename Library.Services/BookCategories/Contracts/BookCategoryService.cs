using Library.Infrastructure.Application;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Library.Services.BookCategories.Contracts
{
    public interface BookCategoryService : Service
    {
        Task<int> Register(AddBookCategoryDto dto);
    }
}
