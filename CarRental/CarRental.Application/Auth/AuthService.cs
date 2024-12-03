using CarRental.CarRental.Domain.Users;
using CarRental.CarRental.Application.Auth;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using FluentValidation;
using CarRental.CarRental.Application.Common;

namespace CarRental.CarRental.Application.Auth
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IValidator<User> _authValidator;

        public AuthService(IUserRepository userRepository,
                           IPasswordHasher<User> passwordHasher,
                           IHttpContextAccessor httpContextAccessor,
                           IValidator<User> authValidator)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
            _httpContextAccessor = httpContextAccessor;
            _authValidator = authValidator;
        }

        #region RegisterAsync
        public async Task<ServiceResult> RegisterAsync(User user, string password)
        {
            var validationResult = await _authValidator.ValidateAsync(user);
            if (!validationResult.IsValid)
            {
                return ServiceResult.Failure(string.Join(" ", validationResult.Errors.Select(e => e.ErrorMessage)));
            }

            if (user.RoleId == null || user.RoleId == Guid.Empty)
            {
                var userRole = await _userRepository.GetRoleByNameAsync("User");
                user.Role = userRole;
                user.RoleId = userRole.Id;
            }

            user.PasswordHash = _passwordHasher.HashPassword(user, password);

            await _userRepository.AddAsync(user);

            return ServiceResult.Success("Kullanıcı kaydı başarılı.");
        }
        #endregion

        #region LoginAsync
        public async Task<ServiceResult> LoginAsync(string username, string password)
        {
            var user = await _userRepository.GetByUsernameAsync(username);

            if (user == null || _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, password) != PasswordVerificationResult.Success)
            {
                return ServiceResult.Failure("Geçersiz kullanıcı adı veya şifre.");
            }

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Role, user.Role.Name)
            };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(claimsIdentity);

            await _httpContextAccessor.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

            return ServiceResult.Success("Giriş başarılı.");
        }
        #endregion

        #region LogoutAsync
        public async Task<ServiceResult> LogoutAsync()
        {
            await _httpContextAccessor.HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return ServiceResult.Success("Çıkış başarılı.");
        }
        #endregion
    }
}
