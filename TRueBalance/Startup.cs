using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TRueBalance.Repositories.Meals;
using TRueBalance.Repositories.Orders;
using TRueBalance.Repositories.Products;
using TRueBalance.Repositories.Sells;
using TRueBalance.Data;
using TRueBalance.Models;
using TRueBalance.Repositories.Dates;
using TRueBalance.Repositories.Invoices;
using TRueBalance.Repositories.Prints;
using TRueBalance.Repositories.UserSettings;
using TRueBalance.Services;
using TRueBalance.Repositories.Categories;
using TRueBalance.Repositories.OrderMeals;
using TRueBalance.Repositories.Statistics;
using TRueBalance.Repositories.CSVManager;

namespace TRueBalance
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
                options.UseSqlServer(Configuration.GetConnectionString("Production")));

            services.AddIdentity<ApplicationUser, IdentityRole>(option =>
            {
                option.Password.RequiredLength = 6;
                option.Password.RequiredUniqueChars = 0;
                option.Password.RequireNonAlphanumeric = false;
            }).AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            //Services
            services.AddTransient<IEmailSender, EmailSender>();
            services.Configure<AuthMessageSenderOptions>(Configuration);

            //Repositories
            //services.AddTransient<ICSVRepository, CSVRepository>();
            services.AddTransient<IProductRepository, ProductRepository>();
            services.AddTransient<IMealRepository, MealRepository>();
            services.AddTransient<ISellRepository, SellRepository>();
            services.AddTransient<IInvoiceRepository, InvoiceRepository>();
            services.AddTransient<IOrderRepository, OrderRepository>();
            services.AddTransient<IDateManageRepository, DateManageRepository>();
            services.AddTransient<IPrintQueueRepository, PrintQueueRepository>();
            services.AddTransient<IUserSettingRepository, UserSettingRepository>();
            services.AddTransient<IOrderMealRepository, OrderMealRepository>();
            services.AddTransient<ICategoriesRepository, CategoriesRepository>();

            //Utils
            services.AddTransient<IStatistic, Statistic>();

            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
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
        }
    }
}
