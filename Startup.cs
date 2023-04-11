using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TimeToStudy.Models;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace TimeToStudy
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
            services.AddControllersWithViews();

            services.AddDbContext<EventContext>(options =>
            options.UseSqlServer(
                Configuration.GetConnectionString("EventContext")));

            //user login
            services.AddIdentity<IdentityUser, IdentityRole>()
        .AddEntityFrameworkStores<EventContext>()
        .AddDefaultTokenProviders();

            services.AddScoped<SignInManager<IdentityUser>>();
        }


        public async Task SeedDatabase(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            // Create the "Member" role if it doesn't already exist
            if (!await roleManager.RoleExistsAsync("Member"))
            {
                var role = new IdentityRole("Member");
                await roleManager.CreateAsync(role);
            }

            // Create a new user
            var user = new IdentityUser
            {
                UserName = "ITExpo",
                Email = "TimeToStudy@uc"
            };

            // Add the user to the database
            var result = await userManager.CreateAsync(user, "ITExpo2023!");

            if (result.Succeeded)
            {
                // Add the user to the "Member" role
                await userManager.AddToRoleAsync(user, "Member");
            }
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
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

            //set up a test user
            using (var scope = app.ApplicationServices.CreateScope())
            {
                var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();
                var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
                SeedDatabase(userManager, roleManager).GetAwaiter().GetResult();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            //user login
            app.UseAuthentication();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=LogInLink}/{id?}");

                endpoints.MapControllerRoute(
            name: "calendar",
            pattern: "calendar/{date}",
            defaults: new { controller = "EventController", action = "Calendar" });
            });
        }
    }
}
