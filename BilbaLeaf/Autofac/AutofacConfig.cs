using Autofac;
using Autofac.Extensions.DependencyInjection;

using BilbaLeaf.Repository;
using BilbaLeaf.Repository.Common;
using BilbaLeaf.Repository.Infrastructure;
using BilbaLeaf.Repository.Repository;
using BilbaLeaf.Service.Infrastructure;
using BilbaLeaf.Service;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Web.Http;

namespace BilbaLeaf.Api.Autofac
{
    public static class AutofacConfig
    {
        //public static IServiceProvider ConfigurationContainer(ContainerBuilder builder)
        //{
        //    //var builder = new ContainerBuilder();
            
        //    //builder.RegisterType<UnitOfWork>().As<IUnitOfWork>().InstancePerRequest();
        //    //builder.RegisterType<DbFactory>().As<IDbFactory>().InstancePerRequest();
        //    //builder.RegisterType<CategoryService>().As<ICategoryService>().InstancePerRequest();
        //    //builder.RegisterType<CategoryImageRepository>().As<ICategoryImageRepository>().InstancePerRequest();
        //    //builder.RegisterType<CategoryRepository>().As<ICategoryRepository>().InstancePerRequest();
        //    //var container = builder.Build();
        //    //return new AutofacServiceProvider(container);
        //}
    }
}
