using CarRental.CarRental.Application.Auth; 
using CarRental.CarRental.Domain.Users; 
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
            if (!result.SuccessStatus)
            {
                return Unauthorized(result.Message);
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
            if (!result.SuccessStatus)
            {
                return BadRequest(result.Message);
            }

            return RedirectToAction("Login");
        }

        public async Task<IActionResult> Logout()
        {
            var result = await _authService.LogoutAsync();
            if (!result.SuccessStatus)
            {
                return BadRequest(result.Message);
            }

            return RedirectToAction("Login", "Auth");
        }
         
    } 
} 
