using CarRental.CarRental.Domain.Vehicles;

namespace CarRental.CarRental.Application.Vehicles
{
    public class VehicleService : IVehicleService
    {
        private readonly IVehicleRepository _vehicleRepository;

        public VehicleService(IVehicleRepository vehicleRepository)
        {
            _vehicleRepository = vehicleRepository;
        }

        public async Task<IEnumerable<Vehicle>> GetAllVehiclesAsync()
        {
            return await _vehicleRepository.GetAllAsync();
        }

        public async Task<Vehicle> GetVehicleByIdAsync(Guid id)
        {
            return await _vehicleRepository.GetByIdAsync(id);
        }

        public async Task<string> AddVehicleAsync(Vehicle vehicle)
        {
            if (vehicle == null)
            {
                return "Geçersiz araç verisi.";
            }

            await _vehicleRepository.AddAsync(vehicle);
            return "Araç kaydı başarılı.";
        }

        public async Task<string> UpdateVehicleAsync(Vehicle vehicle)
        {
            if (vehicle == null)
            {
                return "Geçersiz araç verisi.";
            }

            await _vehicleRepository.UpdateAsync(vehicle);
            return "Araç güncelleme başarılı.";
        }

        public async Task<string> DeleteVehicleAsync(Guid id)
        {
            var vehicle = await _vehicleRepository.GetByIdAsync(id);
            if (vehicle == null)
            {
                return "Araç bulunamadı.";
            }

            await _vehicleRepository.DeleteAsync(id);
            return "Araç silme işlemi başarılı.";
        }
    }
}
