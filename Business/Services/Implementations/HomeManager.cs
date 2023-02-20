using AutoMapper;
using Business.Services.Intefaces;
using Core.Utilities;
using Core.Utilities.Extentions;
using Microsoft.AspNetCore.Hosting;
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

        public HomeManager(IHomeRepository repository, IMapper mapper, IWebHostEnvironment env)
        {
            _repository = repository;
            _mapper = mapper;
            _env = env;
        }

        public async Task CreateAsync(HomePostDto postDto)
        {
            Home home = _mapper.Map<Home>(postDto);
            home.Image = postDto.formFile.FileCreate(_env.WebRootPath, "assets/img");
            await _repository.CreateAsync(home);
        }

        public async Task DeleteAsync(int id)
        {
            Home home = await _repository.Get(h => h.Id == id);
            _repository.Delete(home);
        }

        public async Task<HomeGetDto> Get()
        {
            Home home = await _repository.Get();

            HomeGetDto getDto = _mapper.Map<HomeGetDto>(home);
            return getDto;
        }

        public  async Task UpdateAsync(HomeUpdateDto updateDto)
        {
            Home home = await _repository.Get(e => e.Id == updateDto.getDto.Id);
            updateDto.getDto = _mapper.Map<HomeGetDto>(home);
            if (updateDto.postDto.formFile != null)
            {
                //if (!updateDto.postDto.formFile.ContentType.Contains("image"))
                //{
                //    ModelState.AddModelError("Formfile", "Please send image");
                //    return View(updateDto);
                //}
                string imagename =updateDto.postDto.formFile.FileCreate(_env.WebRootPath, "assets/img");
                Helper.FileDelete(_env.WebRootPath, "assets/img", home.Image);
                home.Image = imagename;
            }
            home.Title = updateDto.postDto.Title;
            home.Slogan = updateDto.postDto.Slogan;
            _repository.Update(home);
        }
    }
}
