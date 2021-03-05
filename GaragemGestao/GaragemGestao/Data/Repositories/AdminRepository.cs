using GaragemGestao.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GaragemGestao.Data.Repositories
{
    public class AdminRepository : GenericRepository<Repair>, IAdminRepository
    {
        public AdminRepository(DataContext context) : base(context)
        {

        }
    }
}
