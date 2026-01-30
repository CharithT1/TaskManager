using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using TaskManagement.Application.Common;
using TaskManagement.Application.Mappers;
using TaskManagement.Application.Responses;
using FluentValidation.Results;

namespace TaskManagement.Application
{
    public static class ApplicationServicesRegistration
    {
        public static IServiceCollection ConfigureApplicationServices(this IServiceCollection services) 
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddSingleton<IMapper<ValidationResult, BaseCommandResponse>, ServiceErrorMapper>();
            services.AddSingleton<IMapper<int, BaseCommandResponse>,ServiceSuccessMapper>();
            return services;
        }   
    }
}
