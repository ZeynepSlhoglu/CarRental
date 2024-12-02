using CarRental.CarRental.Domain.Roles;
using CarRental.CarRental.Infrastructure.BaseRepository;
using Microsoft.EntityFrameworkCore;

namespace CarRental.CarRental.Infrastructure.Roles
{
    public class RoleRepository : BaseRepository<Role>, IRoleRepository
    {
        private readonly ApplicationDbContext _context;
        public RoleRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Role?> GetByNameAsync(string name)
        {
            return await _context.Roles.FirstOrDefaultAsync(r => r.Name == name);
        }

      
    }
}
