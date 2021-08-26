using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketBookingSystem.Common.Utilities;
using TicketBookingSystem.System.BusinessObjects;
using TicketBookingSystem.System.Exceptions;
using TicketBookingSystem.System.UnitOfWorks;

namespace TicketBookingSystem.System.Services
{
    public class TicketService : ITicketService
    {
        private readonly ISystemUnitOfWork _systemUnitOfWork;
        private readonly IDateTimeUtility _dateTimeUtility;

        public TicketService(ISystemUnitOfWork systemUnitOfWork,
            IDateTimeUtility dateTimeUtility)
        {
            _systemUnitOfWork = systemUnitOfWork;
            _dateTimeUtility = dateTimeUtility;
        }

        public IList<Ticket> GetAllTickets()
        {
            var ticketEntities = _systemUnitOfWork.Tickets.GetAll();
            var tickets = new List<Ticket>();

            foreach (var entity in ticketEntities)
            {
                var ticket = new Ticket()
                {
                    Id = entity.Id,
                    CustomerId = entity.CustomerId,
                    Destination = entity.Destination,
                    TicketFee = entity.TicketFee
                };

                tickets.Add(ticket);
            }

            return tickets;
        }

        public void CreateTicket(Ticket ticket)
        {
            if (ticket == null)
                throw new InvalidParameterException("Ticket was not provided");

            

             

            _systemUnitOfWork.Tickets.Add(
                new Entities.Ticket
                {
                    CustomerId = ticket.CustomerId,
                    Destination = ticket.Destination,
                    TicketFee = ticket.TicketFee
                }
            );

            _systemUnitOfWork.Save();
        }

        public void Buyer(Customer customer, Ticket ticket)
        {
            var customerEntity = _systemUnitOfWork.Customers.GetById(customer.Id);

            if (customerEntity == null)
                throw new InvalidOperationException("Customer was not found");

            if (customerEntity.BuyTickets == null)
                customerEntity.BuyTickets = new List<Entities.CustomerTicket>();

            customerEntity.BuyTickets.Add(new Entities.CustomerTicket
            {
                Ticket = new Entities.Ticket
                {
                    Destination = ticket.Destination,
                    TicketFee = ticket.TicketFee
                }
            });

            _systemUnitOfWork.Save();
        }

      
 

        public (IList<Ticket> records, int total, int totalDisplay) GetTickets(int pageIndex, int pageSize,
            string searchText, string sortText)
        {
            var ticketData = _systemUnitOfWork.Tickets.GetDynamic(
                string.IsNullOrWhiteSpace(searchText) ? null : x => x.Destination.Contains(searchText),
                sortText, string.Empty, pageIndex, pageSize);

            var resultData = (from ticket in ticketData.data
                              select new Ticket
                              {
                                  Id = ticket.Id,
                                  CustomerId = ticket.CustomerId,
                                  Destination = ticket.Destination,
                                  TicketFee = ticket.TicketFee
                              }).ToList();

            return (resultData, ticketData.total, ticketData.totalDisplay);
        }

        public Ticket GetTicket(int id)
        {
            var ticket = _systemUnitOfWork.Tickets.GetById(id);

            if (ticket == null) return null;

            return new Ticket
            {
                Id = ticket.Id,
                CustomerId = ticket.CustomerId,
                Destination = ticket.Destination,
                TicketFee = ticket.TicketFee
            };
        }

        public void UpdateTicket(Ticket ticket)
        {
            if (ticket == null)
                throw new InvalidOperationException("Ticket is missing");
           

            var ticketEntity = _systemUnitOfWork.Tickets.GetById(ticket.Id);

            if (ticketEntity != null)
            {
                ticketEntity.CustomerId = ticket.CustomerId;
                ticketEntity.Destination = ticket.Destination;
                ticketEntity.TicketFee = ticket.TicketFee;

                _systemUnitOfWork.Save();
            }
            else
                throw new InvalidOperationException("Couldn't find ticket");
        }

        public void DeleteTicket(int id)
        {
            _systemUnitOfWork.Tickets.Remove(id);
            _systemUnitOfWork.Save();
        }
    }
}
