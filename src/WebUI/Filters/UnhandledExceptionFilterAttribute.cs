using Library.Application.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System.Net;

namespace WebUI.Filters
{
    public class UnhandledExceptionFilterAttribute : ExceptionFilterAttribute
    {
        private readonly ILogger<UnhandledExceptionFilterAttribute> _logger;
        private readonly IEmailHelper _emailHelper;

        public UnhandledExceptionFilterAttribute(ILogger<UnhandledExceptionFilterAttribute> logger, IEmailHelper emailHelper)
        {
            _logger = logger;
            _emailHelper = emailHelper;
        }

        public override void OnException(ExceptionContext context)
        {
            // Customize this object to fit your needs
            var result = new ObjectResult(new
            {
                context.Exception.Message, // Or a different generic message
                context.Exception.Source,
                ExceptionType = context.Exception.GetType().FullName,
            })
            {
                StatusCode = (int)HttpStatusCode.InternalServerError
            };

            var errorMessage = @"
             Error: " + context.Exception.Message + @"\n
             StackTrace: " + context.Exception.StackTrace + @"";
            _emailHelper.SendEmail(errorMessage);
            // Log the exception
            _logger.LogError("Unhandled exception occurred while executing request: {ex}", context.Exception);

            // Set the result
            context.Result = result;
        }
    }
}
