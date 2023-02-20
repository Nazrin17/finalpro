using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace finaProject.Controllers
{
    public class ServiceController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}