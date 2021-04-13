using Library.Services.BookCategories;
using Library.Services.BookCategories.Contracts;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Library.RestApi.Controllers
{
    [Route("api/book-categories")]
    [ApiController]
    public class BookCategoryController : ControllerBase
    {
        private readonly BookCategoryService _service;

        public BookCategoryController(BookCategoryService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<int> Register(RegisterBookCategoryDto dto)
        {
            return await _service.Register(dto);
        }

        [HttpGet]
        public async Task<IList<GetCategoryBooksDto>> GetBooks(int id)
        {
            return await _service.GetBooksOfCategory(id);
        }
    }
}
