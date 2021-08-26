using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketBookingSystem.Data;
using TicketBookingSystem.System.Repositories;

namespace TicketBookingSystem.System.UnitOfWorks
{
    public interface ISystemUnitOfWork : IUnitOfWork
    {
        ITicketRepository Tickets { get; }
        ICustomerRepository Customers { get; }
    }
 
}
