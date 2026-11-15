using Microsoft.AspNetCore.Mvc;

namespace FinnyApp.Controllers.Pages
{
    public class ChildController : Controller
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
