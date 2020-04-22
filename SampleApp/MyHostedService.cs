using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace SampleApp
{
    public class MyHostedService : IHostedService
    {
        private readonly ILogger _logger;

        public MyHostedService(ILogger logger)
        {
            _logger = logger;
        }

        public Task StartAsync(CancellationToken cancellationToken)
            => Task.Run(() => _logger.LogInformation($"{nameof(MyHostedService)} started"));

        public Task StopAsync(CancellationToken cancellationToken)
            => Task.Run(() => _logger.LogInformation($"{nameof(MyHostedService)} stopped"));
    }
}
