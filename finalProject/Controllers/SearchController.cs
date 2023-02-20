using AutoMapper;
using Business.Services.Intefaces;
using Microsoft.AspNetCore.Mvc;

namespace finaProject.Controllers
{
    public class SearchController : Controller
    {

        private readonly IDoctorService _service;
        public SearchController(IDoctorService service)
        {
            _service = service;
        }

        public async Task<IActionResult> Index(DoctorSearchDto? searchDto)
        {
            if(searchDto.getDtos == null)
            {
                List<DoctorGetDto> getDtos = await _service.GetAllAsync(d => !d.IsDeleted);
                getDtos = getDtos.Take(4).ToList();
                searchDto.getDtos=getDtos;
                searchDto.count = getDtos.Count;
                return View(searchDto);
            }
            return View(searchDto);
        }
        [HttpPost]
        public async Task<IActionResult> Result(DoctorSearchDto searchDto)
        {
            List<DoctorGetDto> getDtos = await _service.GetAllAsync(d => d.Name == searchDto.postDto.Name, "Icons");
            if (getDtos == null)
            {
                return RedirectToAction(nameof(Index));
            }
            searchDto.getDtos= getDtos;
            return View("Index", searchDto);
        }
        public async Task<IActionResult> LoadMore(int skip)
        {
            List<DoctorGetDto> getDtos1 = await _service.GetAllAsync(d => !d.IsDeleted);
            getDtos1 = getDtos1.Skip(skip).Take(3).ToList();
            return PartialView("_TeamPartialView", getDtos1);

        }
    }
}
