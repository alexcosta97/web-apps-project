using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using src.Data;
using src.Models;
using src.Services;
using src.Authorization;

namespace src
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
            var skipSSL = Configuration.GetValue<bool>("LocalTest:skipSSL");

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlite(Configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            // Add application services.
            services.AddTransient<IEmailSender, EmailSender>();

            services.AddMvc(config =>
            {
                var policy = new AuthorizationPolicyBuilder()
                                .RequireAuthenticatedUser()
                                .Build();
                config.Filters.Add(new AuthorizeFilter(policy));
            });

            services.AddScoped<IAuthorizationHandler, StaffIsOwnerAuthorizationHandler>();
            services.AddSingleton<IAuthorizationHandler, AdministratorsAuthorizationHandler>();
            services.AddSingleton<IAuthorizationHandler, StaffManagerAuthorizationHandler>();
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
            }

            app.UseStaticFiles();

            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

            //Set password with the Secret Manager tool.
            // dotnet user-secrets set SeedUserPW <pw>
            var testUserPw = Configuration["SeedUserPW"];

            if(String.IsNullOrEmpty(testUserPw))
            {
                throw new System.Exception("Use secrets manager to set SeedUserPW \n" +
                                            "dotnet user-secrets set SeedUserPW <pw>");
            }

            try
            {
                SeedData.Initialize(app.ApplicationServices, testUserPw).Wait();
            }
            catch
            {
                throw new System.Exception("You need to update the DB "
                            + "\nPM > Update-Database "+ "\n or \n" +
                            "> dotnet ef database update"
                            + "\nIf that doesn't work, comment out SeedRoles and "
                            + "register a new user");
            }
        }
    }
}
