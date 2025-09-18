using System.Collections.ObjectModel;
using System.Data;
using Microsoft.EntityFrameworkCore;
using Serilog.Sinks.MSSqlServer;
using Serilog;
using SLAwareWorkerService;
using SLAwareWorkerService.Entities.SLAware;
using SLAwareWorkerService.Interfaces;
using SLAwareWorkerService.Services.SlaSeverity;



var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddHostedService<Worker>();

builder.Services.AddScoped<ISlaSeverityService, SlaSeverityService>();

var connectionString = builder.Configuration.GetConnectionString("SLAware");
builder.Services.AddDbContext<slaware_dataContext>(options =>
{
    options.UseSqlServer(connectionString, sqlServerOptionsAction: sqloptions =>
    {
        sqloptions.EnableRetryOnFailure(maxRetryCount: 60, maxRetryDelay: TimeSpan.FromSeconds(5), errorNumbersToAdd: null);
        sqloptions.MaxBatchSize(20);
    });
});

//Serilog
var columnOptions = new ColumnOptions
{
    AdditionalColumns = new Collection<SqlColumn>
    {
        new SqlColumn { ColumnName = "ticket_id", DataType = SqlDbType.BigInt },
        new SqlColumn { ColumnName = "response_breached_date", DataType = SqlDbType.DateTime },
        new SqlColumn { ColumnName = "resolution_breached_date", DataType = SqlDbType.DateTime },
        new SqlColumn { ColumnName = "response_due_dtm", DataType = SqlDbType.DateTime },
        new SqlColumn { ColumnName = "resolution_due_dtm", DataType = SqlDbType.DateTime },
        new SqlColumn { ColumnName = "response_remaining_time", DataType = SqlDbType.Time },
        new SqlColumn { ColumnName = "resolution_remaining_time", DataType = SqlDbType.Time },
    }
};

Log.Logger = new LoggerConfiguration()
    .WriteTo.MSSqlServer(
        connectionString: connectionString,
        sinkOptions: new MSSqlServerSinkOptions
        {
            TableName = "ticket_breach_logs",
            AutoCreateSqlTable = true
        },
        columnOptions: columnOptions
    )
    .Enrich.FromLogContext()
    .CreateLogger();

builder.Services.AddScoped<slaware_dataContext>();

var host = builder.Build();
host.Run();
