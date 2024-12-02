using CarRental.CarRental.Domain.Vehicles;
using CarRental.CarRental.Infrastructure.BaseRepository;

namespace CarRental.CarRental.Infrastructure.Vehicles
{
    public class VehicleRepository : BaseRepository<Vehicle>, IVehicleRepository
    {
        public VehicleRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
