using CaseMangementSystem.Contexts;
using CaseMangementSystem.Models.Entities;


namespace CaseMangementSystem.Services
{
    internal class TicketStatusService: GenericService<TicketStatusEntity>
    { 
        private readonly DataContext _context = new();

        public async Task LoadTicketStatusesAsync()
        {
            var CreatedTicketStatus = new List<TicketStatusEntity>()
            {
                new TicketStatusEntity {Id = 1, StatusName = "Not Started"},
                new TicketStatusEntity {Id = 2, StatusName = "Started"},
                new TicketStatusEntity {Id = 3, StatusName = "Completed"}

            };
           

            await _context.AddRangeAsync(CreatedTicketStatus);
            await _context.SaveChangesAsync();
        }

    }
}
