using GaragemGestao.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GaragemGestao.Data.Repositories
{
    public class RepairRepository : GenericRepository<Repair>, IRepairRepository
    {
        public RepairRepository(DataContext context) : base(context)
        {


        }
    }
}