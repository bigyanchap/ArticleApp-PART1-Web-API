using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using BilbaLeaf.Api.Autofac;
using BilbaLeaf.Entities.Identity;
using BilbaLeaf.Repository;
using BilbaLeaf.Repository.Common;
using BilbaLeaf.Repository.Infrastructure;
using BilbaLeaf.Repository.Repository;
using BilbaLeaf.Service;
using BilbaLeaf.Service.Infrastructure;
using LogicLync.Api.Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;

namespace BilbaLeaf
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }


        public void ConfigureServices(IServiceCollection services)
        {

            //services.AddCors(options =>
            //{
            //    options.AddPolicy("CorsPolicy",
            //         builder => builder.WithOrigins("http://localhost:4200/")
            //        .WithMethods("POST", "GET", "PUT")
            //        .WithHeaders("*")
            //    );

            //});
            services.AddCors(options =>
            {
                options.AddDefaultPolicy(
                    builder =>
                    {
                        builder.AllowAnyOrigin()
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                    });
            });
           


            services.AddControllers();
            services.Configure<PhotoSettings>(Configuration.GetSection("PhotoSettings"));
            services.AddDbContext<BilbaLeafContext>(x => x.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddIdentity<AppUser, IdentityRole>()
                .AddEntityFrameworkStores<BilbaLeafContext>();

            var mapper=AutoMapping.Automapperinitializer();
            services.AddSingleton(mapper);

           

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IDbFactory, DbFactory>();

            //Article
            //services.AddScoped<IArticleService, ArticleService>();
            //services.AddScoped<IArticleRepository, ArticleRepository>();





        }
        public void ConfigureContainer(ContainerBuilder builder)
        {
            var mapper = AutoMapping.Automapperinitializer();
            builder.RegisterInstance(mapper);

            builder.RegisterAssemblyTypes(Assembly.Load("BilbaLeaf.Service")).
                Where(x => x.Name.EndsWith("Service", StringComparison.OrdinalIgnoreCase)).AsImplementedInterfaces();

            builder.RegisterAssemblyTypes(Assembly.Load("BilbaLeaf.Repository")).
               Where(x => x.Name.EndsWith("Repository", StringComparison.OrdinalIgnoreCase)).AsImplementedInterfaces();

        }
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseAuthentication();
            app.UseStaticFiles(new StaticFileOptions()
            {
                FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), @"Uploads")),
                RequestPath = new PathString("/Uploads")
            });
            app.UseAuthentication();
            app.UseAuthorization();
            //app.UseCors("CorsPolicy");
            app.UseCors();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                //endpoints.MapDefaultControllerRoute();
            });
        }
    }
}
