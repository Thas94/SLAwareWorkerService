using Microsoft.EntityFrameworkCore;
using SLAwareWorkerService;
using SLAwareWorkerService.Entities.SLAware;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddHostedService<Worker>();

var connectionString = builder.Configuration.GetConnectionString("SLAware");
builder.Services.AddDbContext<slaware_dataContext>(options =>
{
    options.UseSqlServer(connectionString, sqlServerOptionsAction: sqloptions =>
    {
        sqloptions.EnableRetryOnFailure(maxRetryCount: 60, maxRetryDelay: TimeSpan.FromSeconds(5), errorNumbersToAdd: null);
        sqloptions.MaxBatchSize(20);
    });
});

var host = builder.Build();
host.Run();
