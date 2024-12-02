using CarRental.CarRental.Application.Vehicles;
using CarRental.CarRental.Domain.Vehicles;
using CarRental.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;

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
            if (result == "Araç kaydı başarılı.")
            {
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", result);

            return View(vehicle);
        }
         
        public async Task<IActionResult> EditFunc(Vehicle vehicle)
        {
            var result = await _vehicleService.UpdateVehicleAsync(vehicle);
            if (result == "Araç güncelleme başarılı.")
            {
                return RedirectToAction("Index");
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

        [Authorize(Roles = "User")]
        public async Task<IActionResult> Statistics()
        {
            var vehicles = await _vehicleService.GetAllVehiclesAsync();

            var vehicleViewModels = vehicles.Select(v => new VehicleViewModel
            {
                Id = v.Id,
                Name = v.Name,
                Plate = v.Plate,
                ActiveWorkTime = v.ActiveWorkTime,
                MaintenanceTime = v.MaintenanceTime
            }).ToList();

            return View(vehicleViewModels);
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Graphics()
        { 
            var vehicles = await _vehicleService.GetAllVehiclesAsync();
             
            var vehicleViewModels = vehicles.Select(v => new VehicleViewModel
            {
                Id = v.Id,
                Name = v.Name,
                Plate = v.Plate,
                ActiveWorkTime = v.ActiveWorkTime,
                MaintenanceTime = v.MaintenanceTime
            }).ToList();
             
            var activeWorkTimes = vehicleViewModels.Select(v => v.ActiveWorkTimePercentage).ToList();
            var idleTimes = vehicleViewModels.Select(v => v.IdleTimePercentage).ToList();
            var labels = vehicleViewModels.Select(v => v.Name).ToList();
             
            ViewBag.ActiveWorkTimes = activeWorkTimes;
            ViewBag.IdleTimes = idleTimes;
            ViewBag.Labels = labels;

            return View(vehicleViewModels);
        }

        public async Task<IActionResult> UpdateWorkTimes(Guid id, string activeWorkTime, string maintenanceTime)
        {
            // Ondalık ayracın doğru şekilde yorumlanması için InvariantCulture kullanın
            if (!double.TryParse(activeWorkTime, NumberStyles.Float, CultureInfo.InvariantCulture, out double parsedActiveWorkTime) ||
                !double.TryParse(maintenanceTime, NumberStyles.Float, CultureInfo.InvariantCulture, out double parsedMaintenanceTime))
            {
                return Json(new { success = false, message = "Geçersiz sayı formatı." });
            }

            // Toplam süre kontrolü
            if (parsedActiveWorkTime + parsedMaintenanceTime > 168)
            {
                return Json(new { success = false, message = "Aktif Çalışma Süresi ve Bakım Süresi toplamı 168 saati geçemez." });
            }

            var vehicle = await _vehicleService.GetVehicleByIdAsync(id);
            if (vehicle == null)
            {
                return NotFound();
            }

            vehicle.ActiveWorkTime = parsedActiveWorkTime;
            vehicle.MaintenanceTime = parsedMaintenanceTime;

            var result = await _vehicleService.UpdateVehicleAsync(vehicle);
            if (result == "Araç güncelleme başarılı.")
            {
                return Json(new { success = true, message = "Araç başarıyla güncellendi." });
            }
            else
            {
                return Json(new { success = false, message = result });
            }
        }




    }
}
