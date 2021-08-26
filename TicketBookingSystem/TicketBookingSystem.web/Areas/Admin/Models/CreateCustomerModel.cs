using Autofac;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using TicketBookingSystem.System.BusinessObjects;
using TicketBookingSystem.System.Services;

namespace TicketBookingSystem.web.Areas.Admin.Models
{
    public class CreateCustomerModel
    {
        [Required, MaxLength(200, ErrorMessage = "Name should be less than 200 charcaters")]
        public string Name { get; set; }
        [Required, Range(1, 50000)]
        public int Age { get; set; }
        [Required, MaxLength(200, ErrorMessage = "Address should be less than 200 charcaters")]
        public string Address { get; set; }

        private readonly ICustomerService _customerService;
        private readonly IMapper _mapper;

        public CreateCustomerModel()
        {
            _customerService = Startup.AutofacContainer.Resolve<ICustomerService>();
            _mapper = Startup.AutofacContainer.Resolve<IMapper>();

        }

        public CreateCustomerModel(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        internal void CreateCustomer()
        {
            var customer = _mapper.Map<Customer>(this);           
            _customerService.CreateCustomer(customer);
        }
    }
}
