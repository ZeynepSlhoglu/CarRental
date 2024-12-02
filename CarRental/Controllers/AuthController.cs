using CarRental.CarRental.Application.Auth;
using CarRental.CarRental.Domain.Users;
using CarRental.CarRental.Infrastructure.Users;
using Microsoft.AspNetCore.Mvc;

namespace CarRental.Controllers
{
    public class AuthController : Controller
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        public IActionResult Login()
        { 
            return View();
        }
        public async Task<IActionResult> LoginFunc(string username, string password)
        {
            var result = await _authService.LoginAsync(username, password);
            if (result != "Giriş başarılı.")
            {
                return Unauthorized(result);
            }

            return RedirectToAction("Index", "Home");
        }

        public IActionResult Register()
        {
            return View();
        }
        public async Task<IActionResult> RegisterFunc(User user, string password)
        {
            var result = await _authService.RegisterAsync(user, password);
            if (result != "Kullanıcı kaydı başarılı.")
            {
                return BadRequest(result);
            }

            return RedirectToAction("Login");
        }

        public async Task<IActionResult> Logout()
        {
            await _authService.LogoutAsync();
            return RedirectToAction("Login", "Auth");
        }
         
    } 
} 
