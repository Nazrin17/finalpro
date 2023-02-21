using AutoMapper;
using Business.Services.Intefaces;
using Core.Utilities;
using Core.Utilities.Extentions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.Implementations
{
    public class HomeManager : IHomeService
    {
        private readonly IHomeRepository _repository;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _env;
        private readonly IActionContextAccessor _actionContextAccessor;


        public HomeManager(IHomeRepository repository, IMapper mapper, IWebHostEnvironment env, IActionContextAccessor actionContextAccessor)
        {
            _repository = repository;
            _mapper = mapper;
            _env = env;
            _actionContextAccessor = actionContextAccessor;
        }

        public async Task CreateAsync(HomePostDto postDto)
        {
            Home home = _mapper.Map<Home>(postDto);
            home.Image = postDto.formFile.FileCreate(_env.WebRootPath, "assets/img");
            await _repository.CreateAsync(home);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            Home home = await _repository.Get(h => h.Id == id);
            if (home is null) return false;
            _repository.Delete(home);
            return true;
        }

        public async Task<HomeGetDto> Get()
        {
            Home home = await _repository.Get();

            HomeGetDto getDto = _mapper.Map<HomeGetDto>(home);
            return getDto;
        }

        public  async Task<bool> UpdateAsync(HomeUpdateDto updateDto)
        {
            Home home = await _repository.Get(e => e.Id == updateDto.getDto.Id);
            updateDto.getDto = _mapper.Map<HomeGetDto>(home);
            if (updateDto.postDto.formFile != null)
            {
                if (!updateDto.postDto.formFile.ContentType.Contains("image"))
                {
                   _actionContextAccessor.ActionContext.ModelState.AddModelError("", "Please send image");
                    return false;
                }
                string imagename =updateDto.postDto.formFile.FileCreate(_env.WebRootPath, "assets/img");
                Helper.FileDelete(_env.WebRootPath, "assets/img", home.Image);
                home.Image = imagename;
            }
            home.Title = updateDto.postDto.Title;
            home.Slogan = updateDto.postDto.Slogan;
            _repository.Update(home);
            return true;
        }
    }
}
