using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.Intefaces
{
    public interface IServiceService
    {
        Task<List<ServiceGetDto>> GetAllAsync();
        Task<ServiceGetDto> GetbyId(int id);
        Task CreateAsync(ServicePostDto postDto);
        Task<bool> UpdateAsync(ServiceUpdateDto updateDto);
        Task<bool> DeleteAsync(int id);
    }
}
