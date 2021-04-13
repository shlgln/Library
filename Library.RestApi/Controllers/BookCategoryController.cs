using Library.Services.BookCategories;
using Library.Services.BookCategories.Contracts;
using Microsoft.AspNetCore.Mvc;
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
        public async Task<int> Register([FromBody]RegisterBookCategoryDto dto)
        {
            return await _service.Register(dto);
        }
    }
}
