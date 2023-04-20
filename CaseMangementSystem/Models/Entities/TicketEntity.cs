using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaseMangementSystem.Models.Entities
{
    internal class TicketEntity
    {
        public Guid Id { get; set; } = new Guid();
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TicketId { get; set; }
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public DateTime WhenCreated { get; set; } = DateTime.Now;
        public DateTime WhenChanged { get; set; }
        public Guid CustomerId { get; set; }
        public int TicketStatusId { get; set; }
        public CustomerEntity Customer { get; set; } = null!;
        public TicketStatusEntity TicketStatus { get; set; } = null!;
        public ICollection<CommentEntity> Comments { get; set; } = new HashSet<CommentEntity>();

    }
}
 

    

