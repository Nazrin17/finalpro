using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dtos.ResHistory
{
    public class ResGetDto
    {
        public int Id { get; set; }
        public string UserEmail { get; set; }
        public Doctor Doctor { get; set; }
        public int DoctorId { get; set; }
        public DateTime date { get; set; }
    }
}
