

using AutoMapper;
using Business.Services.Intefaces;
using Microsoft.AspNetCore.Mvc;

[Area("admin")]

public class ServiceController : Controller
{
    private readonly IServiceService _service;

    public ServiceController(IServiceService service)
    {
        _service = service;
    }

    public async Task<IActionResult> Index()
    {
        List<ServiceGetDto> getDtos = await _service.GetAllAsync();
        return View(getDtos);
    }
    public IActionResult Create()
    {
        return View();
    }
    [HttpPost]
    public async Task<IActionResult> Create(ServicePostDto postDto)
    {
        if (!ModelState.IsValid)
        {
            return View();
        }
        await _service.CreateAsync(postDto);  
        return RedirectToAction(nameof(Index));
    }
    public async Task<IActionResult> Update(int id)
    {

        ServiceGetDto getDto =  await _service.GetbyId(id);
        ServiceUpdateDto updateDto = new ServiceUpdateDto { getDto = getDto };
        return View(updateDto);
    }
    [HttpPost]
    public async Task<IActionResult> Update(ServiceUpdateDto updateDto)
    {
        if (!ModelState.IsValid)
        {
            return View(updateDto);
        }
        await _service.UpdateAsync(updateDto);
        return RedirectToAction(nameof(Index));
    }
    public async Task<IActionResult> Delete(int id)
    {
         await _service.DeleteAsync(id);
        return RedirectToAction(nameof(Index));
    }
}

