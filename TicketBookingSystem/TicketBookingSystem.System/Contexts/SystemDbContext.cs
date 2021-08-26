using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketBookingSystem.System.Entities;

namespace TicketBookingSystem.System.Contexts
{
    public class SystemDbContext : DbContext, ISystemDbContext
    {
        
        private readonly string _connectionString;
        private readonly string _migrationAssemblyName;

        public SystemDbContext(string connectionString, string migrationAssemblyName)
        {
            _connectionString = connectionString;
            _migrationAssemblyName = migrationAssemblyName;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder dbContextOptionsBuilder)
        {
            if (!dbContextOptionsBuilder.IsConfigured)
            {
                dbContextOptionsBuilder.UseSqlServer(
                    _connectionString,
                    m => m.MigrationsAssembly(_migrationAssemblyName));
            }

            base.OnConfiguring(dbContextOptionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            // one to many relationship
            
            modelBuilder.Entity<CustomerTicket>()
                .HasKey(cs => new { cs.CustomerId, cs.TicketId });

            modelBuilder.Entity<CustomerTicket>()
                .HasOne(cs => cs.Customer)
                .WithMany(c => c.BuyTickets)
                .HasForeignKey(cs => cs.CustomerId);

            modelBuilder.Entity<CustomerTicket>()
                .HasOne(cs => cs.Ticket)
                .WithMany(s => s.Buyer)
                .HasForeignKey(cs => cs.TicketId);

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Ticket> Tickets { get; set; }

    }
}
