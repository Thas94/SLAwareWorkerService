using SLAwareWorkerService.Interfaces;

namespace SLAwareWorkerService
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly ISlaSeverityService _slaSeverityService;
        public Worker(ILogger<Worker> logger, ISlaSeverityService slaSeverityService)
        {
            _logger = logger;
            _slaSeverityService = slaSeverityService;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                if (_logger.IsEnabled(LogLevel.Information))
                {
                    _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                }

                _slaSeverityService.SlaTracking();

                await Task.Delay(TimeSpan.FromMinutes(5), stoppingToken);
            }
        }
    }
}
