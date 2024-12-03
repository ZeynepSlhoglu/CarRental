using CarRental.CarRental.Domain.Vehicles;
using CarRental.CarRental.Infrastructure.BaseRepository;
using Microsoft.EntityFrameworkCore;

namespace CarRental.CarRental.Infrastructure.Vehicles
{
    public class VehicleRepository : BaseRepository<Vehicle>, IVehicleRepository
    {
        private readonly ApplicationDbContext _context;

        public VehicleRepository(ApplicationDbContext context) : base(context)
        {
            _context = context; 
        }

        public async Task<List<Vehicle>> GetVehiclesWithStatisticsAsync()
        {
            return await _context.Set<Vehicle>()
                .Select(vehicle => new Vehicle
                {
                    Id = vehicle.Id,
                    Name = vehicle.Name,
                    Plate = vehicle.Plate,
                    ActiveWorkTime = vehicle.ActiveWorkTime,
                    MaintenanceTime = vehicle.MaintenanceTime
                }).ToListAsync();
        }
    }
}
