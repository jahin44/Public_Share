using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketBookingSystem.System.BusinessObjects;

namespace TicketBookingSystem.System.Services
{
    public interface ITicketService
    {
        IList<Ticket> GetAllTickets();
        void Buyer(Customer customer ,Ticket ticket);
        void CreateTicket(Ticket ticket);
        (IList<Ticket> records, int total, int totalDisplay) GetTickets(int pageIndex, int pageSize,
            string searchText, string sortText);
        Ticket GetTicket(int id);
        void UpdateTicket(Ticket ticket);
        void DeleteTicket(int id);

    }
}
