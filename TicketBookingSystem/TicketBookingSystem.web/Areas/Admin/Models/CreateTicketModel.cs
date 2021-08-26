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
    public class CreateTicketModel
    {
        [Required, Range(1, 50000)]
        public int CustomerId { get; set; }

        [Required, MaxLength(2000, ErrorMessage = "Destination should be less than 200 charcaters")]
        public string Destination { get; set; }

        [Required, Range(1, 500)] 
        public int TicketFee { get; set; }

        private readonly ITicketService _ticketService;
        private readonly IMapper _mapper;

        public CreateTicketModel()
        {
            _ticketService = Startup.AutofacContainer.Resolve<ITicketService>();
            _mapper = Startup.AutofacContainer.Resolve<IMapper>();

        }

        public CreateTicketModel(ITicketService ticketService)
        {
            _ticketService = ticketService;
        }

        internal void CreateTicket()
        {
            var ticket = _mapper.Map<Ticket>(this);
             

            _ticketService.CreateTicket(ticket);
        }
    }
}
