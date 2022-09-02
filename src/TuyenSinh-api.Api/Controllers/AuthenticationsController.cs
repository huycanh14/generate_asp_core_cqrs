using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TuyenSinh_api.Api.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthenticationsController : Controller
    {
        private readonly ILogger<AuthenticationsController> _logger;

        public AuthenticationsController(ILogger<AuthenticationsController> logger)
        {
            _logger = logger;
        }
        //[HttpPost("registor")]
        //public async Task<IActionResult> Registor([FromBody] string value)
        //{
        //    return Ok();
        //}

        //[HttpPost("login")]
        //public async Task<IActionResult> Login([FromBody] string value)
        //{
        //    return Ok();
        //}

    }
}
