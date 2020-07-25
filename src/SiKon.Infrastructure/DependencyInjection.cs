using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SiKon.Application.Interfaces;
using SiKon.Infrastructure.Common;
using SiKon.Infrastructure.Persistence;
using SiKon.Infrastructure.Repositories;
using SiKon.Infrastructure.Services;

namespace SiKon.Infrastructure
{

    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<IDateTimeOffsetService, DateTimeOffsetService>();

            services.AddDbContext<SiKonDBContext>(options =>
                options.UseSqlServer(
                    configuration.GetConnectionString(ConnectionStringName.SiKonDatabase),
                    b => b.MigrationsAssembly(typeof(SiKonDBContext).Assembly.FullName)));

            services.AddScoped<ISiKonDBContext>(provider => provider.GetService<SiKonDBContext>());

            services.AddTransient<IMemberRepository, MemberRepository>();
            services.AddTransient<ITCPEndpointRepository, TCPEndpointRepository>();
            services.AddTransient<IUnitOfWork, UnitOfWork>();

            return services;
        }
    }
}