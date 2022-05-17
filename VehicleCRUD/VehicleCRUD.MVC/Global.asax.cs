using Autofac;
using Autofac.Integration.Mvc;
using AutoMapper;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using VehicleCRUD.MVC.ViewModels;
using VehicleCRUD.Service;
using VehicleCRUD.Service.Sorting__Filtering__Paging_classes;

namespace VehicleCRUD.MVC
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            var builder = new ContainerBuilder();
       
            
            builder.RegisterControllers(typeof(MvcApplication).Assembly);

            builder.RegisterType<VehiclesDbEntities>().AsSelf();
            builder.RegisterType<VehicleMake>().AsSelf();
            builder.RegisterType<VehicleModel>().AsSelf();
            builder.RegisterType<VehicleMakeViewModel>().AsSelf();



            builder.Register(context => new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<VehicleMake, VehicleMakeViewModel>();
                cfg.CreateMap<VehicleModel, VehicleModelViewModel>(); 
                
            })).AsSelf().SingleInstance();


            builder.Register(c =>
            {
                //This resolves a new context that can be used later.
                var context = c.Resolve<IComponentContext>();
                var config = context.Resolve<MapperConfiguration>();
                return config.CreateMapper(context.Resolve);
            })
            .As<IMapper>()
            .InstancePerLifetimeScope();
            

            builder.RegisterType<VehicleMakeService>().As<IVehicleMakeService>();
            builder.RegisterType<VehicleModelService>().As<IVehicleModelService>();
            builder.RegisterType<Sorting>().As<ISorting>();
            builder.RegisterType<Filtering>().As<IFiltering>();
            builder.RegisterType<Paging>().As<IPaging>();


            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));

            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}
