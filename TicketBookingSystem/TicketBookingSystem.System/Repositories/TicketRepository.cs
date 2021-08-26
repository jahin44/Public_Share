using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketBookingSystem.Data;
using TicketBookingSystem.System.Contexts;
using TicketBookingSystem.System.Entities;

namespace TicketBookingSystem.System.Repositories
{
    class TicketRepository : Repository<Ticket, int>,
        ITicketRepository
    {
        public TicketRepository(ISystemDbContext context)
            : base((DbContext)context)
        {
        }
    }
 }
