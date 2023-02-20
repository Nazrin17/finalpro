using AutoMapper;
using Business.Services.Intefaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;

namespace HospitalManagement.ViewComponents
{
    public class TestimonialViewComponent:ViewComponent
    {
        private readonly IMessageService _service;

        public TestimonialViewComponent(IMessageService service)
        {
            _service = service;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            List<MessageGetDto> getDtos = await _service.GetAllAsync(m => !m.IsDeleted);    
            //if (getDtos.Count == 0) { return View(); }
            return View(getDtos);
        }
    }
}
