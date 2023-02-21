
using AutoMapper;
using Business.Services.Intefaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[Area("admin")]
[Authorize(Roles = "admin")]
public class AboutController : Controller
{
    private readonly IAboutService _service;
    private readonly IMapper _mapper;
    private readonly IWebHostEnvironment _env;

    public AboutController(IAboutService service, IMapper mapper, IWebHostEnvironment env)
    {
        _service = service;
        _mapper = mapper;
        _env = env;
    }

    public async Task<IActionResult> Index()
    {
        AboutGetDto getDto = await _service.Get();
        if (getDto == null)
        {
            return View();
        }
        return View(getDto);
    }
    public IActionResult Create()
    {
        return View();
    }
    [HttpPost]
    public async Task<IActionResult> Create(AboutPostDto postDto)
    {
        if (!ModelState.IsValid)
        {
            return View();
        }
        if (!postDto.formFile.ContentType.Contains("image"))
        {
            ModelState.AddModelError("Formfile", "please send image");
            return View(postDto);
        }
        return RedirectToAction(nameof(Index));
    }
    public async Task<IActionResult> Update(int id)
    {
        AboutGetDto getDto = await _service.Get();
        if (getDto == null)
        {
            return NotFound();
        }
        AboutUpdateDto updateDto = new AboutUpdateDto { getDto = getDto };
        return View(updateDto);
    }
    [HttpPost]
    public async Task<IActionResult> Update(AboutUpdateDto updateDto)
    {
        AboutGetDto getDto = await _service.Get();
        updateDto.getDto = getDto;
        if (!ModelState.IsValid)
        {
            return View(updateDto);
        }
      var result=  await _service.UpdateAsync(updateDto);
        if (!result)
        {
            ModelState.AddModelError("", "Please send image");
            return View(updateDto);
        }
        return RedirectToAction(nameof(Index));
    }
    public async Task<IActionResult> Delete(int id)
    {
       var result=  await _service.DeleteAsync(id);
        if (!result)
        {
            return NotFound();
        }
        return RedirectToAction(nameof(Index));
    }
}
