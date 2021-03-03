using GaragemGestao.Data.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GaragemGestao.Models
{
    public class VehicleViewModel: Vehicle
    {
        [Display(Name = "Image")]
        public IFormFile ImageFile { get; set; }
    }
}
