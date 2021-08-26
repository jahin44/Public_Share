using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EO = TicketBookingSystem.System.Entities;
using BO = TicketBookingSystem.System.BusinessObjects;

namespace TicketBookingSystem.System.Profiles
{
    public class SystemProfile : Profile
    {
        public SystemProfile()
        {
            CreateMap<EO.Customer, BO.Customer>().ReverseMap();
        }
    }
}
