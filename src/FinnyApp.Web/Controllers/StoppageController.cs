using Microsoft.AspNetCore.Mvc;
using FinnyApp.BLL.Services;
using FinnyApp.Web.ViewModels;

// This Controller is for global actions of joining and logging users alike whether they are subusers or users. 

namespace FinnyApp.Web.Controllers
{
    public class StoppageController : Controller
    {
        [Route("/")]
        public IActionResult Landing()
        {
            return View("Landing");
        }

        [HttpGet("Stoppage")]
        public IActionResult Stoppage()
        {
            return View();
        }

        [HttpPost("Stoppage/Sign")]
        public async Task<IActionResult> Sign(UserSignUpViewModel model)
        {
            return View();
        }

        [HttpPost("Stoppage/Login")]
        public async Task<IActionResult> Login(UserLoginViewModel model)
        {
            return View();
        }

        [HttpPost("Stoppage/SubLogin")]
        public async Task<IActionResult> SubLogin(SubUserLoginViewModel model)
        {
            return View();
        }
    }
}
