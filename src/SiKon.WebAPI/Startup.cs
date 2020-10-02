using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hangfire;
using Hangfire.Dashboard.BasicAuthorization;
using Hangfire.PostgreSql;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Serilog;

namespace SiKon.WebAPI
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
            services.AddCors(options =>
            {
                options.AddPolicy(name: "AllowAnyOrigin",
                    builder =>
                    {
                        builder.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowAnyHeader();
                    });
            });

            services.AddControllers();

            services.AddHangfire(connection => connection
                                                    .UseSerilogLogProvider()
                                                    .UsePostgreSqlStorage(Configuration["Hangfire:Database"]));
            services.AddHangfireServer();

            services.AddSwaggerGen();

            services.AddHealthChecks()
                .AddNpgSql(Configuration["Hangfire:Database"], name: "Hangfire Database")
                .AddHangfire(s => s.MinimumAvailableServers = 1, name:"Hangfire Service");
            services.AddHealthChecksUI(settings => settings.AddHealthCheckEndpoint(Configuration["App:Name"], Configuration["HealthChecks:Url:Base"]))
                .AddPostgreSqlStorage(Configuration["HealthChecks:Database"]);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseSwagger(s =>
            {
                s.PreSerializeFilters.Add((swagger, httpReq) =>
                {
                    swagger.Servers = new List<OpenApiServer> { new OpenApiServer { Url = $"{httpReq.Scheme}://{httpReq.Host.Value}" } };
                });
            });
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", $"{Configuration["App:Name"]} v1");
            });
            app.UseHangfireDashboard(Configuration["Hangfire:Dashboard:Url"], new DashboardOptions
            {
                Authorization = new[]
                {
                    new BasicAuthAuthorizationFilter
                    (new BasicAuthAuthorizationFilterOptions
                    {
                        RequireSsl = false,
                        SslRedirect = false,
                        LoginCaseSensitive = true,
                        Users = new []
                        {
                           new BasicAuthAuthorizationUser
                           {
                               Login = Configuration["Hangfire:Dashboard:Username"],
                               PasswordClear =  Configuration["Hangfire:Dashboard:Password"]
                           }
                        }
                     })
                }
            });

            app.UseSerilogRequestLogging(opts => opts.EnrichDiagnosticContext = LogEnricher.EnrichFromRequest);

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors("AllowAnyOrigin");

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHealthChecks(Configuration["HealthChecks:Url:Base"], new HealthCheckOptions
                {
                    Predicate = _ => true,
                    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
                });
                endpoints.MapHealthChecksUI(options =>
                {
                    options.UIPath = Configuration["HealthChecks:Url:UI"];
                    options.ApiPath = Configuration["HealthChecks:Url:API"];
                });
                endpoints.MapControllers();
            });
        }
    }
}
