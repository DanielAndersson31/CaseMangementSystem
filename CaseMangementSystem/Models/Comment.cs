using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaseMangementSystem.Models
{
    internal class Comment
    {
        public Comment(string comment, Guid ticketId) 
        {
            CommentText= comment;
            TicketId = ticketId;
        }

        public string CommentText { get; set; }
        public Guid TicketId { get; set; }
    }
}
