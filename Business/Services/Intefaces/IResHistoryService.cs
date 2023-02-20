using Entities.Concrete.Models;
using Entities.Dtos.ResHistory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.Intefaces
{
    public interface IResHistoryService
    {
        Task CreateAsync(ResPostDto postDto);
        Task<List<ResGetDto>> GetAllAsync(Expression<Func<ResHistory, bool>> exp);
        Task<List<ResGetDto>> GetAllAsync();
    }
}
