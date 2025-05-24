using FluentValidation;
using TricycleFareAndPassengerManagement.Application.Features.Trips.Commands;

namespace TricycleFareAndPassengerManagement.Client.Common.Validators.Trip
{
    public class CreateTripValidator : AbstractValidator<CreateTripCommand>
    {
        #region Public Constructors

        public CreateTripValidator()
        {
            RuleFor(x => x.DriverId)
                .GreaterThan(0).WithMessage("Please select a driver");

            RuleFor(x => x.PassengerId)
                .GreaterThan(0).WithMessage("Please select a passenger");

            RuleFor(x => x.PickupLocation)
                .NotEmpty().WithMessage("Pickup location is required")
                .Length(2, 200).WithMessage("Pickup location must be between 2 and 200 characters");

            RuleFor(x => x.DropoffLocation)
                .NotEmpty().WithMessage("Dropoff location is required")
                .Length(2, 200).WithMessage("Dropoff location must be between 2 and 200 characters");

            RuleFor(x => x.Distance)
                .GreaterThan(0.1).WithMessage("Distance must be greater than 0.1 km")
                .LessThanOrEqualTo(100).WithMessage("Distance cannot exceed 100 km");
        }

        #endregion Public Constructors
    }
}