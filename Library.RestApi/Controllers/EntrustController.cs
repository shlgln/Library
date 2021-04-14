using Library.Services.Entrusts.Contracts;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Library.RestApi.Controllers
{
    [Route("api/entrusts")]
    [ApiController]
    public class EntrustController : ControllerBase
    {
        private readonly EntrustService _service;

        public EntrustController(EntrustService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<int> Register(RegisterEntrustDto dto)
        {
            return await _service.Register(dto);
        }

        [HttpGet]
        public async Task TackBackBook(int id)
        {
             await _service.TackBackBook(id);
        }
    }
}
