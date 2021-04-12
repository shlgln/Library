using Library.Entities;
using Library.Infrastructure.Application;
using Library.Services.Books.Contracts;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Library.Services.Books
{
    public class BookAppService: BookService
    {
        private readonly UnitOfWork _unitofwork;
        private readonly BookRepository _repository;

        public BookAppService(UnitOfWork unitOfWork, BookRepository repository)
        {
            _unitofwork = unitOfWork;
            _repository = repository;
        }

        public async Task<int> Register(AddBookDto dto)
        {
            var newBook = new Book
            {
                BookCategoryId = dto.BookCategoryId,
                Title = dto.Title,
                Author = dto.Author,
                MinimumAge = dto.MinimumAge
            };

            _repository.Add(newBook);

            await _unitofwork.Complete();
            return newBook.Id;
        }
    }
}
