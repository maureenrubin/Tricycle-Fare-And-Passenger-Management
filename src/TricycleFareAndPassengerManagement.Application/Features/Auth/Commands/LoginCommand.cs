using MediatR;
using TricycleFareAndPassengerManagement.Application.Common.Models;

namespace TricycleFareAndPassengerManagement.Application.Features.Auth.Commands
{
    public class LoginCommand : IRequest<AuthResult>
    {
        #region Properties

        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;

        #endregion Properties
    }
}