using AutoMapper;
using Business.Services.Intefaces;
using Microsoft.AspNetCore.Mvc;

namespace finaProject.ViewComponents
{
    public class ServiceViewComponent:ViewComponent
    {

        private readonly IServiceService _service;

        public ServiceViewComponent(IServiceService service)
        {
            _service = service;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {

            List<ServiceGetDto> getDtos=await _service.GetAllAsync();
            if (getDtos == null)
            {
                return View();
            }
            return View(getDtos);
        }
    }
}
