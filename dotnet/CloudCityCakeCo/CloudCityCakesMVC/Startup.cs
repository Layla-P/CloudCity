using CloudCityCakesMVC.Data.Context;
using CloudCityCakesMVC.Data.Repositories;
using CloudCityCakesMVC.Models.DTO;
using CloudCityCakesMVC.Services.Implementations;
using CloudCityCakesMVC.Services.Interfaces;
using CloudCityCakesMVC.Services.Rules;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
namespace CloudCityCakesMVC
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
            //https://database.guide/how-to-install-sql-server-on-a-mac/
            //https://stormpath.com/blog/tutorial-entity-framework-core-in-memory-database-asp-net-core
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultSql")));
            // --------- IoC Registration -------------
            //repositories
            services.AddScoped<IIngredientsRepository, IngredientsRepository>();
            services.AddScoped<ICakeOrderRepository, CakeOrderRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            //services
            services.AddScoped<IIngredientsService, IngredientsService>();
            services.AddScoped<IPriceCalculationService, PriceCalculationService>();
            services.AddScoped<ICakeOrderService, CakeOrderService>();
            services.AddScoped<IEmailService, EmailService>();
            
            // --------- Notification Handler Registration -------------
            services.AddScoped<IStatusNotificationRule, CompletedNotificationRule>();
            services.AddScoped<IStatusNotificationRule, AcceptedNotificationRule>();
            services.AddScoped<INotificationHandler, NotificationHandler>();
            
            services.Configure<TwilioAccount>(Configuration.GetSection("TwilioAccount"));
            services.Configure<SendGridAccount>(Configuration.GetSection("SendGridAccount"));
            
            services.AddControllersWithViews();
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
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
