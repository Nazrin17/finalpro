using AutoMapper;
using Business.Services.Intefaces;
using Microsoft.AspNetCore.Mvc;

namespace finaProject.Controllers
{
    public class HomeController : Controller
    {

        private readonly IHomeService _service;

        public HomeController( IHomeService service)
        {
            _service = service;
        }

        public  async Task<IActionResult> Index()
        {
            HomeGetDto getdto = await _service.Get();
            if(getdto == null)
            {
                return View();
            }
            HomeSearchDto searchDto = new HomeSearchDto{ getDto = getdto };
            return View(searchDto);
        }
    }
}