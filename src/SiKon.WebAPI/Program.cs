using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using SiKon.WebAPI.SerilogEnrichers;

namespace SiKon.WebAPI
{
    public class Program
    {
        public static IConfiguration Configuration { get; } = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile($"{AppContext.BaseDirectory}appsettings.json", optional: false, reloadOnChange: true)
            .AddJsonFile($"{AppContext.BaseDirectory}appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production"}.json", optional: true)
            .AddEnvironmentVariables()
            .Build();
        public static void Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(Configuration)
                .Enrich.With<EventTypeEnricher>()
                .Enrich.WithProperty("Version", typeof(Program).Assembly.GetName().Version)
                .Enrich.WithProperty("ApplicationName", Configuration["App:Name"])
                .CreateLogger();

            Serilog.Debugging.SelfLog.Enable(msg => Console.WriteLine(msg));

            try
            {
                Log.Information("Getting the service running...");

                CreateHostBuilder(args).Build().Run();
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Host terminated unexpectedly");
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                })
                .UseSerilog();
    }
}
