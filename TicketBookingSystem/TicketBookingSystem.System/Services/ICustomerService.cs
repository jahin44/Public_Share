using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketBookingSystem.System.BusinessObjects;

namespace TicketBookingSystem.System.Services
{
    public interface ICustomerService
    {
        IList<Customer> GetAllCustomers();
        void BuyTicket(Customer customer, Ticket ticket);
        void CreateCustomer(Customer customer);
        (IList<Customer> records, int total, int totalDisplay) GetCustomers(int pageIndex, int pageSize,
            string searchText, string sortText);
        Customer GetCustomer(int id);
        void UpdateCustomer(Customer customer);
        void DeleteCustomer(int id);
    }
}
