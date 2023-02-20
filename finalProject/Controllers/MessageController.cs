using AutoMapper;
using Business.Services.Intefaces;
using Core.Entities.Concrete;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;

namespace finaProject.Controllers
{
    public class MessageController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IMessageService _service;

        public MessageController( UserManager<AppUser> userManager, IMessageService service)
        {

            _userManager = userManager;
            _service = service;
        }
        [HttpPost]
        public async Task<IActionResult> Send(MessagePostDto postDto)
        {
            if (!ModelState.IsValid)
            {
                return Json("Make sure you fill in the fields correctly");
            }
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Contact",postDto);
            }
            AppUser user=await _userManager.FindByEmailAsync(postDto.Email);
            if(user == null)
            {
                return Json("Make sure you fill in the fields correctly");
            }
            postDto.UserName = user.UserName;
            await _service.CreateAsync(postDto);
            return Json(new { status = 200 });
        }
    }
}
