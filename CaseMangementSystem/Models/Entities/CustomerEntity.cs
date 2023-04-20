using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace CaseMangementSystem.Models.Entities
{
    [Index(nameof(Email), IsUnique = true)]
    internal class CustomerEntity
    {

        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        [Required]
        [StringLength(50)]
        public string FirstName { get; set; } = null!;
        [Required]
        [StringLength(50)]
        public string LastName { get; set; } = null!;
        [Required]
        [StringLength(100)]
        public string Email { get; set; } = null!;
        [Required]
        [Column(TypeName ="CHAR(13)")]
        public string PhoneNumber { get; set; } = null!;
        public ICollection<TicketEntity> Tickets { get; set; } = new HashSet<TicketEntity>();

    }
}
