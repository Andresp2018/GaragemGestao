using GaragemGestao.Data.Entities;
using GaragemGestao.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GaragemGestao.Data.Repositories
{
    public interface IRepairRepository : IGenericRepository<Repair>
    {
        Task<IQueryable<Repair>> GetRepairAsync(string username);


        Task<IQueryable<RepairDetailTemp>> GetDetailTempsAsync(string username);


        Task AddItemToRepairAsync(AddVehicleViewModel model, string userName);


        Task ModifyRepairDetailTempQuantityAsync(int id, double quantity);


        Task DeleteDetailTempAsync(int id);

        Task EditDetailsAsync(int id);


        Task<bool> ConfirmRepairAsync(string userName);
    }
}
