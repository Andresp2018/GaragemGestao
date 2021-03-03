using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GaragemGestao.Data;
using GaragemGestao.Data.Entities;
using GaragemGestao.Data.Repositories;
using GaragemGestao.Models;

namespace GaragemGestao.Controllers
{
    public class RepairsController : Controller
    {
        private readonly IRepairRepository _repairRepository;
        private readonly IVehicleRepository _vehicleRepository;

        public RepairsController(IRepairRepository repairRepository, IVehicleRepository vehicleRepository)
        {
            _repairRepository = repairRepository;
            _vehicleRepository = vehicleRepository;
        }
        public async Task<IActionResult> Index()
        {
            var model = await _repairRepository.GetRepairAsync(this.User.Identity.Name);
            return View(model);
        }

        public async Task<IActionResult> Create()
        {
            var model = await _repairRepository.GetDetailTempsAsync(this.User.Identity.Name);
            return View(model);
        }


        public IActionResult AddVehicle()
        {
            var model = new AddVehicleViewModel
            {
                Quantity = 1,
                Vehicles = _vehicleRepository.GetComboVehicles()
            };

            return View(model);
        }


        [HttpPost]
        public async Task<IActionResult> AddVehicle(AddVehicleViewModel model)
        {
            if (this.ModelState.IsValid)
            {
                await _repairRepository.AddItemToRepairAsync(model, this.User.Identity.Name);
                return this.RedirectToAction("Create");
            }

            return this.View(model);
        }


        public async Task<IActionResult> DeleteItem(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            await _repairRepository.DeleteDetailTempAsync(id.Value);
            return this.RedirectToAction("Create");
        }


        public async Task<IActionResult> Increase(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            await _repairRepository.ModifyRepairDetailTempQuantityAsync(id.Value, 1);
            return this.RedirectToAction("Create");
        }


        public async Task<IActionResult> Decrease(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            await _repairRepository.ModifyRepairDetailTempQuantityAsync(id.Value, -1);
            return this.RedirectToAction("Create");
        }


        public async Task<IActionResult> ConfirmRepair()
        {
            var response = await _repairRepository.ConfirmRepairAsync(this.User.Identity.Name);
            if (response)
            {
                return this.RedirectToAction("Index");
            }

            return this.RedirectToAction("Create");
        }
    }
}

