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
    public class EditTicketModel
    {
        [Required, Range(1, 50000)]
        public int? Id { get; set; }
        [Required, Range(1, 50000)]
        public int? CustomerId { get; set; }

        [Required, MaxLength(200, ErrorMessage = "Destination should be less than 200 charcaters")]
        public string Destination { get; set; }

        [Required, Range(1, 50000)]
        public int? TicketFee { get; set; }

        private readonly ITicketService _ticketService;
        private readonly IMapper _mapper;

        public EditTicketModel()
        {
            _ticketService = Startup.AutofacContainer.Resolve<ITicketService>();
            _mapper = Startup.AutofacContainer.Resolve<IMapper>();

        }

        public void LoadModelData(int id)
        {
            var ticket = _ticketService.GetTicket(id);
            _mapper.Map(ticket, this);
        }

        internal void Update()
        {
            var ticket = _mapper.Map<Ticket>(this);
            _ticketService.UpdateTicket(ticket);
        }
    }
}
