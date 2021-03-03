using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GaragemGestao.Models
{
    public class AddVehicleViewModel
    {

        [Display(Name = "Vehicle")]
        [Range(1, int.MaxValue, ErrorMessage = "You must select a Vehicle.")]
        public int VehicleId { get; set; }



        public IEnumerable<SelectListItem> Vehicles { get; set; }


    }
}
