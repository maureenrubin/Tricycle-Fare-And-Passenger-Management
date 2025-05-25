using MediatR;

namespace TricycleFareAndPassengerManagement.Application.Features.Passengers.Commands
{
    public class CreatePassengerCommand : IRequest<int>
    {
        #region Properties

        public string FullName { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;

        #endregion Properties
    }
}