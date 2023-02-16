using System;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

using TradingEngineServer.Core.Configuration;
using TradingEngineServer.Logging;


namespace TradingEngineServer.Core
{
	sealed class TradingEngineServer : BackgroundService, ITradingEngineServer
	{
        private readonly ITextLogger _logger;
        private readonly TradingEngineServerConfiguration _tradingEngineServerConfiguration;

        public TradingEngineServer(
            ITextLogger logger,
            IOptions<TradingEngineServerConfiguration> config)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _tradingEngineServerConfiguration = config.Value ?? throw new ArgumentNullException(nameof(config));
        }

        public Task Run(CancellationToken token) => ExecuteAsync(token);
     
        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.Information(nameof(TradingEngineServer), "Starting Trading Engine");
            while (!stoppingToken.IsCancellationRequested)
            {

            }
            _logger.Information(nameof(TradingEngineServer), "Stoping Trading Engine");
            return Task.CompletedTask;
        }
    }
}

