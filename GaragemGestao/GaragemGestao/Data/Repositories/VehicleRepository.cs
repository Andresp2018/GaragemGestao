using GaragemGestao.Data.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GaragemGestao.Data.Repositories
{
    public class VehicleRepository:GenericRepository<Vehicle>, IVehicleRepository
    {
        private readonly DataContext _context;

        public VehicleRepository(DataContext context) : base(context)
        {
            _context = context;
        }


   //This method will get all the vheicles in the table
        public IEnumerable<SelectListItem> GetComboVehicles()
        {
            var list = _context.Vehicles.Select(p => new SelectListItem
            {
                Text = p.ModelName,
                Value = p.Id.ToString()
            }).ToList();

            list.Insert(0, new SelectListItem
            {
                Text = "(Select a vehicle...)",
                Value = "0"
            });


            return list;
        }
    }
}
