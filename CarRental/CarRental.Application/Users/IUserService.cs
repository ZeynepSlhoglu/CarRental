using CarRental.CarRental.Domain.Users;

namespace CarRental.CarRental.Application.Users
{
    public interface IUserService
    {
        Task<User> GetUserByIdAsync(Guid id);
        Task<User> GetUserByUsernameAsync(string username);
        Task<IEnumerable<User>> GetAllUsersAsync();
        Task RegisterUserAsync(User user, string password);
        Task<bool> ValidateUserAsync(string username, string password);
    }
}
