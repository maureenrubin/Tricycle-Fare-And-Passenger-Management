using FluentValidation;
using TricycleFareAndPassengerManagement.Application.Features.Passengers.Commands;

namespace TricycleFareAndPassengerManagement.Client.Common.Validators.Passenger
{
    public class CreatePassengerValidator : AbstractValidator<CreatePassengerCommand>
    {
        #region Public Constructors

        public CreatePassengerValidator()
        {
            RuleFor(x => x.FullName)
                .NotEmpty().WithMessage("Full name is required")
                .Length(2, 100).WithMessage("Full name must be between 2 and 100 characters");

            RuleFor(x => x.PhoneNumber)
                .NotEmpty().WithMessage("Phone number is required")
                .Matches(@"^\+?[\d\s\-\(\)]+$").WithMessage("Invalid phone number format");
        }

        #endregion Public Constructors
    }
}