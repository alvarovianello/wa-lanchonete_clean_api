using Microsoft.EntityFrameworkCore;
using WA.Application.Interfaces;
using WA.Application.Services;
using WA.Domain.Base;
using WA.Domain.Entities;
using WA.Domain.Interfaces;
using WA.Persistence.Base;
using WA.Persistence.Context;
using WA.Persistence.Repositories;

namespace WA.Api.Extensions
{
    public static class DependencyInjectionConfig
    {
        public static IServiceCollection AddResolveDependencies(this WebApplicationBuilder builder)
        {
            IServiceCollection services = builder.Services;
            var configuration = builder.Configuration;
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            services.AddTransient<ICustomerService, CustomerService>();
            services.AddTransient<ICustomerRepository, CustomerRepository>();

            services.AddTransient<ICategoryService, CategoryService>();
            services.AddTransient<ICategoryRepository, CategoryRepository>();

            services.AddTransient<IProductService, ProductService>();
            services.AddTransient<IRepository<Product>, Repository<Product>>();

            services.AddTransient<IPaymentService, PaymentService>();
            services.AddTransient<IPaymentRepository, PaymentRepository>();

            services.AddTransient<IOrderService, OrderService>();
            services.AddTransient<IOrderRepository, OrderRepository>();
            services.AddTransient<IRepository<Order>, Repository<Order>>();
            services.AddTransient<IRepository<Orderitem>, Repository<Orderitem>>();
            services.AddTransient<IRepository<Payment>, Repository<Payment>>();
            services.AddTransient<IRepository<Orderstatus>, Repository<Orderstatus>>();


            services.AddTransient<IDbConnectionFactory, DbConnectionFactory>((ctx) =>
            {
                return new DbConnectionFactory(connectionString);
            });
            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseNpgsql(connectionString);
            });

            services.AddRouting(options => options.LowercaseUrls = true);
            services.AddHttpClient();

            return services;
        }
    }
}
