using CarRental.CarRental.Application.Vehicles;
using CarRental.CarRental.Domain.Vehicles;
using CarRental.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CarRental.Controllers
{
    [Authorize(Roles = "Admin")]
    public class VehicleController : Controller
    {
        private readonly IVehicleService _vehicleService;

        public VehicleController(IVehicleService vehicleService)
        {
            _vehicleService = vehicleService;
        }
        public async Task<IActionResult> Index()
        {
            var vehicles = await _vehicleService.GetAllVehiclesAsync();
            return View(vehicles);  
        }
        public async Task<IActionResult> Details(Guid id)
        {
            var vehicle = await _vehicleService.GetVehicleByIdAsync(id);
            if (vehicle == null)
            {
                return NotFound();
            }
            return View(vehicle);
        }
        public IActionResult Create()
        {
            return View();
        }
         
        public async Task<IActionResult> CreateFunc(Vehicle vehicle)
        { 
                var result = await _vehicleService.AddVehicleAsync(vehicle);
                if (result == "Araç kaydı başarılı.")
                {
                    return RedirectToAction("Index");
                }
                ModelState.AddModelError("", result);
         
            return View(vehicle);
        }

        public async Task<IActionResult> Edit(Guid id)
        {
            var vehicle = await _vehicleService.GetVehicleByIdAsync(id);
            if (vehicle == null)
            {
                return NotFound();
            }
            return View(vehicle);
        }

        public async Task<IActionResult> EditFunc(Vehicle vehicle)
        {
            if (ModelState.IsValid)
            {
                var result = await _vehicleService.UpdateVehicleAsync(vehicle);
                if (result == "Araç güncelleme başarılı.")
                {
                    return RedirectToAction("Index");
                }
                ModelState.AddModelError("", result);
            }
            return View(vehicle);
        }

        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _vehicleService.DeleteVehicleAsync(id);
            if (result == "Araç silme işlemi başarılı.")
            {
                return RedirectToAction("Index");
            }
            return BadRequest(result);
        }
    }
}
