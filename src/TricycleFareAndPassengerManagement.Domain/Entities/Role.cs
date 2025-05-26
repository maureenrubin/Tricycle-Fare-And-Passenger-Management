using System.ComponentModel.DataAnnotations;

namespace TricycleFareAndPassengerManagement.Domain.Entities
{
    public class Role
    {
        #region Properties

        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        [MaxLength(50)]
        public string Name { get; set; } = string.Empty;

        // Navigation properties
        public ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();

        #endregion Properties
    }
}