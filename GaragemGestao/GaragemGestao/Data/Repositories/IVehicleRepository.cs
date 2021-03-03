using GaragemGestao.Data.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GaragemGestao.Data.Repositories
{
    public interface IVehicleRepository:IGenericRepository<Vehicle>
    {
        IEnumerable<SelectListItem> GetComboVehicles();
    }
}
