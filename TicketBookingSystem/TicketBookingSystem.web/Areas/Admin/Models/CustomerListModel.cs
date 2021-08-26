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
    public class CustomerListModel
    {
        private ICustomerService _customerService;
        private IHttpContextAccessor _httpContextAccessor;
        public CustomerListModel()
        {
            _customerService = Startup.AutofacContainer.Resolve<ICustomerService>();
            _httpContextAccessor = Startup.AutofacContainer.Resolve<IHttpContextAccessor>();

        }

        public CustomerListModel(ICustomerService customerService, IHttpContextAccessor httpContextAccessor)
        {
            _customerService = customerService;
            _httpContextAccessor = httpContextAccessor;

        }

        internal object GetCustomers(DataTablesAjaxRequestModel tableModel)
        {
            var data = _customerService.GetCustomers(
                tableModel.PageIndex,
                tableModel.PageSize,
                tableModel.SearchText,
                tableModel.GetSortText(new string[] { "Name", "Age", "Address" }));

            return new
            {
                recordsTotal = data.total,
                recordsFiltered = data.totalDisplay,
                data = (from record in data.records
                        select new string[]
                        {
                                record.Name,
                                record.Age.ToString(),
                                record.Address.ToString(),
                                record.Id.ToString()
                        }
                    ).ToArray()
            };
        }

        internal void Delete(int id)
        {
            _customerService.DeleteCustomer(id);
        }
    }
}
