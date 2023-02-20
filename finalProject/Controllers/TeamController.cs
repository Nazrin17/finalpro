
using AutoMapper;
using Business.Services.Intefaces;
using Entities.Concrete.Models;
using Entities.Dtos.ResHistory;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol.Core.Types;

namespace finaProject.Controllers
{
    public class TeamController : Controller
    {
        private readonly IDoctorService _service;
        private readonly IResHistoryService _res;
        private readonly IMapper _mapper;

        public TeamController(IDoctorService service, IResHistoryService res, IMapper mapper)
        {
            _service = service;
            _res = res;
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Rezerv(int id, string time, string user)
        {
            DoctorGetDto get = await _service.Get(d => d.Id == id, "Icons", "rezervs");
            foreach (var item in get.rezervs)
            {
                if (item.Time == time)
                {

                    item.Busy = !item.Busy;
                    item.UserEmail = user;
                    ResHistory history= new ResHistory() { UserEmail= user,date=DateTime.Now,Doctor=_mapper.Map<Doctor>(get)};
                    await _service.AddHistory(history,get.Id);
                }
            };
            DoctorUpdateDto updateDto = new DoctorUpdateDto { getDto = get };
            await _service.UpdateAsync(updateDto);
            return RedirectToAction("Index", "Reservation");
        }
    }
}
