using CarRental.CarRental.Domain.Roles;
using CarRental.CarRental.Infrastructure.Roles;

namespace CarRental.CarRental.Application.Roles
{
    public class RoleService : IRoleService
    {
        private readonly RoleRepository _roleRepository;

        public RoleService(RoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }

        public async Task<IEnumerable<Role>> GetAllRolesAsync()
        {
            return await _roleRepository.GetAllAsync();
        }

        public async Task<Role> GetRoleByIdAsync(Guid id)
        {
            return await _roleRepository.GetByIdAsync(id);
        }

        public async Task AddRoleAsync(Role role)
        {
            await _roleRepository.AddAsync(role);
        }

        public async Task DeleteRoleAsync(Guid id)
        {
            await _roleRepository.DeleteAsync(id);
        }
    }
}
