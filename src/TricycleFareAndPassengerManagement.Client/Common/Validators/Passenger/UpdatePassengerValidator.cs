using FluentValidation;
using TricycleFareAndPassengerManagement.Application.Features.Passengers.Commands;

namespace TricycleFareAndPassengerManagement.Client.Common.Validators.Passenger
{
    public class UpdatePassengerValidator : AbstractValidator<UpdatePassengerCommand>
    {
        #region Public Constructors

        public UpdatePassengerValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0).WithMessage("Valid ID is required");

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