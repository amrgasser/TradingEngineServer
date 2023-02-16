using System;
using System.Collections.Generic;
using TradingEngineServer.Orders;

namespace TradingEngineServer.OrderBook
{
	public interface IRetrievalOrderBook : IOrderEntryOrderBook
	{
		public List<OrderBookEntry> GetAskOrders();
		public List<OrderBookEntry> GetBidOrders();
	}
}

