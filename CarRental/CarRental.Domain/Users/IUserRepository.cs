using CarRental.CarRental.Domain.BaseRepository;
using CarRental.CarRental.Domain.Roles;

namespace CarRental.CarRental.Domain.Users
{
    public interface IUserRepository : IBaseRepository<User>
    {
        Task<User> GetByUsernameAsync(string username);
        Task<Role> GetRoleByNameAsync(string roleName);
    }
}
