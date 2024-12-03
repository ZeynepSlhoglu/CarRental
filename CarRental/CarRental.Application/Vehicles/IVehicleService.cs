using CarRental.CarRental.Application.Common;
using CarRental.CarRental.Domain.Vehicles;
using CarRental.Models;

namespace CarRental.CarRental.Application.Vehicles
{
    public interface IVehicleService
    {
        Task<IEnumerable<Vehicle>> GetAllVehiclesAsync();
        Task<Vehicle> GetVehicleByIdAsync(Guid id);
        Task<ServiceResult> AddVehicleAsync(Vehicle vehicle);
        Task<ServiceResult> UpdateVehicleAsync(Vehicle vehicle);
        Task<ServiceResult> DeleteVehicleAsync(Guid id);
        Task<List<VehicleViewModel>> GetVehicleStatisticsAsync();
        Task<GraphDataViewModel> GetVehicleGraphDataAsync();
        Task<ServiceResult> UpdateWorkTimesAsync(Guid id, string activeWorkTime, string maintenanceTime);
    }

}
