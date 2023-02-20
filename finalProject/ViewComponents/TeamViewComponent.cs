using AutoMapper;
using Business.Services.Intefaces;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol.Core.Types;

namespace finaProject.ViewComponents
{
    public class TeamViewComponent:ViewComponent
    {
        private readonly IDoctorService _service;

        public TeamViewComponent(IDoctorService service)
        {
            _service = service;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            List<DoctorGetDto> getDtos = await _service.GetAllAsync(d=>!d.IsDeleted);
            if (getDtos == null)
            {
                return View();
            }
            return View(getDtos);
        }
    }
}
