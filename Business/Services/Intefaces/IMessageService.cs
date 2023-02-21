using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.Intefaces
{
    public interface IMessageService
    {
        Task<List<MessageGetDto>> GetAllAsync(Expression<Func<Message,bool>> exp);
        Task<List<MessageGetDto>> GetAllAsync();
        Task<Message> GetbyId(int id);
        Task<bool> DeleteAsync(int id);
        Task<bool> RestoreAsync(int id);
        Task CreateAsync(MessagePostDto postDto);
    }
}
