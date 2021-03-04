using GaragemGestao.Data;
using GaragemGestao.Data.Repositories;
using GaragemGestao.Helpers;
using GaragemGestao.Hubs;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GaragemGestao
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }


        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            //configures the authentication, Pass will be weak for testing purposes
            services.AddIdentity<User, IdentityRole>(cfg =>
             {
                 cfg.User.RequireUniqueEmail = true;
                 cfg.Password.RequireDigit = false;
                 cfg.Password.RequiredUniqueChars = 0;
                 cfg.Password.RequireLowercase = false;
                 cfg.Password.RequireUppercase = false;
                 cfg.Password.RequireNonAlphanumeric = false;
                 cfg.Password.RequiredLength = 6;
             })
                .AddEntityFrameworkStores<DataContext>(); //Cache
            //Configures END

            services.AddDbContext<DataContext>(cfg =>
            {
                cfg.UseSqlServer(this.Configuration.GetConnectionString("DefaultConnection"));
            });

            //Adding the Interfaces and Repositories
            services.AddTransient<SeedDb>();

            services.AddScoped<SeedDb>();

            services.AddScoped<IVehicleRepository, VehicleRepository>();
            services.AddScoped<IMechanicRepository, MechanicRepository>();
            services.AddScoped<IRepairRepository, RepairRepository>();
            services.AddScoped<IUserHelper, UserHelper>();


            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddSignalR();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            //Activates authentication
            app.UseAuthentication();

            app.UseSignalR(route =>
            {
                route.MapHub<ChatHub>("/Home/Index");
            });
            app.UseCookiePolicy();



            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Account}/{action=Login}/{id?}");
            });
        }
    }
}
