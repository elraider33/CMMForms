using Library.Application.Data.Entity.Commands;
using Library.Application.Data.Entity.Queries;
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
    [Authorize]
    public class EntitiesController : BaseController
    {
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<EntityDto>))]
        public async Task<IActionResult> Get()
        {
            var bulletins = await Sender.Send(new GetAllEntities());
            return Ok(bulletins);
        }

        [HttpGet("{id}", Name = "GetEntity")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(EntityDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(EntityDto))]
        public async Task<IActionResult> Get(string id)
        {
            var optionalData = await Sender.Send(new GetEntityById() { Id = id });
            return optionalData.Match<IActionResult>(bulletin => Ok(bulletin), () => NotFound());
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(EntityDto))]
        public async Task<IActionResult> Post([FromForm] CreateEntity bulletin)
        {
            var data = await Sender.Send(bulletin);
            return CreatedAtRoute("GetBulletin", new { id = data.Id }, bulletin);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(EntityDto))]
        public async Task<IActionResult> Put(string id, [FromForm] EntityRequest entity)
        {
            var bulletinOptional = await Sender.Send(new GetEntityById() { Id = id });
            return await bulletinOptional.Match<Task<IActionResult>>(async b =>
            {
                await Sender.Send(new UpdateEntity { Id = id, Entity = entity });
                return NoContent();
            }, async () => NotFound());
        }
    }
}
