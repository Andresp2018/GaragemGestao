using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GaragemGestao.Data.Entities
{
    public class Repair : IEntity
    {

        public int Id { get; set; }


        [Required]
        [Display(Name = "Repair date")]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd hh:mm tt}", ApplyFormatInEditMode = false)]
        public DateTime RepairDate { get; set; }



        [Display(Name = "Delivery date")]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd hh:mm tt}", ApplyFormatInEditMode = false)]
        public DateTime? DeliveryDate { get; set; }


        public IEnumerable<RepairDetail> Items { get; set; }


        [DisplayFormat(DataFormatString = "{0:N0}")]
        public int Lines { get { return this.Items == null ? 0 : this.Items.Count(); } }


        [DisplayFormat(DataFormatString = "{0:N2}")]
        public double Quantity { get { return this.Items == null ? 0 : this.Items.Sum(i => i.Quantity); } }



        [DisplayFormat(DataFormatString = "{0:C2}")]
        public decimal Value { get { return this.Items == null ? 0 : this.Items.Sum(i => i.Value); } }

        public string Issue { get; set; }


        [Display(Name = "Repair date")]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd HH:mm tt}", ApplyFormatInEditMode = false)]
        public DateTime? RepairDateLocal
        {
            get
            {
                if (this.RepairDate == null)
                {
                    return null;
                }

                return this.RepairDate.ToLocalTime();
            }
        }

        [Required]
        public User User { get; set; }


    }
}
