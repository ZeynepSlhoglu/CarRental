using CarRental.CarRental.Domain.Users;

namespace CarRental.CarRental.Application.Auth
{
    public interface IAuthService
    {
        Task<string> LoginAsync(string username, string password);
        Task<string> RegisterAsync(User user, string password);
        Task LogoutAsync();
    }
}
