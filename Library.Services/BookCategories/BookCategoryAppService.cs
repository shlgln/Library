using Library.Entities;
using Library.Infrastructure.Application;
using Library.Services.BookCategories.Contracts;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Library.Services.BookCategories
{
    public class BookCategoryAppService: BookCategoryService
    {
        private readonly UnitOfWork _unitofwork;
        private readonly BookCategoryRepository _repository;

        public BookCategoryAppService(BookCategoryRepository repository, UnitOfWork unitOfWork)
        {
            _unitofwork = unitOfWork;
            _repository = repository;
        }

        public async Task<GetCategoryBooksDto> GetBooksOfCategory(int id)
        {
            return await _repository.GetBooksOfACategory(id);
        }

        public async Task<int> Register(RegisterBookCategoryDto dto)
        {
            var bookCategory = new BookCategory
            {
                Title = dto.Title
            };

            _repository.Add(bookCategory);
            await _unitofwork.Complete();
            return bookCategory.Id;
        }
    }
}
