using CarRental.CarRental.Domain.Vehicles;

namespace CarRental.CarRental.Application.Vehicles
{
    public interface IVehicleService
    {
        Task<IEnumerable<Vehicle>> GetAllVehiclesAsync();
        Task<Vehicle> GetVehicleByIdAsync(Guid id);
        Task<string> AddVehicleAsync(Vehicle vehicle);
        Task<string> UpdateVehicleAsync(Vehicle vehicle);
        Task<string> DeleteVehicleAsync(Guid id);
    }
}
