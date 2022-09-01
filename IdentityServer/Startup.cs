using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityServer.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;


namespace IdentityServer
{
    public class Startup
    {
        private readonly IConfiguration _config;
        public Startup(IConfiguration config)
        {
            _config = config;
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {

            var connectionString = _config.GetConnectionString("DefaultConnection");

            services.AddDbContext<AppDbContext>(config =>
            {
                config.UseSqlServer(connectionString);
                //config.UseSqlServer(connectionString);
                //config.UseInMemoryDatabase("Memory");
            });

            // AddIdentity registers the services
            services.AddIdentity<IdentityUser, IdentityRole>(config =>
            {
                config.Password.RequiredLength = 4;
                config.Password.RequireDigit = false;
                config.Password.RequireNonAlphanumeric = false;
                config.Password.RequireUppercase = false;
            })
                .AddEntityFrameworkStores<AppDbContext>()
                .AddDefaultTokenProviders();

            services.ConfigureApplicationCookie(config =>
            {
                config.Cookie.Name = "IdentityServer.Cookie";
                config.LoginPath = "/Auth/Login";
                config.LogoutPath = "/Auth/Logout";
            });

            var migrationsAssembly = typeof(Startup).Assembly.GetName().Name;
            services.AddIdentityServer()
             .AddAspNetIdentity<IdentityUser>()
            // .AddConfigurationStore(options =>
            // {
            //     options.ConfigureDbContext = b => b.UseSqlServer(connectionString,
            //         sql => sql.MigrationsAssembly(migrationsAssembly));
            // })
            //.AddOperationalStore(options =>
            //{
            //    options.ConfigureDbContext = b => b.UseSqlServer(connectionString,
            //        sql => sql.MigrationsAssembly(migrationsAssembly));
            //})
            .AddInMemoryApiScopes(Configuration.Apis)
            .AddInMemoryIdentityResources(Configuration.IdentityResources)
            .AddInMemoryApiResources(Configuration.GetApis())
            .AddInMemoryClients(Configuration.GetClients())
            .AddDeveloperSigningCredential();

            services.AddControllersWithViews();
            //services.AddMvc();
            //services.AddCors(options =>
            //{
            //    options.AddPolicy("CorsPolicy",
            //         builder => builder.WithOrigins("http://localhost:4200/")
            //        .WithMethods("POST", "GET", "PUT")
            //        .WithHeaders("*")
            //    );
                

            //});

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }


            app.UseRouting();
            app.UseIdentityServer();

            if (env.IsDevelopment())
            {
                app.UseCookiePolicy(new CookiePolicyOptions()
                {
                    MinimumSameSitePolicy = Microsoft.AspNetCore.Http.SameSiteMode.Lax
                });
            }
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute("default", "{controller=Home}/{action=Index}");
            });
            
        
        }

        private void InitializeDatabase(IApplicationBuilder app)
        {
            
        }
    }
}
