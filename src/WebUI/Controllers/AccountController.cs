using Library.Application.Data.User.Commands;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebUI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : BaseController
    {
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] AuthUser user)
        {
            var token = await Sender.Send(user);
            if(token.StatusCode == 404)
            {
                return NotFound(token);
            }
            if (token.StatusCode == 401)
            {
                return BadRequest(token);
            }
            return Ok(token);
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterUser user)
        {
            var data = await Sender.Send(user);
            return Ok(data);
        }
        
        [HttpPost("LoginDocksite")]
        public async Task<IActionResult> LoginDocksite([FromBody] AuthDockSiteUser user)
        {
            var token = await Sender.Send(user);
            if (token.StatusCode == 404)
            {
                return NotFound(token);
            }
            if (token.StatusCode == 401)
            {
                return BadRequest(token);
            }
            return Ok(token);
        }
    }
}
