using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketBookingSystem.Data;
using TicketBookingSystem.System.Contexts;
using TicketBookingSystem.System.Repositories;

namespace TicketBookingSystem.System.UnitOfWorks
{
    public class SystemUnitOfWork : UnitOfWork, ISystemUnitOfWork
    {
        public ITicketRepository Tickets { get; private set; }
        public ICustomerRepository Customers { get; private set; }

        public SystemUnitOfWork(ISystemDbContext context,
            ITicketRepository tickets,
            ICustomerRepository customers
            ) : base((DbContext)context)
        {
            Tickets = tickets;
            Customers = customers;
        }
    }
}
