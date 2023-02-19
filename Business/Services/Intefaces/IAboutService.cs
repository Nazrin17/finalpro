using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.Intefaces
{
    public interface IAboutService
    {
        Task<AboutGetDto> Get();
        Task CreateAsync(AboutPostDto postDto);
        Task UpdateAsync(AboutUpdateDto updateDto);
        Task DeleteAsync(int id);
    }
}
