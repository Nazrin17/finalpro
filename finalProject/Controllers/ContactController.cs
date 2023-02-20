using AutoMapper;
using Business.Services.Intefaces;
using Microsoft.AspNetCore.Mvc;

namespace finaProject.Controllers
{
    public class ContactController : Controller
    {
        private readonly ISettingService _service;

        public ContactController(ISettingService service)
        {
            _service = service;
        }
        public async Task< IActionResult> Index(MessagePostDto? postDto)
        {
            if (postDto.Letter != null)
            {
              ModelState.AddModelError("", "Please first login or register");

            }
            SettingGetDto getDto = await _service.Get();
            ContactGetDto contact = new ContactGetDto { Setting = getDto };
            return View(contact);
        }
    }
}
