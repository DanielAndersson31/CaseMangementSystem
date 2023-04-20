using CaseMangementSystem.Contexts;
using CaseMangementSystem.Models;
using CaseMangementSystem.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace CaseMangementSystem.Services
{
    
    internal class TicketService: GenericService<TicketEntity>
    {
        private readonly DataContext _context = new();

        public override async Task<IEnumerable<TicketEntity>> GetAllAsync()
        {
            return await _context.Tickets.Include(x => x.TicketStatus).Include(x => x.Comments).Include(x => x.Customer).ToListAsync();
        }
        
      
       public async Task<TicketEntity> GetTicketByIdAsync(int ticketId)
        {
            return await _context.Tickets
                .Include(x => x.TicketStatus)
                .Include(x => x.Comments)
                .Include(x => x.Customer)
                .FirstOrDefaultAsync(x => x.TicketId == ticketId) ?? null!;
        }
       
        public async Task<TicketEntity> CreateTicketAsync(Ticket ticket)
        {
            var CreatedTicket = new TicketEntity()
            {
                Title = ticket.Title,
                Description = ticket.Description,
                CustomerId = ticket.CustomerId,
                TicketStatusId= ticket.TicketStatusId,
            };

            await _context.Tickets.AddAsync(CreatedTicket);
            await _context.SaveChangesAsync();

            return CreatedTicket;
        }

        public async Task<TicketEntity> UpdateTicketStatus(int statusId, int ticketId)
        {
            var updateTicket = await _context.Tickets.FirstOrDefaultAsync(x => x.TicketId == ticketId);
             
            if(updateTicket != null)
            {
                updateTicket.TicketStatusId = statusId;
                await _context.SaveChangesAsync();
                return updateTicket;
            }

            return null!;
           
            
        }

    }
}
