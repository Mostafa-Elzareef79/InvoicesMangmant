using ItRootsTask_Core.Features.LoginFeatures.Command;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Controllers
{
    public class LoginController : BaseApiController
    {
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromForm] CmdLoginCheck command)
        {
            var result = await Mediator.Send(command);

            if (result.succeeded)
                return RedirectToAction("GridPage", "Invoices");

            else if (result.succeeded==false)
            {
                TempData["ErrorMessage"] = result.errors[0];
                return RedirectToAction("index", "Home");
            }

            TempData["ErrorMessage"] = "An unexpected error occurred.";
            return RedirectToAction("index", "Home");
        }

      
    }
}
