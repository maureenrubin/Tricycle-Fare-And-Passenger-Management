using MediatR;

namespace TricycleFareAndPassengerManagement.Application.Features.Passengers.Commands
{
    public class UpdatePassengerCommand : IRequest<bool>
    {
        #region Properties

        public int Id { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public bool IsActive { get; set; }

        #endregion Properties
    }
}