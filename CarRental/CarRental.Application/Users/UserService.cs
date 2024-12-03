using CarRental.CarRental.Application.Common;
using CarRental.CarRental.Application.Users;
using CarRental.CarRental.Domain.Users;
using FluentValidation;
using Microsoft.AspNetCore.Identity;

namespace CarRental.CarRental.Application.Users
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly IValidator<User> _userValidator;

        public UserService(IUserRepository userRepository, IPasswordHasher<User> passwordHasher, IValidator<User> userValidator)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
            _userValidator = userValidator;
        }

        public async Task<User> GetUserByIdAsync(Guid id) => await _userRepository.GetByIdAsync(id);
       
         
        public async Task<User> GetUserByUsernameAsync(string username) => await _userRepository.GetByUsernameAsync(username);

        public async Task<IEnumerable<User>> GetAllUsersAsync() => await _userRepository.GetAllAsync();
         
        public async Task<ServiceResult> ValidateUserAsync(string username, string password)
        {
            var user = await _userRepository.GetByUsernameAsync(username);
            return user == null || _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, password) != PasswordVerificationResult.Success
                ? ServiceResult.Failure("Kullanıcı adı veya şifre hatalı.")
                : ServiceResult.Success("Giriş başarılı.");
        }

    }
}
 