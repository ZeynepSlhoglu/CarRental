using CarRental.CarRental.Domain.Users;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CarRental.CarRental.Application.Auth
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AuthService(IUserRepository userRepository,
                           IPasswordHasher<User> passwordHasher,
                           IHttpContextAccessor httpContextAccessor)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
            _httpContextAccessor = httpContextAccessor;
        }

        #region RegisterAsync 
        public async Task<string> RegisterAsync(User user, string password)
        {
             
            var existingUser = await _userRepository.GetByUsernameAsync(user.Username);
            if (existingUser != null)
            {
                return "Bu kullanıcı adı zaten alınmış.";
            }
             
            var userRole = await _userRepository.GetRoleByNameAsync("User");
            if (userRole == null)
            {
                return "Rol bulunamadı.";
            }

            user.Role = userRole;
            user.RoleId = userRole.Id;
             
            user.PasswordHash = _passwordHasher.HashPassword(user, password);
             
            await _userRepository.AddAsync(user);

            return "Kullanıcı kaydı başarılı.";
        }


        #endregion

        #region LoginAsync 
        public async Task<string> LoginAsync(string username, string password)
        { 
            var user = await _userRepository.GetByUsernameAsync(username);
             
            if (user == null || _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, password) != PasswordVerificationResult.Success)
            {
                return "Geçersiz kullanıcı adı veya şifre.";
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

            return "Giriş başarılı.";
        }

        #endregion

        #region LogoutAsync 

        public async Task LogoutAsync()
        { 
            await _httpContextAccessor.HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        }

        #endregion
    }
}
