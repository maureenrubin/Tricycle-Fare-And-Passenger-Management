using MediatR;

namespace TricycleFareAndPassengerManagement.Application.Features.Driver.Commands
{
    public class UpdateDriverCommand : IRequest<bool>
    {
        #region Properties

        public int Id { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string LicenseNumber { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string TricycleNumber { get; set; } = string.Empty;
        public bool IsActive { get; set; }

        #endregion Properties
    }
}