using MediatR;
using TricycleFareAndPassengerManagement.Application.Common.Interfaces;
using TricycleFareAndPassengerManagement.Application.Common.Models;

namespace TricycleFareAndPassengerManagement.Application.Features.Auth.Commands
{
    public class LoginCommandHandler(IAuthService authService) : IRequestHandler<LoginCommand, AuthResult>
    {
        #region Public Methods

        public async Task<AuthResult> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            return await authService.LoginAsync(request.Email, request.Password);
        }

        #endregion Public Methods
    }
}