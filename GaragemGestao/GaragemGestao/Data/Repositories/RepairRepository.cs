using GaragemGestao.Data.Entities;
using GaragemGestao.Helpers;
using GaragemGestao.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GaragemGestao.Data.Repositories
{
    public class RepairRepository : GenericRepository<Repair>, IRepairRepository
    {
        private readonly DataContext _context;
        private readonly IUserHelper _userHelper;

        public RepairRepository(DataContext context, IUserHelper userHelper) : base(context)
        {
            _context = context;
            _userHelper = userHelper;
        }


        public async Task AddItemToRepairAsync(AddVehicleViewModel model, string userName)
        {
            var user = await _userHelper.GetUserByEmailAsync(userName);
            if (user == null)
            {
                return;
            }


            var vehicle = await _context.Vehicles.FindAsync(model.VehicleId);
            if (vehicle == null)
            {
                return;
            }

            var repairDetailTemp = await _context.RepairDetailTemps
                .Where(odt => odt.User == user && odt.Vehicle == vehicle)
                .FirstOrDefaultAsync();

            if (repairDetailTemp == null)
            {
                repairDetailTemp = new RepairDetailTemp
                {
                    Price = vehicle.RepairPrice,
                    Vehicle = vehicle,
                    Issue = model.Issue,
                    Quantity = model.Quantity,
                    User = user,
                };

                _context.RepairDetailTemps.Add(repairDetailTemp);
            }
            else
            {
                repairDetailTemp.Quantity += model.Quantity;
                _context.RepairDetailTemps.Update(repairDetailTemp);
            }

            await _context.SaveChangesAsync();
        }

        public async Task<bool> ConfirmRepairAsync(string userName)
        {
            var user = await _userHelper.GetUserByEmailAsync(userName);
            if (user == null)
            {
                return false;
            }

            var repairTmps = await _context.RepairDetailTemps
                .Include(o => o.Vehicle)
                .Where(o => o.User == user)
                .ToListAsync();

            if (repairTmps == null || repairTmps.Count == 0)
            {
                return false;
            }

            var details = repairTmps.Select(o => new RepairDetail
            {
                Price = o.Price,
                Vehicle = o.Vehicle,
                Issue = o.Issue,
                Quantity = o.Quantity
            }).ToList();


            var repair = new Repair
            {
                RepairDate = DateTime.UtcNow,
                User = user,
                Items = details,
            };

            _context.Repairs.Add(repair);
            _context.RepairDetailTemps.RemoveRange(repairTmps);
            await _context.SaveChangesAsync();
            return true;
        }



        public async Task DeleteDetailTempAsync(int id)
        {
            var repairDetailTemp = await _context.RepairDetailTemps.FindAsync(id);
            if (repairDetailTemp == null)
            {
                return;
            }

            _context.RepairDetailTemps.Remove(repairDetailTemp);
            await _context.SaveChangesAsync();
        }

        public async Task DeliverRepairAsync(DeliverViewModel model)
        {
            var order = await _context.Repairs.FindAsync(model.Id);
            if (order == null)
            {
                return;
            }


            order.DeliveryDate = model.DeliveryDate;
            _context.Repairs.Update(order);
            await _context.SaveChangesAsync();
        }

        public async Task EditDetailsAsync(int id)
        {
            var repairDetailTemp = await _context.RepairDetailTemps.FindAsync(id);
            if (repairDetailTemp == null)
            {
                _context.RepairDetailTemps.Add(repairDetailTemp);
                await _context.SaveChangesAsync();
            }
            return;

        }

        public async Task<IQueryable<RepairDetailTemp>> GetDetailTempsAsync(string username)
        {
            var user = await _userHelper.GetUserByEmailAsync(username);
            if (user == null)
            {
                return null;
            }

            return _context.RepairDetailTemps
               .Include(o => o.Vehicle)
               .Where(o => o.User == user)
               .OrderBy(o => o.Vehicle.ModelName);
        }


        public async Task<IQueryable<Repair>> GetRepairAsync(string username)
        {
            var user = await _userHelper.GetUserByEmailAsync(username);
            if (user == null)
            {
                return null;
            }

            if (await _userHelper.IsUserInRoleAsync(user, "Admin"))
            {
                //If User is admin
                return _context.Repairs
                .Include(o => o.User)
                .Include(o => o.Items)
                .ThenInclude(i => i.Vehicle)
                .OrderByDescending(o => o.RepairDate);

            }

            //If user is Client
            return _context.Repairs
                .Include(o => o.Items)
                .ThenInclude(i => i.Vehicle)
                .Where(o => o.User == user)
                .OrderByDescending(o => o.RepairDate);
        }

        public async Task<Repair> GetRepairAsync(int id)
        {
            return await _context.Repairs.FindAsync(id);
        }

        //The cart will only modify the quantity
        public async Task ModifyRepairDetailTempQuantityAsync(int id, double quantity)
        {
            var repairDetailTemp = await _context.RepairDetailTemps.FindAsync(id);
            if (repairDetailTemp == null)
            {
                return;
            }

            repairDetailTemp.Quantity += quantity;
            if (repairDetailTemp.Quantity > 0)
            {
                _context.RepairDetailTemps.Update(repairDetailTemp);
                await _context.SaveChangesAsync();
            }
        }
    }
}
