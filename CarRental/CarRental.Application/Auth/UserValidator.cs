using CarRental.CarRental.Domain.Users; 
using FluentValidation;

namespace CarRental.CarRental.Application.Auth
{
    public class UserValidator : AbstractValidator<User>
    {
        private readonly IUserRepository _userRepository;

        public UserValidator(IUserRepository userRepository)
        {
            _userRepository = userRepository;

            RuleFor(x => x.Username)
              .NotEmpty().WithMessage("Kullanıcı adı boş olamaz.")
              .MustAsync(BeUniqueUsername).WithMessage("Bu kullanıcı adı zaten alınmış.");

        }

        private async Task<bool> BeUniqueUsername(string username, CancellationToken cancellationToken)
        {
            var existingUser = await _userRepository.GetByUsernameAsync(username);
            return existingUser == null;
        }
    }
}
