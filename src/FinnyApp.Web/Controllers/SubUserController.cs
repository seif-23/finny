using Microsoft.AspNetCore.Mvc;

namespace FinnyApp.Web.Controllers
{
    public class SubUserController : Controller
    {
        [Route("/Me")]
        public IActionResult Me()
        {
            return View();
        }

        [Route("/Me/Task")]
        public IActionResult Task()
        {
            return View();
        }

        [Route("/Me/Goal")]
        public IActionResult Goal()
        {
            return View();
        }
    }
}
