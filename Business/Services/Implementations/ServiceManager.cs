using AutoMapper;
using Business.Services.Intefaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.Implementations
{

    public class ServiceManager : IServiceService
    {
        private readonly IServiceRepository _repository;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _env;

        public ServiceManager(IServiceRepository repository, IMapper mapper, IWebHostEnvironment env)
        {
            _repository = repository;
            _mapper = mapper;
            _env = env;
        }

        public async Task CreateAsync(ServicePostDto postDto)
        {
            Service service = _mapper.Map<Service>(postDto);
            await _repository.CreateAsync(service);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            Service service = await _repository.Get(a => a.Id == id);
            if (service is null) return false;
            _repository.Delete(service);
            return true;
        }

        public async Task<List<ServiceGetDto>> GetAllAsync()
        {
            List<Service> services = await _repository.GetAllAsnyc();
            List<ServiceGetDto> getDtos = _mapper.Map<List<ServiceGetDto>>(services);
            return getDtos;
        }

        public async Task<ServiceGetDto> GetbyId(int id)
        {
            Service service = await _repository.Get(e => e.Id == id);
            ServiceGetDto getDto = _mapper.Map<ServiceGetDto>(service);
            return getDto;
        }

        public async Task<bool> UpdateAsync(ServiceUpdateDto updateDto)
        {
            Service service = await _repository.Get(e => e.Id == updateDto.getDto.Id);
            if (service is null) return false;
            updateDto.getDto = _mapper.Map<ServiceGetDto>(service);
            service.Title = updateDto.postDto.Title;
            service.Description = updateDto.postDto.Description;
            service.Icon = updateDto.postDto.Icon;
            if (updateDto.postDto.IconUrl != null)
            {
                service.IconUrl = updateDto.postDto.IconUrl;
            }
            _repository.Update(service);
            return true;
        }
    }
}
