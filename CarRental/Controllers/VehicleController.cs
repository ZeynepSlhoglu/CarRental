using CarRental.CarRental.Application.Vehicles;
using CarRental.CarRental.Domain.Vehicles; 
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc; 

namespace CarRental.Controllers
{
    public class VehicleController : Controller
    {
        private readonly IVehicleService _vehicleService;

        public VehicleController(IVehicleService vehicleService)
        {
            _vehicleService = vehicleService;
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Index()
        {
            var vehicles = await _vehicleService.GetAllVehiclesAsync();
            return View(vehicles);
        }

        public async Task<IActionResult> CreateFunc(Vehicle vehicle)
        {
            var result = await _vehicleService.AddVehicleAsync(vehicle);
            if (result.SuccessStatus)
            {
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", result.Message);
            return View(vehicle);
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> EditFunc(Vehicle vehicle)
        {
            var result = await _vehicleService.UpdateVehicleAsync(vehicle);
            if (result.SuccessStatus)
            {
                return RedirectToAction("Index");
            }

            return View(vehicle);
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _vehicleService.DeleteVehicleAsync(id);
            if (result.SuccessStatus)
            {
                return RedirectToAction("Index");
            }

            return BadRequest(result.Message);
        }

        [Authorize(Roles = "User")]
        public async Task<IActionResult> Statistics()
        {
            var vehicleViewModels = await _vehicleService.GetVehicleStatisticsAsync();
            return View(vehicleViewModels);
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Graphics()
        {
            var graphData = await _vehicleService.GetVehicleGraphDataAsync();

            ViewBag.ActiveWorkTimes = graphData.ActiveWorkTimes;
            ViewBag.IdleTimes = graphData.IdleTimes;
            ViewBag.Labels = graphData.Labels;

            return View(graphData.VehicleViewModels);
        }

        [Authorize(Roles = "User")]
        public async Task<IActionResult> UpdateWorkTimes(Guid id, string activeWorkTime, string maintenanceTime)
        {
            var result = await _vehicleService.UpdateWorkTimesAsync(id, activeWorkTime, maintenanceTime);

            if (result.SuccessStatus)
            {
                return Json(new { success = true, message = result.Message });
            }

            return Json(new { success = false, message = result.Message });
        }
    }
}