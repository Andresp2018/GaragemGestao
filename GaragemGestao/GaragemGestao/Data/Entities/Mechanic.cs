﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GaragemGestao.Data.Entities
{
    public class Mechanic :IEntity
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "Name")]
        [Required]
        public string Name { get; set; }
        [Display(Name = "Starting Date")]
        [Required]
        public DateTime StartDate { get; set; }
        [Display(Name = "End Date")]
        [Required]
        public DateTime EndDate { get; set; }
        [Required]
        public bool IsActive { get; set; }

        [Required]
        public Repair Repair { get; set; }

        //public virtual ICollection<User> Users { get; set; }
        public User User { get; set; }

    }
}
