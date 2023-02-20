using AutoMapper;
using Business.Services.Intefaces;
using DAL.Repositories.Interfaces;
using Entities.Concrete.Models;
using Entities.Dtos.ResHistory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.Implementations
{
    public class ResHistoryManager : IResHistoryService
    {
        private readonly IResRepository _repository;
        private readonly IDoctorRepository _doctor;
        private readonly IMapper _mapper;

        public ResHistoryManager(IResRepository repository, IMapper mapper, IDoctorRepository doctor)
        {
            _repository = repository;
            _mapper = mapper;
            _doctor = doctor;
        }

        public async Task CreateAsync(ResPostDto postDto)
        {
            ResHistory res=_mapper.Map<ResHistory>(postDto);
          await   _repository.CreateAsync(res);
        }

        public async Task<List<ResGetDto>> GetAllAsync(Expression<Func<ResHistory, bool>> exp)
        {
            List<ResHistory> res = await _repository.GetAllAsnyc(exp,"Doctor");
            List<ResGetDto> getDtos=_mapper.Map<List<ResGetDto>>(res);
            return getDtos;

        }

        public async Task<List<ResGetDto>> GetAllAsync()
        {
            List<ResHistory> res = await _repository.GetAllAsnyc("Doctor");
            List<ResGetDto> getDtos = _mapper.Map<List<ResGetDto>>(res);
            return getDtos;
        }

    }
}
