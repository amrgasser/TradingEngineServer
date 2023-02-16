using System;
namespace TradingEngineServer.OrderBook
{
	public class OrderBookSpread
	{
		public OrderBookSpread(long? bid, long? ask)
		{
			Bid = bid;
			Ask = ask;
		}
		public long? Bid { get; }
		public long? Ask { get; }
		public long? Spread
		{
			get
			{
				if (Bid.HasValue && Ask.HasValue)
					return (Ask.Value - Bid.Value);
				else
					return null;
			}
		}
	}
}

