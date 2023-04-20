using CaseMangementSystem.Contexts;
using CaseMangementSystem.Models;
using CaseMangementSystem.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaseMangementSystem.Services
{
    
    internal class CustomerService : GenericService<CustomerEntity>
    {
        private readonly DataContext _context = new();

        public override async Task<IEnumerable<CustomerEntity>> GetAllAsync()
        {
            return await _context.Customers.Include(x => x.Tickets).ThenInclude(x => x.Comments).Include(x => x.Tickets).ThenInclude(x => x.TicketStatus).ToListAsync();
        }
        public async Task<CustomerEntity> GetAsync(string email)
        {

            return await _context.Customers.Include(x => x.Tickets).ThenInclude(x => x.Comments).Include(x => x.Tickets).ThenInclude(x => x.TicketStatus).FirstOrDefaultAsync(x => x.Email.Equals(email)) ?? null!;

        }

        public async Task<CustomerEntity> CreateCustomerAsync(Customer customer)
        {
            var CreatedCustomer  = new CustomerEntity()
            {
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                Email = customer.Email,
                PhoneNumber = customer.PhoneNumber,
            };

            await _context.Customers.AddAsync(CreatedCustomer);
            await _context.SaveChangesAsync();

            return CreatedCustomer;
        }

       
    }
}
