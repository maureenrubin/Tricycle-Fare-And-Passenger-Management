using FluentValidation;
using TricycleFareAndPassengerManagement.Application.Features.Driver.Commands;

namespace TricycleFareAndPassengerManagement.Client.Common.Validators.Driver
{
    public class CreateDriverValidator : AbstractValidator<CreateDriverCommand>
    {
        #region Public Constructors

        public CreateDriverValidator()
        {
            RuleFor(x => x.FullName)
                .NotEmpty().WithMessage("Full name is required")
                .Length(2, 100).WithMessage("Full name must be between 2 and 100 characters");

            RuleFor(x => x.LicenseNumber)
                .NotEmpty().WithMessage("License number is required")
                .Length(5, 20).WithMessage("License number must be between 5 and 20 characters");

            RuleFor(x => x.PhoneNumber)
                .NotEmpty().WithMessage("Phone number is required")
                .Matches(@"^\+?[\d\s\-\(\)]+$").WithMessage("Invalid phone number format");

            RuleFor(x => x.TricycleNumber)
                .NotEmpty().WithMessage("Tricycle number is required")
                .Length(3, 15).WithMessage("Tricycle number must be between 3 and 15 characters");
        }

        #endregion Public Constructors
    }
}