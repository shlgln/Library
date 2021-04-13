using Library.Entities;
using Library.Services.BookCategories.Contracts;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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

        public async Task<IList<GetCategoryBooksDto>> GetCategoryBooks(int id)
        {
            var query = _set.Where(_ => _.Id == id).Select(_ => new GetCategoryBooksDto
            {
                Title = _.Title,
                Books = _.Books.Select(_ => _.Title).ToList()

            });
            return await query.ToListAsync();
        }
    }
}
