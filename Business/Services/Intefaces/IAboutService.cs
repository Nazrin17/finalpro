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
        Task<bool> CreateAsync(AboutPostDto postDto);
        Task<bool> UpdateAsync(AboutUpdateDto updateDto);
        Task<bool> DeleteAsync(int id);
    }
}
