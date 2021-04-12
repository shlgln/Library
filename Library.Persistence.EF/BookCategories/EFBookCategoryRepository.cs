using Library.Entities;
using Library.Services.BookCategories.Contracts;
using Microsoft.EntityFrameworkCore;

namespace Library.Persistence.EF.BookCategories
{
    public class EFBookCategoryRepository: BookCategoryRepository
    {
        private readonly EFDataContext _context;
        private readonly DbSet<BookCategory> _set;

        public EFBookCategoryRepository(EFDataContext context)
        {
            _context = context;
            _set = _context.BookCategories;

        }

        public void Add(BookCategory bookCategory)
        {
            _set.Add(bookCategory);
        }
    }
}
