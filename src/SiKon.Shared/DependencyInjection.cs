using System.Reflection;
using AutoMapper;
using Microsoft.Extensions.DependencyInjection;

namespace SiKon.Shared
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddShared(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            return services;
        }
    }
}