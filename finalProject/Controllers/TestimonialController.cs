
using Microsoft.AspNetCore.Mvc;

namespace finaProject.Controllers
{
    public class TestimonialController : Controller
    {

        public async Task< IActionResult> Index()
        {
            return View();
        }
    }
}
