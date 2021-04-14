using Library.Entities;
using Library.Services.BookCategories.Contracts;
using Library.Services.Books.Contracts;
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

        public async Task<GetCategoryBooksDto> GetBooksOfACategory(int id)
        {
            var query = _set.Select(_ => new GetCategoryBooksDto
            {
                Id = _.Id,
                Title = _.Title,
                Books = _.Books.Select(_ => new GetBookDto
                { 
                    Title = _.Title,
                    Author = _.Author,
                    MinimumAge = _.MinimumAge
                }).ToList()

            });
            return await query.SingleOrDefaultAsync(_ => _.Id == id);
        }
    }
}
