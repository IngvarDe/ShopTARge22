using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace ShopTARge22.Controllers
{
    public class ErrorController : Controller
    {
        private readonly ILogger<ErrorController> _logger;

        public ErrorController(ILogger<ErrorController> logger)
        {
                _logger = logger;
        }


        public IActionResult HttpStatusCodeHandler(int statusCode)
        {
            var statusCoderesult = HttpContext.Features.Get<IStatusCodeReExecuteFeature>();

            switch (statusCode)
            {
                case 404:
                    ViewBag.ErrorMessage = "Sorry, the resource you requested could not be founf";
                    _logger.LogWarning($"404 Error occured. Path = {statusCoderesult.OriginalPath}" +
                        $"and QueryString = {statusCoderesult.OriginalQueryString}");
                    break;
            }
            return View("NotFound");
        }

        [Route("Error")]
        [AllowAnonymous]
        public IActionResult Error()
        {
            var exceptionDetails = HttpContext.Features.Get<IExceptionHandlerFeature>();
            _logger.LogError($"The path {exceptionDetails.Path} threw an exception "
                + $"{exceptionDetails.Error}");

            return View("Error");
        }
    }
}
