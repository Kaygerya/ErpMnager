using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using ERPManager.Service.Model;
using ERPManager.Service.Abstract;
using ERPManager.DataAccess.Abstract;
using ERPManager.DataAccess.Model.EntityFramework;
using ERPManager.Web.MiddleWares;
using ERPManager.Entities.Model.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using ERPManager.Web.Models.Base;
using ERPManager.Core.Settings;

namespace ERPManager.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            //services.Configure<ErpManagerSettings>(Configuration.GetSection("ErpManagerSettings"));
            services.AddSingleton(Configuration.GetSection("ErpManagerSettings").Get<ErpManagerSettings>());
            services.AddDbContext<CustomIdentityDbContext>();
            services.AddDbContext<LanguageDbContext>();

            services.AddScoped<ICoolComService, CoolComService>();
            services.AddScoped<ICoolComData, EfCoolComData>();

            services.AddScoped<ICoolUserService, CoolUserService>();
            services.AddScoped<ICoolUserData, EfCoolUserData>();

            services.AddScoped<ICoolAppService, CoolAppService>();
            services.AddScoped<ICoolAppData, EfCoolAppData>();


            services.AddScoped<ICoolQueryService, CoolQueryService>();
            services.AddScoped<ICoolQueryData, EfCoolQueryData>();

            services.AddScoped<ILanguageService, LanguageService>();
            services.AddScoped<ILanguageData, EfLanguageData>();
            services.AddScoped<ILanguageItemData, EfLanguageItemData>();

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddSingleton<ICacheService, CacheService>();

            services.AddIdentity<CustomIdentityUser, CustomIdentityRole>()
                .AddEntityFrameworkStores<CustomIdentityDbContext>()
                .AddDefaultTokenProviders();

            services.ConfigureApplicationCookie(options =>
            {
                options.Cookie.HttpOnly = true;
                options.Cookie.Expiration = TimeSpan.FromDays(150);
                options.LoginPath = "/Account/LogIn";
                options.LogoutPath = "/Account/Logout";
                options.AccessDeniedPath = "/Account/AccessDenied";
            }
           );


            services.Configure<IdentityOptions>(options =>
            {
                // Password settings
                options.Password.RequireDigit = true;
                options.Password.RequiredLength = 8;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = true;
                options.Password.RequireLowercase = false;
                options.Password.RequiredUniqueChars = 6;

                // Lockout settings
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(30);
                options.Lockout.MaxFailedAccessAttempts = 10;
                options.Lockout.AllowedForNewUsers = true;

                // User settings
                options.User.RequireUniqueEmail = true;


                
            });

           

            // services.AddSingleton(Configuration.GetSection("PageConfiguration").Get<PagingSettings>());

            //services.ConfigureApplicationCookie(options =>
            //{
            //    // Cookie settings
            //    options.Cookie.HttpOnly = true;
            //    options.Cookie.Expiration = TimeSpan.FromDays(150);
            //    options.LoginPath = "/Account/Login"; // If the LoginPath is not set here, ASP.NET Core will default to /Account/Login
            //    options.LogoutPath = "/Account/Logout"; // If the LogoutPath is not set here, ASP.NET Core will default to /Account/Logout
            //    options.AccessDeniedPath = "/Account/AccessDenied"; // If the AccessDeniedPath is not set here, ASP.NET Core will default to /Account/AccessDenied
            //    options.SlidingExpiration = true;
            //});


            //session active
            services.AddSession();
            services.AddDistributedMemoryCache();

            services.AddMemoryCache();
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {

            var builder = new ConfigurationBuilder()
        .SetBasePath(env.ContentRootPath)
        .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseFileServer();
            app.UseNodeModules(env.ContentRootPath);
            app.UseSession();
            app.UseAuthentication();
            app.UseMvc(configureRoutes);

        }

        private void configureRoutes(IRouteBuilder routebuilder)
        {
            routebuilder.MapRoute("Default", "{Controller=Home}/{Action=Index}/{id?}");
        }
    }
}
