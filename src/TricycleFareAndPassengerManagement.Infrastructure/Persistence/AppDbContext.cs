using Microsoft.EntityFrameworkCore;
using TricycleFareAndPassengerManagement.Domain.Entities;
using TricycleFareAndPassengerManagement.Domain.Interfaces;

namespace TricycleFareAndPassengerManagement.Infrastructure.Data
{
    public class AppDbContext : DbContext, IAppDbContext
    {
        #region Public Constructors

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        #endregion Public Constructors

        #region Properties

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<Driver> Drivers => Set<Driver>();
        public DbSet<Passenger> Passengers => Set<Passenger>();
        public DbSet<Trip> Trips => Set<Trip>();

        #endregion Properties

        #region Protected Methods

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // User configuration
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.HasIndex(e => e.Email).IsUnique();
            });

            // Role configuration
            modelBuilder.Entity<Role>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.HasIndex(e => e.Name).IsUnique();
            });

            // UserRole configuration (Many-to-Many)
            modelBuilder.Entity<UserRole>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.RoleId });

                entity.HasOne(e => e.User)
                    .WithMany(e => e.UserRoles)
                    .HasForeignKey(e => e.UserId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(e => e.Role)
                    .WithMany(e => e.UserRoles)
                    .HasForeignKey(e => e.RoleId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            // Driver configuration
            modelBuilder.Entity<Driver>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.FullName).HasMaxLength(50).IsRequired();
                entity.Property(e => e.LicenseNumber).HasMaxLength(20).IsRequired();
                entity.Property(e => e.PhoneNumber).HasMaxLength(15);
                entity.Property(e => e.TricycleNumber).HasMaxLength(10).IsRequired();
            });

            // Passenger configuration
            modelBuilder.Entity<Passenger>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.FullName).HasMaxLength(50).IsRequired();
                entity.Property(e => e.PhoneNumber).HasMaxLength(15);
            });

            // Trip configuration
            modelBuilder.Entity<Trip>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.PickupLocation).HasMaxLength(200).IsRequired();
                entity.Property(e => e.DropoffLocation).HasMaxLength(200).IsRequired();
                entity.Property(e => e.BaseFare).HasColumnType("decimal(10,2)");
                entity.Property(e => e.TotalFare).HasColumnType("decimal(10,2)");

                // Relationships
                entity.HasOne(e => e.Driver)
                      .WithMany(d => d.Trips)
                      .HasForeignKey(e => e.DriverId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(e => e.Passenger)
                      .WithMany(p => p.Trips)
                      .HasForeignKey(e => e.PassengerId)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            base.OnModelCreating(modelBuilder);
        }

        #endregion Protected Methods
    }
}