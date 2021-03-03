using GaragemGestao.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GaragemGestao.Data.Repositories
{
    public class VehicleRepository:GenericRepository<Vehicle>, IVehicleRepository
    {
        public VehicleRepository(DataContext context) : base(context)
        {
                
        }
    }
}
