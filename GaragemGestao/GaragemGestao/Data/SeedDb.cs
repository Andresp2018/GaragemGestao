using GaragemGestao.Data.Entities;
using GaragemGestao.Helpers;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GaragemGestao.Data
{
    public class SeedDb
    {

        private readonly DataContext _context;
        private readonly IUserHelper _userHelper;
        private Random _random;


        public SeedDb(DataContext context, IUserHelper userHelper)
        {
            _context = context;
            _userHelper = userHelper;
            _random = new Random();
        }


        public async Task SeedAsync()
        {
            await _context.Database.EnsureCreatedAsync();

            await _userHelper.CheckRoleAsync("Admin");
            await _userHelper.CheckRoleAsync("Client");

            var user = await _userHelper.GetUserByEmailAsync("admin@gmail.com");
            if (user == null)
            {
                user = new User
                {
                    FirstName = "Andre ",
                    LastName = "Pires",
                    Email = "admin@gmail.com",
                    UserName = "admin@gmail.com",
                };

                var result = await _userHelper.AddUserAsync(user, "123456");
                if (result != IdentityResult.Success)
                {
                    throw new InvalidOperationException("Could not create the user in seeder");
                }

                await _userHelper.AddUserToRoleAsync(user, "Admin");
            }

            var isInRole = await _userHelper.IsUserInRoleAsync(user, "Admin");
            if (!isInRole)
            {
                await _userHelper.AddUserToRoleAsync(user, "Admin");
            }
            if (!_context.Vehicles.Any())
            {
                this.AddVehicle("Renault Megane", user);
                this.AddVehicle("Opel Astra", user);
                this.AddVehicle("Fiat 500", user);
                this.AddVehicle("Other", user);
                await _context.SaveChangesAsync();
            }
        }


        private void AddVehicle(string name, User user)
        {
            _context.Vehicles.Add(new Vehicle
            {
                ModelName = name,
                RepairPrice = _random.Next(1000),
                User = user
            });
        }
    }
}
