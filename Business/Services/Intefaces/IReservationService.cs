using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.Intefaces
{
    public interface IReservationService
    {
        Task Reserv(int id, string time, string user);
        Task Okei();
    }
}
