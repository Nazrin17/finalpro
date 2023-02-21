using AutoMapper;
using Business.Services.Intefaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.Implementations
{
    public class MessageManager : IMessageService
    {
        private readonly IMessageRepository _repository;
        private readonly IMapper _mapper;

        public MessageManager(IMessageRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task CreateAsync(MessagePostDto postDto)
        {
            Message message=_mapper.Map<Message>(postDto);
            await _repository.CreateAsync(message);
        }

        public  async Task<bool> DeleteAsync(int id)
        {
            Message message = await _repository.Get(m => m.Id == id);
            if (message == null) return false;
            message.IsDeleted = true;
            _repository.Update(message);
            return true;
        }

        public async Task<List<MessageGetDto>> GetAllAsync(Expression<Func<Message, bool>> exp)
        {
            List<Message> messages = await _repository.GetAllAsnyc(exp);
            List<MessageGetDto> getDtos = _mapper.Map<List<MessageGetDto>>(messages);
            return getDtos;

        }

        public async Task<List<MessageGetDto>> GetAllAsync()
        {
            List<Message> messages = await _repository.GetAllAsnyc();
            List<MessageGetDto> getDtos = _mapper.Map<List<MessageGetDto>>(messages);
            return getDtos;
        }

        public async Task<Message> GetbyId(int id)
        {
            Message message = await _repository.Get(m => m.Id == id);
            return message;
        }

        public async Task<bool> RestoreAsync(int id)
        {
            Message message = await _repository.Get(m => m.Id == id);
            if (message == null) return false;
            message.IsDeleted = false;
            _repository.Update(message);
            return true;
        }
    }
}
