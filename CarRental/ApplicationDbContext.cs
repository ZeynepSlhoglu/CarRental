
using CarRental.CarRental.Domain.Roles;
using CarRental.CarRental.Domain.Users;
using CarRental.CarRental.Domain.Vehicles;
using Microsoft.EntityFrameworkCore;

namespace CarRental
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Vehicle> Vehicles { get; set; }
         
        public DbSet<User> Users { get; set; } 

        public DbSet<Role> Roles { get; set; }
       
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>()
             .HasOne(u => u.Role) 
             .WithMany(r => r.Users) 
             .HasForeignKey(u => u.RoleId)  
             .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
