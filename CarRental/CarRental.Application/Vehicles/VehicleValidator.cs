using CarRental.CarRental.Domain.Vehicles;
using FluentValidation;

namespace CarRental.CarRental.Application.Vehicles
{
    public class VehicleValidator : AbstractValidator<Vehicle>
    {
        public VehicleValidator()
        {
            RuleFor(v => v.Name).NotEmpty().WithMessage("Araç adı boş olamaz.");
            RuleFor(v => v.Plate).NotEmpty().WithMessage("Araç plakası boş olamaz.");
        }
    }
     
}
