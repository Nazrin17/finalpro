

using AutoMapper;
using Business.Services.Intefaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

[Area("admin")]
[Authorize(Roles = "admin")]

public class PositionController : Controller
{
    private readonly IPositionService _service;
    private readonly IMapper _mapper;

    public PositionController(IPositionService service, IMapper mapper)
    {
        _service = service;
        _mapper = mapper;
    }

    public async Task<IActionResult> Index(int currentpage = 1, int take = 5)
    {
        List<PositionGetDto> positions = await _service.GetAllAsync();
        if (positions == null) return View();
        List<PositionGetDto> getDtos = positions
                .OrderByDescending(d => d.Id)
                .Skip((currentpage - 1) * take)
                .Take(take)
                .ToList();

        int pageCount = (int)Math.Ceiling((decimal)positions.Count / take);
        if (pageCount == 0) pageCount = 1;
        PaginationDto<PositionGetDto> pagination = new PaginationDto<PositionGetDto>
        {
            Models = getDtos,
            CurrentPage = currentpage,
            PageCount = pageCount,
            Next = currentpage < pageCount,
            Previous = currentpage > 1
        };

        return View(pagination);
    }

    public IActionResult Create()
    {
        return View();
    }
    [HttpPost]
    public async Task<IActionResult> Create(PositionPostDto postDto)
    {
        if (!ModelState.IsValid) return View(postDto);
        await _service.CreateAsync(postDto);
        return RedirectToAction(nameof(Index));
    }
    public async Task<IActionResult> Update(int id)
    {
        PositionGetDto getDto = await _service.GetbyId(id);
        if (getDto == null) return NotFound();
        PositionUpdateDto updateDto = new PositionUpdateDto { getDto = getDto };
        return View(updateDto);
    }
    [HttpPost]
    public async Task<IActionResult> Update(PositionUpdateDto updateDto)
    {
      var result=  await _service.UpdateAsync(updateDto);
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
