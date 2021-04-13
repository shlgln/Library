using Library.Services.Books.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.RestApi.Controllers
{
    [Route("api/books")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly BookService _service;

        public BookController(BookService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<int> Register(RegisterBookDto dto)
        {
            return await _service.Register(dto);
        }


        [HttpPut]
        public async Task Edit(int id, EditBookDto dto)
        {
            await _service.Edit(id, dto);
        }
    }
}
