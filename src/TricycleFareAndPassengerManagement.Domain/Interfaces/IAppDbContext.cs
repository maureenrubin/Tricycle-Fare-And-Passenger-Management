using Microsoft.EntityFrameworkCore;
using TricycleFareAndPassengerManagement.Domain.Entities;

namespace TricycleFareAndPassengerManagement.Domain.Interfaces
{
    public interface IAppDbContext
    {
        #region Properties

        DbSet<User> Users { get; }
        DbSet<UserRole> UserRoles { get; }
        DbSet<Role> Roles { get; }
        DbSet<Driver> Drivers { get; }
        DbSet<Passenger> Passengers { get; }
        DbSet<Trip> Trips { get; }

        #endregion Properties

        #region Public Methods

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

        #endregion Public Methods
    }
}