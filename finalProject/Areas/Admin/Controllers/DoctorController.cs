
using Business.Services.Intefaces;
using Entities.Dtos.ResHistory;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

[Area("admin")]
[Authorize(Roles = "admin")]


public class DoctorController : Controller
{
    private readonly IDoctorService _service;
    private readonly IPositionService _position;
    private readonly IResHistoryService _history;

    public DoctorController(IDoctorService service, IPositionService position, IResHistoryService history)
    {
        _service = service;
        _position = position;
        _history = history;
    }

    public async Task<IActionResult> Index(int currentpage = 1, int take = 5)
    {
        List<DoctorGetDto> getDtos = await _service.GetAllAsync(d=>!d.IsDeleted);
        getDtos = getDtos
                .OrderByDescending(d => d.Id)
                .Skip((currentpage - 1) * take)
                .Take(take)
                .ToList();
        int pageCount = (int)Math.Ceiling((decimal)getDtos.Count / take);
        if (pageCount == 0) pageCount = 1;
        PaginationDto<DoctorGetDto> pagination = new PaginationDto<DoctorGetDto>
        {
            Models = getDtos,
            CurrentPage = currentpage,
            PageCount = pageCount,
            Next = currentpage < pageCount,
            Previous = currentpage > 1
        };

        return View(pagination);
    }
    public async Task<IActionResult> Create()
    {
        ViewBag.Positions = await _position.GetAllAsync();
        return View();
    }
    [HttpPost]
    public async Task<IActionResult> Create(DoctorPostDto postDto)
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
    public async Task<IActionResult> Update(int id)
    {
        DoctorGetDto getDto = await _service.GetbyId(id);
        if (getDto.IsDeleted == true) { return NotFound(); }
        DoctorUpdateDto updateDto = new DoctorUpdateDto { getDto = getDto };
        ViewBag.Positions = await _position.GetAllAsync();
        return View(updateDto);
    }
    [HttpPost]
    public async Task<IActionResult> Update(DoctorUpdateDto updateDto)
    {
        await _service.UpdateAsync(updateDto);
        return RedirectToAction(nameof(Index));
    }
    public async Task<IActionResult> Delete(int id)
    {
        await _service.DeleteAsync(id);
        return RedirectToAction(nameof(Index));
    }
    public async Task<IActionResult> Detail(int id)
    {
        DoctorGetDto doctor = await _service.GetbyId(id);
        if (doctor == null) { return NotFound(); };
        return View(doctor);
    }
    public async Task<IActionResult> GetAll(int currentpage = 1, int take = 5)
    {
        List<DoctorGetDto> getDtos = await _service.GetAllAsync(d => d.IsDeleted || !d.IsDeleted|| (d.IsDeleted && !d.IsDeleted));
        if (getDtos == null) return View();
        getDtos = getDtos
                .OrderByDescending(d => d.Id)
                .Skip((currentpage - 1) * take)
                .Take(take)
                .ToList();
        int pageCount = (int)Math.Ceiling((decimal)getDtos.Count / take);
        if (pageCount == 0) pageCount = 1;
        PaginationDto<DoctorGetDto> pagination = new PaginationDto<DoctorGetDto>
        {
            Models = getDtos,
            CurrentPage = currentpage,
            PageCount = pageCount,
            Next = currentpage < pageCount,
            Previous = currentpage > 1
        };

        return View(pagination);
    }
    public async Task<IActionResult> History(int currentpage = 1, int take = 5)
    {
        List<ResGetDto> getDtos = await _history.GetAllAsync();
        getDtos = getDtos
               .OrderByDescending(d => d.Id)
               .Skip((currentpage - 1) * take)
               .Take(take)
               .ToList();
        int pageCount = (int)Math.Ceiling((decimal)getDtos.Count / take);
        if (pageCount == 0) pageCount = 1;
        PaginationDto<ResGetDto> pagination = new PaginationDto<ResGetDto>
        {
            Models = getDtos,
            CurrentPage = currentpage,
            PageCount = pageCount,
            Next = currentpage < pageCount,
            Previous = currentpage > 1
        };

        return View(pagination);
    }

}

