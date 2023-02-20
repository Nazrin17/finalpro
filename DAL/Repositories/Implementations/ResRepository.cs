using DAL.Repositories.Interfaces;
using Entities.Concrete.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories.Implementations
{
    public class ResRepository:BaseRepository<ResHistory, Hospital2DbContext>,IResRepository
    {
        private readonly Hospital2DbContext _context;

        public ResRepository(Hospital2DbContext context):base(context)
        {
            _context= context;
        }

    }
}
