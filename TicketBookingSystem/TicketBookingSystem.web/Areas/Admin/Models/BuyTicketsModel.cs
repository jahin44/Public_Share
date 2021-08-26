using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicketBookingSystem.System.BusinessObjects;
using TicketBookingSystem.System.Services;

namespace TicketBookingSystem.web.Areas.Admin.Models
{
    public class BuyTicketsModel
    {
        public int TicketId { get; set; }
        public string CustomerName { get; set; }

        private readonly ICustomerService _customerService;
        public BuyTicketsModel()
        {
            _customerService = Startup.AutofacContainer.Resolve<ICustomerService>();
        }

        public BuyTicketsModel(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        public void BuyTicket()
        {
            var customers = _customerService.GetAllCustomers();

            var selectedCustomer = customers.Where(x => x.Name == CustomerName).FirstOrDefault();

            var ticket = new Ticket
            {
                Id = TicketId,
                Destination = "Jahin",
            };

            _customerService.BuyTicket(selectedCustomer, ticket);
        }
    }
}
