using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaseMangementSystem.Models.Entities
{
    internal class CommentEntity
    {
        public Guid Id { get; set; } = new Guid();
        public string CommentText { get; set; } = null!;
        public DateTime WhenCreated { get; set; }
        public Guid TicketId { get; set; }

        public TicketEntity Ticket { get; set; } = null!;

    }
}
