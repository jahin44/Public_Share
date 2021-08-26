using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TicketBookingSystem.System.Exceptions;
using System.Threading.Tasks;
using TicketBookingSystem.Common.Utilities;
using TicketBookingSystem.System.BusinessObjects;
using TicketBookingSystem.System.UnitOfWorks;

namespace TicketBookingSystem.System.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ISystemUnitOfWork _systemUnitOfWork;
        private readonly IDateTimeUtility _dateTimeUtility;

        public CustomerService(ISystemUnitOfWork systemUnitOfWork,
            IDateTimeUtility dateTimeUtility)
        {
            _systemUnitOfWork = systemUnitOfWork;
            _dateTimeUtility = dateTimeUtility;
        }

        public IList<Customer> GetAllCustomers()
        {
            var customerEntities = _systemUnitOfWork.Customers.GetAll();
            var customers = new List<Customer>();

            foreach (var entity in customerEntities)
            {
                var customer = new Customer()
                {
                    Id = entity.Id,
                    Name = entity.Name,
                    Age = entity.Age,
                    Address = entity.Address
                };

                customers.Add(customer);
            }

            return customers;
        }

        public void CreateCustomer(Customer customer)
        {
            if (customer == null)
                throw new InvalidParameterException("Customer was not provided");

            if (IsTitleAlreadyUsed(customer.Name))
                throw new DuplicateTitleException("Customer Name already exists");

            /*if (!IsValidStartDate(customer.Age))
                throw new InvalidOperationException("Start date should be atleast 30 days ahead");
            */
            _systemUnitOfWork.Customers.Add(
                new Entities.Customer
                {
                    Name = customer.Name,
                    Age = customer.Age,
                    Address = customer.Address
                }
            );

            _systemUnitOfWork.Save();
        }

        public void BuyTicket(Customer customer, Ticket ticket)
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
                    CustomerId = ticket.CustomerId,
                    Destination = ticket.Destination,
                    TicketFee = ticket.TicketFee 
                }
            });

            _systemUnitOfWork.Save();
        }

        private bool IsTitleAlreadyUsed(string name) =>
            _systemUnitOfWork.Customers.GetCount(x => x.Name == name) > 0;

        private bool IsTitleAlreadyUsed(string name, int id) =>
            _systemUnitOfWork.Customers.GetCount(x => x.Name == name && x.Id != id) > 0;

       /* private bool IsValidStartDate(DateTime startDate) =>
            startDate.Subtract(_dateTimeUtility.Now).TotalDays > 30;*/

        public (IList<Customer> records, int total, int totalDisplay) GetCustomers(int pageIndex, int pageSize,
            string searchText, string sortText)
        {
            var customerData = _systemUnitOfWork.Customers.GetDynamic(
                string.IsNullOrWhiteSpace(searchText) ? null : x => x.Name.Contains(searchText),
                sortText, string.Empty, pageIndex, pageSize);

            var resultData = (from customer in customerData.data
                              select new Customer
                              {
                                  Id = customer.Id,
                                  Name = customer.Name,
                                  Age = customer.Age,
                                  Address = customer.Address
                              }).ToList();

            return (resultData, customerData.total, customerData.totalDisplay);
        }

        public Customer GetCustomer(int id)
        {
            var customer = _systemUnitOfWork.Customers.GetById(id);

            if (customer == null) return null;

            return new Customer
            {
                Id = customer.Id,
                Name = customer.Name,
                Age = customer.Age,
                Address = customer.Address
            };
        }

        public void UpdateCustomer(Customer customer)
        {
            if (customer == null)
                throw new InvalidOperationException("Customer is missing");

            if (IsTitleAlreadyUsed(customer.Name, customer.Id))
                throw new DuplicateTitleException("Customer title already used in other customer.");

            var customerEntity = _systemUnitOfWork.Customers.GetById(customer.Id);

            if (customerEntity != null)
            {
                customerEntity.Name = customer.Name;
                customerEntity.Age = customer.Age;
                customerEntity.Address = customer.Address;

                _systemUnitOfWork.Save();
            }
            else
                throw new InvalidOperationException("Couldn't find customer");
        }

        public void DeleteCustomer(int id)
        {
            _systemUnitOfWork.Customers.Remove(id);
            _systemUnitOfWork.Save();
        }
    }
}
