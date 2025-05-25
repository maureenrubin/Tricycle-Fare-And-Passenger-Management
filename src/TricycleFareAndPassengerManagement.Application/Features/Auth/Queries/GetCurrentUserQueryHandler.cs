using MediatR;
using TricycleFareAndPassengerManagement.Application.Common.DTOs;
using TricycleFareAndPassengerManagement.Domain.Interfaces;

namespace TricycleFareAndPassengerManagement.Application.Features.Auth.Queries
{
    public class GetCurrentUserQueryHandler(IUserRepository userRepository,
        IRoleRepository roleRepository
        ) : IRequestHandler<GetCurrentUserQuery, UserDto?>
    {
        #region Public Methods

        public async Task<UserDto?> Handle(GetCurrentUserQuery request, CancellationToken cancellationToken)
        {
            var user = await userRepository.GetByIdAsync(request.UserId);
            if (user == null)
                return null;

            var roles = await roleRepository.GetUserRolesAsync(user.Id);

            return new UserDto
            {
                Id = user.Id,
                FullName = user.FullName,
                Email = user.Email,
                Roles = roles.Select(r => r.Name).ToList()
            };
        }

        #endregion Public Methods
    }
}