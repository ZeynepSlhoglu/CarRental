using FluentValidation;

namespace CarRental.CarRental.Application.Vehicles
{
    public class WorkTimeValidator : AbstractValidator<(string ActiveWorkTime, string MaintenanceTime)>
    {
        public WorkTimeValidator()
        {
            RuleFor(x => x.ActiveWorkTime).NotEmpty().WithMessage("Aktif çalışma süresi boş olamaz.")
                .Must(BeValidDouble).WithMessage("Geçerli bir sayı formatı girin.");
            RuleFor(x => x.MaintenanceTime).NotEmpty().WithMessage("Bakım süresi boş olamaz.")
                .Must(BeValidDouble).WithMessage("Geçerli bir sayı formatı girin.");
            RuleFor(x => x).Must(BeValidTotalHours).WithMessage("Aktif Çalışma Süresi ve Bakım Süresi toplamı 168 saati geçemez.");
        }

        private bool BeValidDouble(string value)
        {
            return double.TryParse(value, out _);
        }

        private bool BeValidTotalHours((string ActiveWorkTime, string MaintenanceTime) times)
        {
            if (!double.TryParse(times.ActiveWorkTime, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out double active) ||
                !double.TryParse(times.MaintenanceTime, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out double maintenance))
                return false;

            return active + maintenance <= 168;
        }
    }
}
