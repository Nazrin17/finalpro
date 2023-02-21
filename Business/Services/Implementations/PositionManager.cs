using AutoMapper;
using Business.Services.Intefaces;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.Implementations
{
    public class PositionManager : IPositionService
    {
        private readonly IPositionRepository _repository;
        private readonly IMapper _mapper;
        private readonly IActionContextAccessor _actionContextAccessor;

        public PositionManager(IPositionRepository repository, IMapper mapper, IActionContextAccessor actionContextAccessor)
        {
            _repository = repository;
            _mapper = mapper;
            _actionContextAccessor = actionContextAccessor;
        }

        public async Task CreateAsync(PositionPostDto postDto)
        {
            Position position = _mapper.Map<Position>(postDto);
            await _repository.CreateAsync(position);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            Position position=await _repository.Get(p=>p.Id==id);
            if(position is null) return false;
             _repository.Delete(position);
            return true;
        }

        public async Task<List<PositionGetDto>> GetAllAsync()
        {
            List<Position> position = await _repository.GetAllAsnyc();
           List< PositionGetDto> getDto = _mapper.Map<List<PositionGetDto>>(position);
            return getDto;
        }

        public async Task<PositionGetDto> GetbyId(int id)
        {
           Position position=await _repository.Get(p=>p.Id==id);
            PositionGetDto getDto=_mapper.Map<PositionGetDto>(position);
            return getDto;
        }

        public  async Task<bool> UpdateAsync(PositionUpdateDto updateDto)
        {
            Position position = await _repository.Get(e => e.Id == updateDto.getDto.Id);
            if (position is null) return false;
            updateDto.getDto = _mapper.Map<PositionGetDto>(position);
            position.Name = updateDto.postDto.Name;
            if (!_actionContextAccessor.ActionContext.ModelState.IsValid)
            {
                return false;
            }
            _repository.Update(position);
            return true;
        }
    }
}
