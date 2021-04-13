using Library.Services.Members.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.RestApi.Controllers
{
    [Route("api/members")]
    [ApiController]
    public class MemberController : ControllerBase
    {
        private readonly MemberService _service;

        public MemberController(MemberService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<int> Register(RegisterMembetDto dto)
        {
            return await _service.Register(dto);
        }
    }
}
