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

        public UserManager<User> UserManager { get; }

        public async Task SeedAsync()
        {
            await _context.Database.EnsureCreatedAsync();
            await _userHelper.CheckRoleAsync("Admin");
            await _userHelper.CheckRoleAsync("Client");

            if (!_context.Countries.Any())
            {
                var cities = new List<City>();
                cities.Add(new City { Name = "Lisboa" });
                cities.Add(new City { Name = "Porto" });
                cities.Add(new City { Name = "Coimbra" });
                cities.Add(new City { Name = "Faro" });


                _context.Countries.Add(new Country
                {
                    Cities = cities,
                    Name = "Portugal"
                });


                await _context.SaveChangesAsync();
            }

            var user = await _userHelper.GetUserByEmailAsync("andrep_developer@outlook.com");
            if (user == null)
            {
                user = new User
                {
                    FirstName = "Andre ",
                    LastName = "Pires",
                    Email = "andrep_developer@outlook.com",
                    UserName = "andrep_developer@outlook.com",
                    Address = "Rua XXXX",
                    CityId = _context.Countries.FirstOrDefault().Cities.FirstOrDefault().Id,
                    City = _context.Countries.FirstOrDefault().Cities.FirstOrDefault()
                };

                var result = await _userHelper.AddUserAsync(user, "123456");


                if (result != IdentityResult.Success)
                {
                    throw new InvalidOperationException("Could not create the user in seeder");
                }

                var token = await _userHelper.GenerateEmailConfirmationTokenAsync(user);
                await _userHelper.ConfirmEmailAsync(user, token);


                var isInRole = await _userHelper.IsUserInRoleAsync(user, "Admin");
                if (!isInRole)
                {
                    await _userHelper.AddUsertoRoleAsync(user, "Admin");
                }


            }

            if (!_context.Vehicles.Any())
            {
                this.AddVehicle("Renault Megane", "DT-18-DH", "Renault", "4 Doors", "The French car", user);
                this.AddVehicle("Opel Astra", "RJ-10-KF", "Opel", "4 Doors", "Not that small", user);
                this.AddVehicle("Fiat 500", "F6-21-GJ", "Fiat", "2 Doors", "Small and portable", user);
                this.AddVehicle("Other", "N/A", "N/A", "N/A", "N/A", user);
                await _context.SaveChangesAsync();
            }
        }


        private void AddVehicle(string name, string licensePlate, string makerName, string typeName, string details, User user)
        {
            _context.Vehicles.Add(new Vehicle
            {
                ModelName = name,
                MakerName = makerName,
                LicensePlate = licensePlate,
                typeName = typeName,
                Details = details,
                RepairPrice = _random.Next(1000),
                User = user
            });
        }
    }
}
