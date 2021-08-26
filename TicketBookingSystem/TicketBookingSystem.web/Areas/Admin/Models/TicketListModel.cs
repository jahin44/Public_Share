using Autofac;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicketBookingSystem.System.Services;
using TicketBookingSystem.web.Models;

namespace TicketBookingSystem.web.Areas.Admin.Models
{
    public class TicketListModel
    {
        private ITicketService _ticketService;
        private IHttpContextAccessor _httpContextAccessor;

        public TicketListModel()
        {
            _ticketService = Startup.AutofacContainer.Resolve<ITicketService>();
            _httpContextAccessor = Startup.AutofacContainer.Resolve<IHttpContextAccessor>();
        }

        public TicketListModel(ITicketService ticketService, IHttpContextAccessor httpContextAccessor)
        {
            _ticketService = ticketService;
            _httpContextAccessor = httpContextAccessor;
        }

        internal object GetTickets(DataTablesAjaxRequestModel tableModel)
        {
            var data = _ticketService.GetTickets(
                tableModel.PageIndex,
                tableModel.PageSize,
                tableModel.SearchText,
                tableModel.GetSortText(new string[] { "CustomerId", "Destination", "TicketFee" }));

            return new
            {
                recordsTotal = data.total,
                recordsFiltered = data.totalDisplay,
                data = (from record in data.records
                        select new string[]
                        {
                                record.CustomerId.ToString(),
                                record.Destination,
                                record.TicketFee.ToString(),
                                record.Id.ToString()
                        }
                    ).ToArray()
            };
        }

        internal void Delete(int id)
        {
            _ticketService.DeleteTicket(id);
        }
    }
}
