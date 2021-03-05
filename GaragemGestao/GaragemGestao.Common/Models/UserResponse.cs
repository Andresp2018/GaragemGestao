using System;
using System.Collections.Generic;
using System.Text;

namespace GaragemGestao.Common.Models
{
    public class UserResponse
    {
        public string Id { get; set; }


        public string FirstName { get; set; }


        public string LastName { get; set; }


        public string UserName { get; set; }


        public string NormalizedUserName { get; set; }


        public string Email { get; set; }


        public string NormalizedEmail { get; set; }


        public bool EmailConfirmed { get; set; }


        public string PasswordHash { get; set; }


        public string SecurityStamp { get; set; }


        public string ConcurrencyStamp { get; set; }


        public string PhoneNumber { get; set; }


        public bool PhoneNumberConfirmed { get; set; }


        public bool TwoFactorEnabled { get; set; }


        public object LockoutEnd { get; set; }


        public bool lockoutEnabled { get; set; }


        public int accessFailedCount { get; set; }
    }
}
