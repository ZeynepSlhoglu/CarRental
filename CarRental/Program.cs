using CarRental;
using CarRental.CarRental.Application.Auth;
using CarRental.CarRental.Application.Vehicles;
using CarRental.CarRental.Domain.Roles;
using CarRental.CarRental.Domain.Users;
using CarRental.CarRental.Domain.Vehicles;
using CarRental.CarRental.Infrastructure;
using CarRental.CarRental.Infrastructure.Roles;
using CarRental.CarRental.Infrastructure.Users;
using CarRental.CarRental.Infrastructure.Vehicles;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

#region AddScoped

builder.Services.AddScoped<IUserRepository, UserRepository>(); 
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IRoleRepository, RoleRepository>();
builder.Services.AddScoped<IVehicleService, VehicleService>();
builder.Services.AddScoped<IVehicleRepository, VehicleRepository>(); 
builder.Services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();
builder.Services.AddScoped<IValidator<User>, UserValidator>();


builder.Services.AddScoped<IValidator<(string ActiveWorkTime, string MaintenanceTime)>, WorkTimeValidator>();

builder.Services.AddScoped<IValidator<Vehicle>, VehicleValidator>(); 

builder.Services.AddScoped<DataSeeder>();

builder.Services.AddHttpContextAccessor();

#endregion

#region DB 

string? connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

if (string.IsNullOrEmpty(connectionString))
{
    throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
}
 
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(connectionString));

#endregion

#region Cookies

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Auth/Login";
        options.LogoutPath = "/Auth/Logout";
    });

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminOnly", policy => policy.RequireRole("Admin"));
});


#endregion

var app = builder.Build();

#region Seed

using (var scope = app.Services.CreateScope())
{
    try
    {
        var seeder = scope.ServiceProvider.GetRequiredService<DataSeeder>();
        await seeder.SeedAsync();
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Seed iþlemi sýrasýnda bir hata oluþtu: {ex.Message}");
        throw;
    }
}

#endregion


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // Varsayýlan HSTS deðeri 30 gündür.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();



app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
