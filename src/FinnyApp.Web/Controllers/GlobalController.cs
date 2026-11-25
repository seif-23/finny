using Microsoft.AspNetCore.Mvc;

namespace FinnyApp.Web.Controllers
{
    public class GlobalController : Controller
    {
        public IActionResult Home()
        {
            return View();
        }
    }
}
