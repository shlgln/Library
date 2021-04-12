using Library.Entities;
using Library.Infrastructure.Application;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Services.BookCategories.Contracts
{
    public interface BookCategoryRepository : Repository
    {
        void Add(BookCategory bookCategory);
    }
}
