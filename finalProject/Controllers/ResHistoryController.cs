using Business.Services.Intefaces;
using Core.Entities.Concrete;
using Entities.Concrete.Models;
using Entities.Dtos.ResHistory;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace finalProject.Controllers
{
    public class ResHistoryController : Controller
    {
        private readonly IResHistoryService _service;
        private readonly UserManager<AppUser> _manager;
        private readonly IDoctorService _doctor;

        public ResHistoryController(IResHistoryService service, UserManager<AppUser> manager, IDoctorService doctor)
        {
            _service = service;
            _manager = manager;
            _doctor = doctor;
        }

        public async Task<IActionResult> Index()
        {
            AppUser user = await _manager.FindByNameAsync(User.Identity.Name);
            List<ResGetDto>history = await _service.GetAllAsync(h => h.UserEmail == user.Email);
            List<Doctor> docs = new List<Doctor>();
            foreach (var item in history)
            {
                docs.Add(item.Doctor);
            }
            List<DoctorGetDto> getDtos = new List<DoctorGetDto>();
            foreach (var item in docs)
            {
              getDtos.Add( await _doctor.Get(d=>d.Id==item.Id,"Position","rezervs"));
            }
            ShowDto show = new ShowDto { DoctorGet = getDtos, ResGet = history };
            return View(show);
        }
    }
}
