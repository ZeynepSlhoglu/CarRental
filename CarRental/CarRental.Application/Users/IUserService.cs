using CarRental.CarRental.Application.Common;
using CarRental.CarRental.Domain.Users;

namespace CarRental.CarRental.Application.Users
{
    public interface IUserService
    {
        Task<User> GetUserByIdAsync(Guid id);
        Task<User> GetUserByUsernameAsync(string username);
        Task<IEnumerable<User>> GetAllUsersAsync(); 
        Task<ServiceResult> ValidateUserAsync(string username, string password);
    }
}
