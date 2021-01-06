using Labo5_ASP_MVC.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Labo5_ASP_MVC.Models;
using Microsoft.AspNetCore.Mvc.Razor;

namespace Labo5_ASP_MVC
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
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));
            services.AddDatabaseDeveloperPageExceptionFilter();

            services.AddDefaultIdentity<IdentityUser>(options => { 
                options.SignIn.RequireConfirmedAccount = true;
                options.Password = new PasswordOptions
                {
                    RequiredLength = 6,
                    RequireLowercase = true,
                    RequireUppercase = true
                };
                options.Lockout = new LockoutOptions
                {
                    DefaultLockoutTimeSpan = new TimeSpan(0, 5, 0),
                    MaxFailedAccessAttempts = 3
                };
                options.User.RequireUniqueEmail = true;
                })
                .AddEntityFrameworkStores<ApplicationDbContext>();

            services.AddControllersWithViews()
                .AddViewLocalization(
                    LanguageViewLocationExpanderFormat.Suffix)
                .AddDataAnnotationsLocalization();

            services.AddDbContext<TheatersContext>(options =>
                    options.UseSqlServer(Configuration.GetConnectionString("TheatersContext")));

            services.AddLocalization( options => options.ResourcesPath = "Resources");


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            var supportedCultures = new[] { "nl", "en", "en-US", "fr", "fr-FR" };
            var localizationOptions = new RequestLocalizationOptions()
                    .SetDefaultCulture(supportedCultures[0])
                    .AddSupportedCultures(supportedCultures)
                    .AddSupportedUICultures(supportedCultures);
            app.UseRequestLocalization(localizationOptions);

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();



            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=NewsMessages}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
        }
    }
}
