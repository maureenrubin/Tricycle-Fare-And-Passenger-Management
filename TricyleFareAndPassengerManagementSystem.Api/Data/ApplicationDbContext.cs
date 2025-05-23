using Microsoft.EntityFrameworkCore;
using TricyleFareAndPassengerManagementSystem.Api.Models;

namespace TricyleFareAndPassengerManagementSystem.Api.Data
{
    public class ApplicationDbContext : DbContext
    {
        #region Public Constructors

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        #endregion Public Constructors

        #region Properties

        public DbSet<User> Users { get; set; }

        #endregion Properties

        #region Protected Methods

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.HasIndex(e => e.Username).IsUnique();
                entity.HasIndex(e => e.Email).IsUnique();
            });
        }

        #endregion Protected Methods
    }
}