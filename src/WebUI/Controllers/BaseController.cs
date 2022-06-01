using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace WebUI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BaseController : ControllerBase
    {
        private IMediator _sender;
        protected IMediator Sender => _sender ??= HttpContext.RequestServices.GetService<IMediator>();
    }
}