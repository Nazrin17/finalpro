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
        Task DeleteAsync(int id);
        Task RestoreAsync(int id);
        Task CreateAsync(MessagePostDto postDto);
    }
}
