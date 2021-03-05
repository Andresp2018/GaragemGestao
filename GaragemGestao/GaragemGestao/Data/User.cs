using GaragemGestao.Data.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GaragemGestao.Data
{

    public class User : IdentityUser
    {
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        public string LastName { get; set; }


        [MaxLength(100, ErrorMessage = "The field {0} only can contain {1} characters.")]
        public string Address { get; set; }



        public int CityId { get; set; }


        public City City { get; set; }



        [Display(Name = "Full name")]
        public string FullName { get { return $"{this.FirstName} {this.LastName}"; } }


        public User()
        {
            Messages = new HashSet<Message>();
        }
        //1-* User||Messages
        public virtual ICollection<Message> Messages { get; set; }


    }
}

