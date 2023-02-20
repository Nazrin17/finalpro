using AutoMapper;
using Business.Services.Intefaces;
using Microsoft.AspNetCore.Mvc;

namespace finaProject.ViewComponents
{
    public class AboutViewComponent:ViewComponent
    {
        private readonly IAboutService _service;

        public AboutViewComponent(IAboutService service)
        {
            _service = service;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        { 
           AboutGetDto getDto = await _service.Get();
            if(getDto == null) { return View(); }
            return View(getDto);
        }
    }
}
