using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dtos.ResHistory
{
    public class ShowDto
    {
        public List<ResGetDto> ResGet { get; set; }
        public List<DoctorGetDto> DoctorGet { get; set; }
    }
}
