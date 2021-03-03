using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GaragemGestao.Data.Entities
{
    public class Vehicle : IEntity
    {

        public int Id { get; set; }
        [Display(Name = "License Plate")]
        [MaxLength(10)]
        [Required]
        public string LicensePlate { get; set; }
        [Display(Name = "Maker")]
        [MaxLength(50)]
        [Required]
        public string MakerName { get; set; }
        [Display(Name = "Model")]
        [MaxLength(50)]
        [Required]
        public string ModelName { get; set; }
        [MaxLength(400)]
        [Required]
        public string Details { get; set; }

        [Display(Name = "Image")]
        public string ImageUrl { get; set; }
        [Display(Name = "Type Of Vehicle")]
        [MaxLength(50)]
        [Required]
        public string typeName { get; set; }


        [DisplayFormat(DataFormatString = "{0:C2}", ApplyFormatInEditMode = false)]
        [Display(Name = "Repair Price")]
        public decimal RepairPrice { get; set; }
        public User User { get; set; }

    }
}
