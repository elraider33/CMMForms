using Library.Application.Data.Role.Commands;
using Library.Application.Data.Role.Queries;
using Library.Application.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebUI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class RolesController : BaseController
    {
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<RoleDto>))]
        public async Task<IActionResult> Get()
        {
            var roles = await Sender.Send(new GetRoles());
            return Ok(roles);
        }

        [HttpGet("{id}", Name = "GetRole")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(RoleDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(RoleDto))]
        public async Task<IActionResult> Get(string id)
        {
            var role = await Sender.Send(new GetRoleById() { Id = id});
            if (role == null) return NotFound();
            return Ok(role);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(RoleDto))]
        public async Task<IActionResult> Post([FromBody]CreateRole role)
        {
            var data = await Sender.Send(role);
            return CreatedAtRoute("GetRole", new {id = data.Id}, data);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(RoleDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(RoleDto))]
        public async Task<IActionResult> Put(string id, [FromBody] UpdateRole role)
        {
            var data = await Sender.Send(new GetRoleById() { Id = id });
            if (data == null) return NotFound();
            await Sender.Send(role);
            return NoContent();
        }
        
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(RoleDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(RoleDto))]
        public async Task<IActionResult> Delete(string id)
        {
            var data = await Sender.Send(new GetRoleById() { Id = id });
            if (data == null) return NotFound();
            await Sender.Send(new DeleteRole() { Id = id });
            return NoContent();
        }
    }
}
