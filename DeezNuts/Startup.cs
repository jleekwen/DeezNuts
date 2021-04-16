using DeezNuts.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using DeezNuts.Repositories;
using DeezNuts.Services;
using DeezNuts.Managers;
using DeezNuts.Managers.States.Interfaces;

namespace DeezNuts
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
            services.Configure<DeezNutsConfig>(Configuration);

            services.AddDbContext<DeezNutsContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddSession();
            services.AddHttpContextAccessor();

            services.AddTransient<ICustomerRepository, CustomerRepository>();
            services.AddTransient<IListeningActionRepository, ListeningActionRepository>();
            services.AddTransient<IMessageLogRepository, MessageLogRepository>();
            services.AddTransient<IMessageRepository, MessageRepository>();
            services.AddTransient<IOrderRepository, OrderRepository>();
            services.AddTransient<IProductRepository, ProductRepository>();
            services.AddTransient<ISettingRepository, SettingRepository>();

            services.AddTransient<ICustomerService, CustomerService>();
            services.AddTransient<IGoogleAnalyticsService, GoogleAnalyticsService>();
            services.AddTransient<IListeningActionService, ListeningActionService>();
            services.AddTransient<IMessageLogService, MessageLogService>();
            services.AddTransient<IMessageService, MessageService>();
            services.AddTransient<IOrderService, OrderService>();
            services.AddTransient<IProductService, ProductService>();
            services.AddTransient<ISettingService, SettingService>();
            services.AddTransient<ITwilioService, TwilioService>();

            services.AddTransient<IState, StateManager>();

            services.AddControllersWithViews();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //if (env.IsDevelopment())
            //{
            //    app.UseDeveloperExceptionPage();
            //}
            //else
            //{
            //    app.UseExceptionHandler("/Home/Error");
            //    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            //    app.UseHsts();
            //}
            //app.UseHttpsRedirection();
            //app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
            });
        }
    }
}
