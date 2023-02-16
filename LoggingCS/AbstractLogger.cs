using System;
namespace TradingEngineServer.Logging
{
	public abstract class AbstractLogger: ILogger
	{
        public void Debug(string module, string message) => Log(LogLevel.Debug, module, message);

        public void Debug(string module, Exception e) => Log(LogLevel.Debug, module, $"{e}");

        public void Error(string module, string message) => Log(LogLevel.Error, module, message);


        public void Error(string module, Exception e) => Log(LogLevel.Error, module, $"{e}");

        public void Information(string module, string message) => Log(LogLevel.Information, module, message);


        public void Information(string module, Exception e) => Log(LogLevel.Information, module, $"{e}");


        public void Warning(string module, string message) => Log(LogLevel.Warning, module, message);


        public void Warning(string module, Exception e) => Log(LogLevel.Warning, module, $"{e}");

        protected abstract void Log(LogLevel loglevel, string module, string message);

        protected AbstractLogger()
        {

        }
	}
}

