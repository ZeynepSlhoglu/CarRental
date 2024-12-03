using CarRental.CarRental.Application.Common;
using CarRental.CarRental.Domain.Users;

namespace CarRental.CarRental.Application.Auth
{
    public interface IAuthService
    {
        Task<ServiceResult> LoginAsync(string username, string password);
        Task<ServiceResult> RegisterAsync(User user, string password);
        Task<ServiceResult> LogoutAsync();
    }
}
