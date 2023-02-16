using System;
namespace TradingEngineServer.OrderBook
{
	public interface IMatchingOrderBook : IRetrievalOrderBook
	{
		MatchResult Match();
	}
}

