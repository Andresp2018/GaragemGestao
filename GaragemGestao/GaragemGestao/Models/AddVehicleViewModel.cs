using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GaragemGestao.Models
{
    public class AddVehicleViewModel
    {

        [Display(Name = "Vehicle")]
        [Range(1, int.MaxValue, ErrorMessage = "You must select a vehicle.")]
        public int VehicleId { get; set; }


        [Range(0.0001, double.MaxValue, ErrorMessage = "The quantity of vehicle added must be a positive number.")]
        public double Quantity { get; set; }



        public IEnumerable<SelectListItem> Vehicles { get; set; }
    }


}

