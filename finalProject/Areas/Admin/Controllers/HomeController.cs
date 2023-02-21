
using Business.Services.Intefaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

[Area("admin")]
[Authorize(Roles = "admin")]


public class HomeController : Controller
{
    private readonly IHomeService _service;

    public HomeController(IHomeService service)
    {
        _service = service;
    }

    public async Task<IActionResult> Index()
    {
        HomeGetDto home = await _service.Get();
        if (home == null)
        {
            return View();
        }
        return View(home);
    }
    public IActionResult Create()
    {
        return View();
    }
    [HttpPost]
    public async Task<IActionResult> Create(HomePostDto postDto)
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
        await _service.CreateAsync(postDto);
        return RedirectToAction(nameof(Index));
    }
    public async Task<IActionResult> Update()
    {
        HomeGetDto getDto = await _service.Get();
        if (getDto == null) return View();
        HomeUpdateDto updateDto = new HomeUpdateDto { getDto = getDto };
        return View(updateDto);
    }
    [HttpPost]
    public async Task<IActionResult> Update(HomeUpdateDto updateDto)
    {
        updateDto.getDto = await _service.Get();
        if (!ModelState.IsValid)
        {
            return View(updateDto);
        }
        var result = await _service.UpdateAsync(updateDto);
        if (!result) return View(updateDto);
        return RedirectToAction(nameof(Index));
    }
    public async Task<IActionResult> Delete(int id)
    {
       var result= await _service.DeleteAsync(id);
        if (!result) return NotFound();
        return RedirectToAction(nameof(Index));

    }
}
