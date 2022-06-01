using System.Collections.Generic;
using System.Threading.Tasks;
using Library.Application.Data.Bulletin.Command;
using Library.Application.Data.Bulletin.Queries;
using Library.Application.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Linq;

namespace WebUI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class BulletinsController : BaseController
    {

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<BulletinDto>))]
        public async Task<IActionResult> Get()
        {
            List<BulletinDto> bulletins = null;
            var userRole = HttpContext.User.Claims.First(c => c.Type.Contains("role")).Value;
            if (string.Equals(userRole, "tdadmin", System.StringComparison.OrdinalIgnoreCase) || string.Equals(userRole, "internal", System.StringComparison.OrdinalIgnoreCase))
            {
                bulletins = await Sender.Send(new GetAllBulletins());
            }
            else if(!string.Equals(userRole, "guest", System.StringComparison.OrdinalIgnoreCase))
            {
                bulletins = await Sender.Send(new GetAllBulletinsByRole() { Role = userRole });
            }
            return Ok(bulletins);
        }

        [HttpGet("{id}", Name = "GetBulletin")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(BulletinDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(BulletinDto))]

        public async Task<IActionResult> Get(string id)
        {
            var optionalData = await Sender.Send(new GetBulletinById() {Id = id});
            return optionalData.Match<IActionResult>(Ok, NotFound);
        }
        
        [HttpPost]
        [DisableRequestSizeLimit]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(BulletinDto))]
        public async Task<IActionResult> Post([FromForm] CreateBulletin bulletin)
        {
            var data = await Sender.Send(bulletin);
            return CreatedAtRoute("GetBulletin", new {id = data.Id}, bulletin);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(BulletinDto))]
        [DisableRequestSizeLimit]
        public async Task<IActionResult> Put(string id, [FromForm] BulletinRequestDto bulletin)
        {
            var bulletinOptional = await Sender.Send(new GetBulletinById() {Id = id});
            return await bulletinOptional.Match<Task<IActionResult>>(async b =>
            {
                await Sender.Send(new UpdateBulletin { Id = id, Bulletin = bulletin});
                return NoContent(); 
            }, () => Task.FromResult<IActionResult>(NotFound()));
        }

        [HttpGet("Roles")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<BulletinDto>))]
        public async Task<IActionResult> GetRoles()
        {
            Dictionary<string,List<BulletinDto>> bulletins = null;
            var userRole = HttpContext.User.Claims.First(c => c.Type.Contains("role")).Value;
            if (userRole.ToLower() == "tdadmin" || userRole.ToLower() == "internal")
            {
                bulletins = await Sender.Send(new GetBulletinByRole());
            }
            else if (userRole.ToLower() != "guest")
            {
                bulletins = await Sender.Send(new GetAllBulletinsByRoleRole() { Role = userRole });
            }
            return Ok(bulletins);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Delete(string id)
        {
            var bulletinOptional = await Sender.Send(new GetBulletinById() { Id = id });
            return await bulletinOptional.Match<Task<IActionResult>>(async b =>
            {
                await Sender.Send(new DeleteBulletin() { Id = id });
                return NoContent();
            }, () => Task.FromResult<IActionResult>(NotFound()));
        }
    }
}