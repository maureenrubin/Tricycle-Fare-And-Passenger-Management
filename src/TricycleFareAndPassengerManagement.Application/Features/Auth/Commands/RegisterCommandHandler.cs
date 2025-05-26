using MediatR;
using TricycleFareAndPassengerManagement.Application.Common.Interfaces;
using TricycleFareAndPassengerManagement.Application.Common.Models;

namespace TricycleFareAndPassengerManagement.Application.Features.Auth.Commands
{
    public class RegisterCommandHandler(IAuthService authService) : IRequestHandler<RegisterCommand, AuthResult>
    {
        #region Public Methods

        public async Task<AuthResult> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            return await authService.RegisterAsync(
                request.FullName,
                request.Email,
                request.Password);
        }

        #endregion Public Methods
    }
}