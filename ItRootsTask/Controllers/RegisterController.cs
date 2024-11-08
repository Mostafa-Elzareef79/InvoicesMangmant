using Azure.Core;
using ItRootsTask_Core.Features.LoginFeatures.Command;
using ItRootsTask_Core.Features.RegisterFeaturs.Command;
using ItRootsTask_Core.Interfaces.Repositories.RegisterRepo;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Controllers
{
    public class RegisterController : BaseApiController
    {
        private UserModel? UserData { get; set; }

       
        public IActionResult Register()
        {
            return View("RegisterPage");
        }

        [HttpPost("AddRegister")]
        public async Task<IActionResult> AddRegister([FromForm] CmdAddRegister command)
        {
            TempData["registerPage"] = null;
            var result = await Mediator.Send(command);

            if (result.succeeded)
            {
                UserData = result.data;


                HttpContext.Session.SetString("UserData", JsonConvert.SerializeObject(UserData));

                return RedirectToAction("VerficationPage");
            }
            TempData["registerPage"] = result.message;
            return RedirectToAction("Register");
        }


        [HttpPost("verifyCode")]
        public async Task<IActionResult> verifyCode([FromForm] string code)
        {

            var userDataJson = HttpContext.Session.GetString("UserData");
            if (userDataJson != null)
            {
                UserData = JsonConvert.DeserializeObject<UserModel>(userDataJson);
            }

            if (code == UserData?.Code)
            {
                var response = await Mediator.Send(new CmdverifyCode()
                {
                    UserName = UserData?.UserName,
                    VerificationCode = int.Parse(code)
                });
                if (response.succeeded)
                {
                    return RedirectToAction("index", "Home");
                }

            }
            TempData["ErrorMessage"] = "Verification code is incorrect. Please try again.";
            return RedirectToAction("VerficationPage");
        }

        public IActionResult VerficationPage()
        {
            var userDataJson = HttpContext.Session.GetString("UserData");
            if (userDataJson != null)
            {
                var userData = JsonConvert.DeserializeObject<UserModel>(userDataJson);
                ViewBag.ErrorMessage = TempData["ErrorMessage"];
                return View("VerficationPage", userData);
            }

            ViewBag.ErrorMessage = TempData["ErrorMessage"];
            return View("VerficationPage");
        }

    }
}
