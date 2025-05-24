using TricycleFareAndPassengerManagement.Domain.Entities;

namespace TricycleFareAndPassengerManagement.Domain.Interfaces
{
    public interface IRoleRepository
    {
        #region Public Methods

        Task<Role?> GetByIdAsync(Guid id);

        Task<Role?> GetByNameAsync(string name);

        Task<Role> CreateAsync(Role role);

        Task<IEnumerable<Role>> GetAllAsync();

        Task<IEnumerable<Role>> GetUserRolesAsync(Guid userId);

        Task AssignRoleToUserAsync(Guid userId, Guid roleId);

        Task RemoveRoleFromUserAsync(Guid userId, Guid roleId);

        #endregion Public Methods
    }
}