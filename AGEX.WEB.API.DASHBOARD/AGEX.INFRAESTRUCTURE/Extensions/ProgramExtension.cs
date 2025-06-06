using AGEX.CORE.Interfaces.Repositories;
using AGEX.CORE.Interfaces.Repositories.Employees;
using AGEX.CORE.Interfaces.Repositories.Orders;
using AGEX.CORE.Interfaces.Repositories.Users;
using AGEX.CORE.Interfaces.Services;
using AGEX.CORE.Models.Configuration;
using AGEX.CORE.Services;
using AGEX.CORE.Services.Employees;
using AGEX.CORE.Services.Orders;
using AGEX.CORE.Services.Users;
using AGEX.INFRAESTRUCTURE.Filters;
using AGEX.INFRAESTRUCTURE.Interfaces;
using AGEX.INFRAESTRUCTURE.Repositories;
using AGEX.INFRAESTRUCTURE.Repositories.Employees;
using AGEX.INFRAESTRUCTURE.Repositories.Orders;
using AGEX.INFRAESTRUCTURE.Repositories.Users;
using AGEX.INFRAESTRUCTURE.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

namespace AGEX.INFRAESTRUCTURE.Extensions
{
    public static class ProgramExtension
    {
        public static void AddServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddControllers(options =>
            {
                options.Filters.Add<GlobalExceptionFilter>();
            });

            services.Configure<ConfigurationDb>(configuration.GetSection("ConnectionStrings"));
            services.Configure<ConfigurationMessages>(configuration.GetSection("MessagesDefault"));
            services.Configure<ConfigurationLog>(configuration.GetSection("LogService"));
            services.ConfigureWritable<ConfigurationDb>(configuration.GetSection("ConnectionStrings"));

            services.AddScoped<ILogService, LogService>();
            services.AddScoped<IActivityService, ActivityService>();

            services.AddTransient<IEmployeeService, EmployeeService>();
            services.AddTransient<IOrderService, OrderService>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IParseService, ParseService>();
            services.AddTransient<IConfigurationService, ConfigurationService>();
            services.AddTransient<ICryptoService, CryptoService>();
            services.AddTransient<IDbService, DbService>();

            services.AddTransient<IEmployeeRepository, EmployeeRepository>();
            services.AddTransient<IOrderRepository, OrderRepository>();
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IBaseAgexRepository, BaseAgexRepository>();
            services.AddTransient<IActivityRepository, ActivityRepository>();
        }

        public static void ConfigureWritable<T>(this IServiceCollection services, IConfigurationSection section, string file = "appsettings.json") where T : class, new()
        {
            services.Configure<T>(section);
            services.AddTransient<IWritableConfigurationService<T>>(provider =>
            {
                var configuration = (IConfigurationRoot)provider.GetService<IConfiguration>();
                var environment = provider.GetService<IHostEnvironment>();
                var options = provider.GetService<IOptionsMonitor<T>>();
                return new WritableConfigurationService<T>(environment, options, configuration, section.Key, file);
            });
        }
    }
}
