using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using GarageV2.Models;
using AutoMapper;
using GarageV2.Services;
using Microsoft.AspNetCore.Identity;

namespace GarageV2
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
            //Adds Dependency Injection functionality for Automapper
            services.AddAutoMapper();

            //Service for generating vehicles
            services.AddSingleton<ParkedVehicleGenerator>();

            //Class for accessing the garage settings e.g garage price per minute.
            services.AddSingleton<GarageSettings>();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddDbContext<GarageV2Context>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("GarageV2Context"))
                //options.UseSqlServer(Configuration.GetConnectionString("GarageV2Mac"))
            );

            services.AddIdentity<IdentityUser, IdentityRole>(options =>
            {
                /*** Password Policies ***/
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;
            })
                .AddEntityFrameworkStores<GarageV2Context>(); //The database context where to store the security info.
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            //For using custom Error404 page.
            app.UseStatusCodePagesWithRedirects("/Home/Error404");

            //Needs to be implemented before useMvc.
            //Middleware that handles the cookies used for the Core Identity.
            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
