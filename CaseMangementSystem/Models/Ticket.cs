using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaseMangementSystem.Models
{
    internal class Ticket
    {
       public  Ticket(string title, string description, Guid customerId , int ticketStatusId) 
        {
            Title= title;
            Description= description;
            CustomerId = customerId;
            TicketStatusId = ticketStatusId;
        }

        public string Title { get; set; }   
        public string Description { get; set; }
        public Guid CustomerId { get; set; }
        public int TicketStatusId { get; set; }
    }
}
