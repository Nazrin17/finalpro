using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.Intefaces
{
    public interface IReservationService
    {
        Task<bool> Reserv(int id, string time, string user);
        Task<bool> Okei();
    }
}
