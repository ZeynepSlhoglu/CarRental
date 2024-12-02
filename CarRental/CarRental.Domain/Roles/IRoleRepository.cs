using CarRental.CarRental.Domain.BaseRepository;

namespace CarRental.CarRental.Domain.Roles
{
    public interface IRoleRepository : IBaseRepository<Role>
    { 
        Task<Role?> GetByNameAsync(string name); 
    }
}
