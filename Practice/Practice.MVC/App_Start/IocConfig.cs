using Autofac;
using Autofac.Integration.Mvc;
using AutoMapper;
using Practice.Dal;
using Practice.MVC.Mapping;
using Practice.Repository;
using Practice.Repository.Common;
using Practice.Service;
using Practice.Service.Common;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;



namespace Practice.MVC.App_Start
{
    public class IocConfig
    {
        public static void Configure()
        {

            var builder = new ContainerBuilder();

            builder.RegisterControllers(Assembly.GetExecutingAssembly());

            builder.RegisterType<RestaurantContext>().AsSelf().InstancePerLifetimeScope();

            builder.RegisterType<ChefService>().As<IChefService>().InstancePerRequest();
            builder.RegisterType<EFChefRepository>().As<IChefRepository>().InstancePerRequest();


            builder.Register(context => new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            })).AsSelf().SingleInstance();

            builder.Register(context => context.Resolve<MapperConfiguration>().CreateMapper())
                .As<IMapper>()
                .InstancePerLifetimeScope();


            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}