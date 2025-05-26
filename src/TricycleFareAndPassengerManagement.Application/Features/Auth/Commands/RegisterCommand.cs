using MediatR;
using TricycleFareAndPassengerManagement.Application.Common.Models;

namespace TricycleFareAndPassengerManagement.Application.Features.Auth.Commands
{
    public class RegisterCommand : IRequest<AuthResult>
    {
        #region Properties

        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;

        #endregion Properties
    }
}