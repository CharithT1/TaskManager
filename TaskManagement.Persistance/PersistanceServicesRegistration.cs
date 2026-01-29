using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Application.Configurations;
using TaskManagement.Application.Contracts.Persistence;
using TaskManagement.Persistance.Repositories;

namespace TaskManagement.Persistance
{
    public static class PersistanceServicesRegistration
    {
        public static IServiceCollection ConfigurePersistanceServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<TaskManagementDbContext>(options => 
                options.UseSqlServer(configuration.GetConnectionString("DevConnection"))
            );
            services.Configure<AuthConfigurations>(configuration.GetSection("AuthConfigurations"));
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<ITaskTypeRepository,TaskTypeRepository>();
            services.AddScoped<IManagedTaskRepository, ManagedTaskRepository>();
            return services;
        }
    }
}
