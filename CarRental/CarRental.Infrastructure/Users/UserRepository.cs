using CarRental.CarRental.Domain.Roles;
using CarRental.CarRental.Domain.Users;
using CarRental.CarRental.Infrastructure.BaseRepository;
using Microsoft.EntityFrameworkCore;

namespace CarRental.CarRental.Infrastructure.Users
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        private readonly ApplicationDbContext _context;
        public UserRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<User> GetByUsernameAsync(string username)
        {
            return await _context.Users.Include(u => u.Role).FirstOrDefaultAsync(u => u.Username == username);
        }

        public async Task<Role> GetRoleByNameAsync(string roleName)
        { 
            return await _context.Set<Role>().FirstOrDefaultAsync(r => r.Name == roleName);
        }
    }
}
