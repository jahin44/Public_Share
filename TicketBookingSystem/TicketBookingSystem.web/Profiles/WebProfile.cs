using AutoMapper;
using TicketBookingSystem.web.Areas.Admin.Models;
using TicketBookingSystem.System.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TicketBookingSystem.web.Profiles
{
    public class WebProfile : Profile
    {
        public WebProfile()
        {
            CreateMap<CreateCustomerModel, Customer>().ReverseMap();
            CreateMap<EditCustomerModel, Customer>().ReverseMap();
        }
    }
}
