using AutoMapper;
using Business.Services.Intefaces;
using Core.Entities.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace finaProject.Areas.doctor.Controllers
{
    [Area("Doctor")]
    [Authorize(Roles ="doctor")]
    public class ProfileController : Controller
    {
        private readonly IDoctorService _service;
        private readonly UserManager<AppUser> _userManager;

        public ProfileController(IDoctorService service, UserManager<AppUser> userManager)
        {
            _service = service;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            string name = User.Identity.Name;
            AppUser user = await _userManager.FindByNameAsync(name);
            DoctorGetDto getDto = await _service.Get(d => d.Email == name && !d.IsDeleted, "rezervs", "Position");
            if (getDto == null)
            {
                return View();
            }
            return View(getDto);
        }
    }
}
