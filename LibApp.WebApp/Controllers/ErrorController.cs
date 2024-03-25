using Microsoft.AspNetCore.Mvc;

namespace LibApp.WebApp.Controllers;

[Route("Error")]
public class ErrorController : Controller
{
    [Route("ServerError")]
    public IActionResult ServerError()
    {
        return View("Error/ServerError");
    }

    [Route("StatusCode/{statusCode}")]
    public IActionResult StatusCode(int statusCode)
    {
        return View(statusCode == 404 ? "Error/NotFound" : "Error/ServerError");
    }
}