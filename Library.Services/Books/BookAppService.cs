using Library.Entities;
using Library.Infrastructure.Application;
using Library.Services.Books.Contracts;
using Library.Services.Books.Exceptions;
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

        public async Task Edit(int id, EditBookDto dto)
        {
            var book = await _repository.FindBookById(id);
            if (book == null)
                throw new NotFoundBookByIdException();

            book.BookCategoryId = dto.BookCategoryId;
            book.Title = dto.Title;
            book.Author = dto.Author;
            book.MinimumAge = dto.MinimumAge;

            await _unitofwork.Complete();
        }

        public async Task<int> Register(RegisterBookDto dto)
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
