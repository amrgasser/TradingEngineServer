using System;

using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using TradingEngineServer.Core.Configuration;
using TradingEngineServer.Logging;
using TradingEngineServer.Logging.LoggingConfiguration;

namespace TradingEngineServer.Core
{
	public class TradingEngineServerHostBuilder
	{
		public static IHost BuildingTradingEngineServer()
			=> Host.CreateDefaultBuilder().ConfigureServices((context, services) =>
			{
				//Configuration
				services.AddOptions();
				services.Configure<TradingEngineServerConfiguration>(context.Configuration.GetSection(nameof(TradingEngineServerConfiguration)));
                services.Configure<LoggingConfiguration>(context.Configuration.GetSection(nameof(LoggingConfiguration)));
                // Singleton Objects
                services.AddSingleton<ITradingEngineServer, TradingEngineServer>();
				services.AddSingleton<ITextLogger, TextLogger>();
				//hosted service
				services.AddHostedService<TradingEngineServer>();

			}).Build();
	}
}

