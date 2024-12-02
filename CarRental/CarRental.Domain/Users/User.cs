using CarRental.CarRental.Domain.Roles;

namespace CarRental.CarRental.Domain.Users
{
    public class User
    {
        public Guid Id { get; set; } 
        public string Username { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty; 
        public Role Role { get; set; } = new Role();
        public Guid RoleId { get; set; }
    }
}
