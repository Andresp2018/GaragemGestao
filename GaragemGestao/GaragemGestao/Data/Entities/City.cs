using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GaragemGestao.Data.Entities
{
    public class City:IEntity
    {
        public int Id { get; set; }



        [MaxLength(50, ErrorMessage = "The field {0} only can contain {1} characters.")]
        [Required]
        [Display(Name = "City")]
        public string Name { get; set; }
    }
}
