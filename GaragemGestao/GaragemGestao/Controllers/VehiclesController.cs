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
using GaragemGestao.Helpers;
using Microsoft.AspNetCore.Authorization;
using GaragemGestao.Models;
using System.IO;

namespace GaragemGestao.Controllers
{
    public class VehiclesController : Controller
    {
        private readonly IVehicleRepository _vehicleRepository;
        private readonly IUserHelper _userHelper;

        public VehiclesController(IVehicleRepository vehicleRepository, IUserHelper userHelper)
        {
            _vehicleRepository = vehicleRepository;
            _userHelper = userHelper;
        }

        // GET: Vehicles
        public IActionResult Index()
        {
            return View(_vehicleRepository.GetAll());
        }

        // GET: Vehicles/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vehicle = await _vehicleRepository.GetByIdAsync(id.Value);
            if (vehicle == null)
            {
                return NotFound();
            }

            return View(vehicle);
        }
        [Authorize(Roles = "Client,Admin")]
        // GET: Vehicles/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Vehicles/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(VehicleViewModel view)
        {
            if (ModelState.IsValid)
            {
                var path = string.Empty;

                if (view.ImageFile != null && view.ImageFile.Length > 0)
                {
                    var guid = Guid.NewGuid().ToString();
                    var file = $"{guid}.jpg";

                    path = Path.Combine(Directory.GetCurrentDirectory(),
                        "wwwroot\\images\\Vehicles",
                        file);

                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        await view.ImageFile.CopyToAsync(stream);
                    }

                    path = $"~/images/Vehicles/{file}";
                }

                var vehicle = this.ToVehicle(view, path);
                vehicle.User = await _userHelper.GetUserByEmailAsync(this.User.Identity.Name);
                await _vehicleRepository.CreateAsync(vehicle);
                return RedirectToAction(nameof(Index));
            }
            return View(view);
        }

        private Vehicle ToVehicle(VehicleViewModel view, string path)
        {
            return new Vehicle
            {
                Id = view.Id,
                ImageUrl = path,
                LicensePlate=view.LicensePlate,
                MakerName=view.MakerName,
                ModelName=view.ModelName,
                Details=view.Details,
                RepairPrice=view.RepairPrice,
                typeName=view.typeName,
                User = view.User
            };
        }
        [Authorize(Roles = "Admin")]
        // GET: Vehicles/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vehicle = await _vehicleRepository.GetByIdAsync(id.Value);
            if (vehicle == null)
            {
                return NotFound();
            }
            var view = this.ToVehicleViewModel(vehicle);
            return View(vehicle);
        }
        private VehicleViewModel ToVehicleViewModel(Vehicle vehicle)
        {
            return new VehicleViewModel
            {
                Id = vehicle.Id,
                ImageUrl = vehicle.ImageUrl,
                LicensePlate = vehicle.LicensePlate,
                MakerName = vehicle.MakerName,
                ModelName = vehicle.ModelName,
                Details = vehicle.Details,
                RepairPrice = vehicle.RepairPrice,
                typeName = vehicle.typeName,
                User = vehicle.User
            };

        }

        // POST: Vehicles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, VehicleViewModel view)
        {
            if (id != view.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var path = view.ImageUrl;

                    if (view.ImageFile != null && view.ImageFile.Length > 0)
                    {

                        var guid = Guid.NewGuid().ToString();
                        var file = $"{guid}.jpg";

                        path = Path.Combine(Directory.GetCurrentDirectory(),
                        "wwwroot\\images\\Vehicles",
                        file);

                        using (var stream = new FileStream(path, FileMode.Create))
                        {
                            await view.ImageFile.CopyToAsync(stream);
                        }

                        path = $"~/images/Vehicles/{file}";
                    }

                    var vehicle = this.ToVehicle(view, path);
                    vehicle.User = await _userHelper.GetUserByEmailAsync(this.User.Identity.Name);
                    await _vehicleRepository.UpdateAsync(vehicle);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await _vehicleRepository.ExistAsync(view.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(view);
        }

        [Authorize(Roles = "Admin")]
        // GET: Vehicles/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vehicle = await _vehicleRepository.GetByIdAsync(id.Value);
            if (vehicle == null)
            {
                return NotFound();
            }

            return View(vehicle);
        }
        // POST: Vehicles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var vehicle = await _vehicleRepository.GetByIdAsync(id);
            await _vehicleRepository.DeleteAsync(vehicle);
            return RedirectToAction(nameof(Index));
        }
    }
}
