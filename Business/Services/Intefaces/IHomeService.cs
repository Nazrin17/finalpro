using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.Intefaces
{
    public interface IHomeService
    {
        Task<HomeGetDto> Get();
        Task CreateAsync(HomePostDto postDto);
        Task UpdateAsync(HomeUpdateDto updateDto);
        Task DeleteAsync(int id);
    }
}
