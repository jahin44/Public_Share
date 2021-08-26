using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketBookingSystem.Data;
using TicketBookingSystem.System.Entities;

namespace TicketBookingSystem.System.Repositories
{
    public interface ICustomerRepository : IRepository<Customer, int>
    {

    }
}
