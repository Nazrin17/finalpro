
using Business.Services.Intefaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

[Area("admin")]
[Authorize(Roles = "admin")]

public class MessageController : Controller
{
    private readonly IMessageService _service;

    public MessageController(IMessageService service)
    {
        _service = service;
    }

    public async Task<IActionResult> Index(int currentpage = 1, int take = 6)
    {
        List<MessageGetDto> messages = await _service.GetAllAsync(m => !m.IsDeleted);
        if (messages == null) { return View(); }
        messages = messages
                 .OrderByDescending(d => d.Id)
                .Skip((currentpage - 1) * take)
                .Take(take)
                .ToList();
        int pageCount = (int)Math.Ceiling((decimal)messages.Count / take);
        if (pageCount == 0) pageCount = 1;
        PaginationDto<MessageGetDto> pagination = new PaginationDto<MessageGetDto>
        {
            Models = messages,
            CurrentPage = currentpage,
            PageCount = pageCount,
            Next = currentpage < pageCount,
            Previous = currentpage > 1
        };
        return View(pagination);
    }
    public async Task<IActionResult> Delete(int id)
    {
      var result=  await _service.DeleteAsync(id);
        if (!result) return NotFound();
        return RedirectToAction(nameof(Index));
    }
    public async Task<IActionResult> Deleted()
    {
        List<MessageGetDto> getDtos = await _service.GetAllAsync(m => m.IsDeleted);
        if (getDtos == null) return View();
        return View(getDtos);
    }
    public async Task<IActionResult> Restore(int id)
    {
      var result=  await _service.RestoreAsync(id);
        if (!result) return NotFound();
        return RedirectToAction(nameof(Deleted));
    }

}

