using AutoMapper;
using Business.Services.Intefaces;
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

        public PositionManager(IPositionRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task CreateAsync(PositionPostDto postDto)
        {
            Position position = _mapper.Map<Position>(postDto);
            await _repository.CreateAsync(position);
        }

        public async Task DeleteAsync(int id)
        {
            Position position=await _repository.Get(p=>p.Id==id);
             _repository.Delete(position);
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

        public  async Task UpdateAsync(PositionUpdateDto updateDto)
        {
            Position position = await _repository.Get(e => e.Id == updateDto.getDto.Id);
            updateDto.getDto = _mapper.Map<PositionGetDto>(position);
            position.Name = updateDto.postDto.Name;
            //if (!ModelState.IsValid)
            //{
            //    return View(updateDto);
            //}
            _repository.Update(position);
        }
    }
}
