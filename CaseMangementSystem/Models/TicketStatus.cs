using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaseMangementSystem.Models
{
    internal class TicketStatus
    {
            public TicketStatus(int id, string statusName)
            {
                Id = id;
                StatusName = statusName;
            }

        public int Id { get; set; }
        public string StatusName { get; set; }
        
    }
}
