using Library.Application.Data.Bulletin.Queries;
using Library.Application.Data.CMM.Command;
using Library.Application.Data.CMM.Queries;
using Library.Application.Dtos;
using Microsoft.AspNetCore.Authorization;
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
    [RequestSizeLimit(long.MaxValue)]
    [Authorize]
    public class ManualsController : BaseController
    {
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<CMMDto>))]
        public async Task<IActionResult> Get()
        {
            List<CMMDto> cmms = null;
            var userRole = HttpContext.User.Claims.First(c => c.Type.Contains("role")).Value;
            if (userRole.ToLower() == "tdadmin" || userRole.ToLower() == "internal")
            {
                cmms = await Sender.Send(new GetAllCMMs());
            }
            else if (userRole.ToLower() != "guest")
            {
                cmms = await Sender.Send(new GetAllCMMsByRole() { Role = userRole });
            }
            return Ok(cmms);
        }

        [HttpGet("{id}", Name = "GetCMMs")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CMMDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(CMMDto))]
        public async Task<IActionResult> Get(string id)
        {
            var optionalData = await Sender.Send(new GetCMMById() { Id = id });
            return optionalData.Match<IActionResult>(Ok, NotFound);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(CMMDto))]
        [DisableRequestSizeLimit]
        public async Task<IActionResult> Post([FromForm] CreateCMM cmm)
        {
            var data = await Sender.Send(cmm);
            return CreatedAtRoute("GetCMMs", new { id = data.Id }, cmm);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(CMMDto))]
        [DisableRequestSizeLimit]
        public async Task<IActionResult> Put(string id, [FromForm] CMMRequestDto cmm)
        {
            var bulletinOptional = await Sender.Send(new GetCMMById() { Id = id });
            return await bulletinOptional.Match<Task<IActionResult>>(async b =>
            {
                await Sender.Send(new UpdateCMM { Id = id, CMM = cmm });
                return NoContent();
            }, () => Task.FromResult<IActionResult>(NotFound()));
        }

        [HttpGet("Roles")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<CMMDto>))]
        public async Task<IActionResult> GetRoles()
        {
            Dictionary<string, List<CMMDto>> cmms = null;
            var userRole = HttpContext.User.Claims.First(c => c.Type.Contains("role")).Value;
            if (userRole.ToLower() == "tdadmin" || userRole.ToLower() == "internal")
            {
                cmms = await Sender.Send(new GetCMMByRole());
            }
            else if (userRole.ToLower() != "guest")
            {
                cmms = await Sender.Send(new GetCMMByRoleRole() { Role = userRole });
            }
            return Ok(cmms);
        }
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Delete(string id)
        {
            var bulletinOptional = await Sender.Send(new GetCMMById() { Id = id });
            return await bulletinOptional.Match<Task<IActionResult>>(async b =>
            {
                await Sender.Send(new DeleteCMM() { Id = id });
                return NoContent();
            }, () => Task.FromResult<IActionResult>(NotFound()));
        }
    }
}
