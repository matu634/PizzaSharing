using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.DAL.App;
using Contracts.DAL.Base;
using Contracts.DAL.Base.Helpers;
using DAL.App.EF;
using DAL.App.EF.Helpers;
using DAL.Base.EF.Helpers;
using Domain.Identity;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WebApp.Helpers;

namespace WebApp
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
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddDbContext<AppDbContext>(options =>
                options.UseMySql(
                    Configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<AppUser, AppRole>()
                .AddEntityFrameworkStores<AppDbContext>()
                .AddDefaultTokenProviders();                //cookie functionality

            services.AddSingleton<IEmailSender, EmailSender>();
            
            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 1;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredUniqueChars = 0;
                options.Password.RequireNonAlphanumeric = false;
            });

            services.AddScoped<IDataContext, AppDbContext>();
            services.AddSingleton<IRepositoryFactory, AppRepositoryFactory>();
            services.AddScoped<IRepositoryProvider, BaseRepositoryProvider>();
            services.AddScoped<IAppUnitOfWork, AppUnitOfWork>();

            services.AddCors(options =>
            {
                options.AddPolicy("CorsAllowAll",
                    builder =>
                    {
                        builder.AllowAnyOrigin();
                        builder.AllowAnyHeader();
                        builder.AllowAnyMethod();
                    });
            });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
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
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseAuthentication();

            app.UseCors("CorsAllowAll");

            app.UseMvc(routes =>
            {
//                routes.MapRoute(
//                    name: "areas",
//                    template: "{area:exists}/{controller=}")
                
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}