using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mail;
using System.Net;
using Core.Entities.Concrete;
using Business.Services.Intefaces;
using System.Xml.Linq;
using Core.Utilities;

namespace finaProject.Areas.doctor.Controllers
{
    [Area("Doctor")]
    [Authorize(Roles = "doctor")]
    public class RezervationController : Controller
    {
        private readonly IDoctorService _service;
        private readonly UserManager<AppUser> _userManager;

        public RezervationController(UserManager<AppUser> userManager, IDoctorService service)
        {
            _userManager = userManager;
            _service = service;
        }
        public async Task<IActionResult> Index()
        {
            string name = User.Identity.Name;
            AppUser user = await _userManager.FindByNameAsync(name);
            DoctorGetDto getDto = await _service.Get(d => d.Email == name, "rezervs");
            if (getDto == null)
            {
                return View();
            }
            return View(getDto);
        }
        public async Task<IActionResult> Rezerv(int id, string time, string user)
        {
            DoctorGetDto getDto = await _service.Get(d => d.Id == id, "Icons", "rezervs");

            foreach (var item in getDto.rezervs)
            {
                if (item.Time == time)
                {
                    item.Busy = !item.Busy;
                    if (!item.Busy) Mail.SendMessage(user, "Rezervasiya", "Teessufle bildiririk ki, rezervasiyaniz qebul olunmadi");
                }
            };
            DoctorUpdateDto dto = new DoctorUpdateDto { getDto = getDto };
            await _service.UpdateAsync(dto);
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Okei()
        {
            List<DoctorGetDto> docs = await _service.GetAllAsync(d=>!d.IsDeleted);
            foreach (DoctorGetDto doc in docs)
            {
                foreach (var item in doc.rezervs)
                {
                    if (item.Busy)
                        Mail.SendMessage(item.UserEmail, "Rezervasiya", "Rezervasiyaniz qebul olundu,zehmet olmasa gecikmeyin.Tesekkurler;)");
                }
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
