using CarRental.CarRental.Application.Auth; 
using CarRental.CarRental.Domain.Roles;
using CarRental.CarRental.Domain.Users;

namespace CarRental.CarRental.Infrastructure
{
    public class DataSeeder
    {
        private readonly ApplicationDbContext _context;
        private readonly IAuthService _authService;

        public DataSeeder(ApplicationDbContext context, IAuthService authService)
        {
            _context = context;
            _authService = authService;
        }

        public async Task SeedAsync()
        { 
            await _context.Database.EnsureCreatedAsync();
             
            await AddRolesAsync();
             
            await AddAdminUserAsync();
        }

        private async Task AddRolesAsync()
        { 
            if (!_context.Roles.Any(r => r.Name == "Admin"))
            {
                var adminRole = new Role { Id = Guid.NewGuid(), Name = "Admin" };
                _context.Roles.Add(adminRole);
            }

            if (!_context.Roles.Any(r => r.Name == "User"))
            {
                var userRole = new Role { Id = Guid.NewGuid(), Name = "User" };
                _context.Roles.Add(userRole);
            }

            await _context.SaveChangesAsync();
        }

        private async Task AddAdminUserAsync()
        {
            if (!_context.Users.Any(u => u.Username == "Admin"))
            {
                var adminRole = _context.Roles.SingleOrDefault(r => r.Name == "Admin");
                if (adminRole == null)
                {
                    throw new Exception("Admin rolü bulunamadı.");
                }

                var adminUser = new User
                {
                    Username = "Admin",
                    RoleId = adminRole.Id,
                    Role = adminRole
                };

                var result = await _authService.RegisterAsync(adminUser, "admin1");
                if (!result.SuccessStatus)
                {
                    throw new Exception("Admin kullanıcı oluşturulamadı: " + result.Message);
                }
            }
        }

    }
}
