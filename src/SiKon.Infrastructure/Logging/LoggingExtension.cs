using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NpgsqlTypes;
using Serilog;
using Serilog.Extensions.Logging;
using Serilog.Sinks.PostgreSQL;
using SiKon.Infrastructure.Common;

namespace SiKon.Infrastructure.Logging
{
    public static class LoggingExtension
    {
        public static void AddSerilog(this IServiceCollection services, IConfiguration configuration)
        {
            IDictionary<string, ColumnWriterBase> columnWriters = new Dictionary<string, ColumnWriterBase>
            {
                {Constants.SiKonLogDB.ColumnName.Message, new RenderedMessageColumnWriter(NpgsqlDbType.Text) },
                {Constants.SiKonLogDB.ColumnName.Template, new MessageTemplateColumnWriter(NpgsqlDbType.Text) },
                {Constants.SiKonLogDB.ColumnName.Level, new LevelColumnWriter(true, NpgsqlDbType.Varchar) },
                {Constants.SiKonLogDB.ColumnName.Created, new TimestampColumnWriter(NpgsqlDbType.Timestamp) },
                {Constants.SiKonLogDB.ColumnName.Exception, new ExceptionColumnWriter(NpgsqlDbType.Text) },
                {Constants.SiKonLogDB.ColumnName.Serialized, new LogEventSerializedColumnWriter(NpgsqlDbType.Jsonb) },
                {Constants.SiKonLogDB.ColumnName.Properties, new PropertiesColumnWriter(NpgsqlDbType.Jsonb) },
                {Constants.SiKonLogDB.ColumnName.Machine, new SinglePropertyColumnWriter(Constants.SiKonLogDB.PropertyName.MachineName, PropertyWriteMethod.ToString, NpgsqlDbType.Text, "l") }
            };

            string siKonLogDatabaseConnectionString = configuration.GetConnectionString(ConnectionStringName.SiKonLogDatabase);

            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .Enrich.FromLogContext()
                .Enrich.WithMachineName()
                .WriteTo.Console()
                .WriteTo.PostgreSQL(siKonLogDatabaseConnectionString, Constants.SiKonLogDB.TableName.AppLogs, columnWriters, needAutoCreateTable: true)
                .CreateLogger();

            AppDomain.CurrentDomain.ProcessExit += (s, e) => Log.CloseAndFlush();

            services.AddSingleton<ILoggerFactory, SerilogLoggerFactory>();
        }
    }
}