using CarRental.CarRental.Application.Common;
using CarRental.CarRental.Domain.Vehicles;
using CarRental.Models;
using FluentValidation;
using System.Globalization;

namespace CarRental.CarRental.Application.Vehicles
{
    public class VehicleService : IVehicleService
    {
        private readonly IVehicleRepository _vehicleRepository;
        private readonly IValidator<Vehicle> _vehicleValidator;
        private readonly IValidator<(string ActiveWorkTime, string MaintenanceTime)> _workTimeValidator;

        public VehicleService(IVehicleRepository vehicleRepository,
                              IValidator<Vehicle> vehicleValidator,
                              IValidator<(string ActiveWorkTime, string MaintenanceTime)> workTimeValidator)
        {
            _vehicleRepository = vehicleRepository;
            _vehicleValidator = vehicleValidator;
            _workTimeValidator = workTimeValidator;
        }

        public async Task<IEnumerable<Vehicle>> GetAllVehiclesAsync() => await _vehicleRepository.GetAllAsync();

        public async Task<Vehicle> GetVehicleByIdAsync(Guid id) => await _vehicleRepository.GetByIdAsync(id);

        public async Task<ServiceResult> AddVehicleAsync(Vehicle vehicle)
        {
            var validationResult = _vehicleValidator.Validate(vehicle);
            return !validationResult.IsValid
                ? ServiceResult.Failure(string.Join(" ", validationResult.Errors.Select(e => e.ErrorMessage)))
                : await _vehicleRepository.AddAsync(vehicle).ContinueWith(_ => ServiceResult.Success("Araç kaydı başarılı.")); ;
        }

        public async Task<ServiceResult> UpdateVehicleAsync(Vehicle vehicle)
        {
            var validationResult = _vehicleValidator.Validate(vehicle);
            return !validationResult.IsValid
                ? ServiceResult.Failure(string.Join(" ", validationResult.Errors.Select(e => e.ErrorMessage)))
                : await _vehicleRepository.UpdateAsync(vehicle).ContinueWith(_ => ServiceResult.Success("Araç güncelleme başarılı."));
        }

        public async Task<ServiceResult> DeleteVehicleAsync(Guid id)
        {
            var vehicle = await _vehicleRepository.GetByIdAsync(id);
            return vehicle == null
                ? ServiceResult.Failure("Araç bulunamadı.")
                : await _vehicleRepository.DeleteAsync(id).ContinueWith(_ => ServiceResult.Success("Araç silme işlemi başarılı."));
        }

        public async Task<List<VehicleViewModel>> GetVehicleStatisticsAsync()
        {
            var vehicles = await _vehicleRepository.GetVehiclesWithStatisticsAsync();
            return vehicles.Select(v => new VehicleViewModel
            {
                Id = v.Id,
                Name = v.Name,
                Plate = v.Plate,
                ActiveWorkTime = v.ActiveWorkTime,
                MaintenanceTime = v.MaintenanceTime
            }).ToList();
        }

        public async Task<GraphDataViewModel> GetVehicleGraphDataAsync()
        {
            var vehicles = await GetVehicleStatisticsAsync();

            return new GraphDataViewModel
            {
                VehicleViewModels = vehicles,
                ActiveWorkTimes = vehicles.Select(v => v.ActiveWorkTimePercentage).ToList(),
                IdleTimes = vehicles.Select(v => v.IdleTimePercentage).ToList(),
                Labels = vehicles.Select(v => v.Name).ToList()
            };
        }

        public async Task<ServiceResult> UpdateWorkTimesAsync(Guid id, string activeWorkTime, string maintenanceTime)
        {
            var validationResult = _workTimeValidator.Validate((activeWorkTime, maintenanceTime));
            return !validationResult.IsValid
                ? ServiceResult.Failure(string.Join(" ", validationResult.Errors.Select(e => e.ErrorMessage)))
                : await _vehicleRepository.GetByIdAsync(id).ContinueWith(vehicleTask =>
                {
                    var vehicle = vehicleTask.Result;
                    if (vehicle == null) return ServiceResult.Failure("Araç bulunamadı.");

                    vehicle.ActiveWorkTime = double.Parse(activeWorkTime, CultureInfo.InvariantCulture);
                    vehicle.MaintenanceTime = double.Parse(maintenanceTime, CultureInfo.InvariantCulture);
                    _vehicleRepository.UpdateAsync(vehicle);

                    return ServiceResult.Success("Araç başarıyla güncellendi.");
                });
        }
    }
}
