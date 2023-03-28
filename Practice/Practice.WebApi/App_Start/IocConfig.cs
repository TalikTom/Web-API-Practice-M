using Autofac;
using Autofac.Integration.WebApi;
using Practice.Repository;
using Practice.Repository.Common;
using Practice.Service;
using Practice.Service.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http;

namespace Practice.WebApi.App_Start
{
    public class IocConfig

    {

        public static void Configure()
        {
            var builder = new ContainerBuilder();
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());
            //builder.RegisterType<ChefService>().AsSelf().InstancePerRequest();
            //builder.RegisterType<ChefRepository>().AsSelf().InstancePerRequest();
            builder.RegisterType<ChefService>().As<IChefService>().InstancePerRequest();
            builder.RegisterType<ChefRepository>().As<IChefRepository>().InstancePerRequest();
            var container = builder.Build();
            var resolver = new AutofacWebApiDependencyResolver(container);
            GlobalConfiguration.Configuration.DependencyResolver = resolver;

        }
    




    }
}