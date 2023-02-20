
using Microsoft.AspNetCore.Mvc;


namespace finaProject.Controllers
{
    public class AboutController : Controller
    {

        public async Task<IActionResult> Index()
        {
            return View();
        }
    }
}