using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TicketBookingSystem.web
{
    public class WebModule : Module
    {


        protected override void Load(ContainerBuilder builder)
        {

          //  builder.RegisterType<SimpleDatabaseService>().As<IDatabaseService>()
          //       .InstancePerLifetimeScope();                                   // example how Autofac work   
            base.Load(builder);
        }
    }
}