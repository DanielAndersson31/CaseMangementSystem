using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaseMangementSystem.Models.Entities
{
    internal class TicketStatusEntity
    {
        [Required, Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }
        public string StatusName { get; set; } = null!; 

        public ICollection<TicketEntity> Tickets { get; set; } = new HashSet<TicketEntity>();
    }
}
