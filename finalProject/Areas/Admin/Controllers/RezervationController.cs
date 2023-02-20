
using Business.Services.Intefaces;
using Core.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[Area("admin")]
[Authorize(Roles = "admin")]

public class RezervationController : Controller
{
    private readonly IDoctorService _doctorService;
    private readonly IReservationService _service;

    public RezervationController(IDoctorService doctorService, IReservationService service)
    {
        _doctorService = doctorService;
        _service = service;
    }

    public async Task<IActionResult> Index(int currentpage = 1, int take = 7)
    {
        List<DoctorGetDto> getDtos = await _doctorService.GetAllAsync(d=>!d.IsDeleted);
        if (getDtos == null)
        {
            return View();
        }
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
    public async Task<IActionResult> Rezerv(int id, string time, string user)
    {
        await _service.Reserv(id, time, user);
        return RedirectToAction(nameof(Index));
    }
    public async Task<IActionResult> Okei()
    {
        await _service.Okei();
        return RedirectToAction(nameof(Index));
    }
    public async Task<IActionResult> DeleteAll()
    {
        List<DoctorGetDto> docs = await _doctorService.GetAllAsync(d=>!d.IsDeleted);
        foreach (DoctorGetDto doc in docs)
        {
            foreach (var item in doc.rezervs)
            {
                if (item.Busy)
                {
                    item.Busy = false;
                }
            }
            DoctorUpdateDto dto = new DoctorUpdateDto { getDto = doc };
            await _doctorService.UpdateAsync(dto);
        }
        
        return RedirectToAction(nameof(Index));
    }
}

