using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using VehicleCRUD.Service;

namespace VehicleCRUD.MVC
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<VehiclesDbEntities>().AsSelf();
            builder.RegisterType<VehicleMake>().AsSelf();
            builder.RegisterType<VehicleModel>().AsSelf();

            builder.RegisterType<VehicleService>().As<IVehicleService>();

            builder.Build();

            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}
