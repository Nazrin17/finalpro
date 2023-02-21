

using AutoMapper;
using Business.Services.Intefaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

[Area("admin")]
[Authorize(Roles = "admin")]

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
        if (getDtos is null) return View();
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
        if (getDto is null) return NotFound();
        ServiceUpdateDto updateDto = new ServiceUpdateDto { getDto = getDto };
        return View(updateDto);
    }
    [HttpPost]
    public async Task<IActionResult> Update(ServiceUpdateDto updateDto)
    {
        updateDto.getDto = await _service.GetbyId(updateDto.getDto.Id);
        if (!ModelState.IsValid)
        {
            return View(updateDto);
        }

       var result= await _service.UpdateAsync(updateDto);
        if (!result) return NotFound();
        return RedirectToAction(nameof(Index));
    }
    public async Task<IActionResult> Delete(int id)
    {
       var result=  await _service.DeleteAsync(id);
        if(!result) return NotFound();  
        return RedirectToAction(nameof(Index));
    }
}

