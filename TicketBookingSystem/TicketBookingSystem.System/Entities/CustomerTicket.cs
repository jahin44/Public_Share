using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketBookingSystem.System.Entities
{
    public class CustomerTicket
    {
        
        public Customer Customer { get; set; }
        
        public int CustomerId { get; set; }
       
        public Ticket Ticket { get; set; }
        public int TicketId { get; set; }
    }
}
