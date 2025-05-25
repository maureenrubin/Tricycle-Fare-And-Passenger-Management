using MediatR;
using TricycleFareAndPassengerManagement.Application.Common.DTOs;

namespace TricycleFareAndPassengerManagement.Application.Features.Auth.Queries
{
    public class GetCurrentUserQuery : IRequest<UserDto?>
    {
        #region Properties

        public Guid UserId { get; set; }

        #endregion Properties
    }
}