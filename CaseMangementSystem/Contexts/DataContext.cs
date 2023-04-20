using CaseMangementSystem.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace CaseMangementSystem.Contexts
{
    internal class DataContext : DbContext
    {
        

        public DataContext()
        {
        }
        public DataContext(DbContextOptions options) : base(options)
        {
        }



        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=HYPERION\SQLEXPRESS01;Initial Catalog=CMSDB;Integrated Security=True;Encrypt=False");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<CustomerEntity> Customers { get; set; }
        public DbSet<TicketEntity> Tickets { get; set; }
        public DbSet<TicketStatusEntity> TicketStatuses { get; set; }
        public DbSet<CommentEntity> Comments { get; set; }
    }
}
