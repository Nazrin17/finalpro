using AutoMapper;
using Business.Services.Intefaces;
using Core.Entities.Concrete;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace finaProject.Controllers
{
    public class ReservationController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IDoctorService _service;
        public ReservationController( UserManager<AppUser> userManager, IDoctorService service)
        {
            _userManager = userManager;
            _service = service;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Index(ReservationDto rez)
        {
            DoctorGetDto doc = await _service.Get(d => d.Name == rez.DoctorName && d.Surname == rez.DoctorSurname, "rezervs");
            if (doc == null)
            {
                ModelState.AddModelError("", "Doctor not found");
                return View();
            }
            if (!User.Identity.IsAuthenticated)
            {
                ModelState.AddModelError("", "Please first login or register");
                return View();
            }
            if (rez.UserName != User.Identity.Name)
            {
                ModelState.AddModelError("", "Please enter your name correctly");
                return View();
            }
            AppUser user = await _userManager.FindByNameAsync(rez.UserName);
            if (user.Email != rez.UserEmail)
            {
                ModelState.AddModelError("", "Please enter your email correctly");
                return View();
            }
            return RedirectToAction(nameof(Next),rez);
        }
        public async Task<IActionResult> Next(ReservationDto rez)
        {
       
            DoctorGetDto getDto= await _service.Get(d => d.Name == rez.DoctorName && d.Surname == rez.DoctorSurname, "rezervs");
            rez.getDto = getDto;
            return View(rez);
        }

    }
}
