using System;
namespace TradingEngineServer.Orders
{
	public class OrderBookEntry
	{
		public OrderBookEntry(Order currentOrder, Limit parentLimit)
		{
			CreationTime = DateTime.UtcNow;
			ParentLimit = parentLimit;
			CurrentOrder = currentOrder;
		}

		public DateTime CreationTime { get; }
		public Order CurrentOrder { get; }
		public Limit ParentLimit { get; }
		public OrderBookEntry Next { get; set }
		public OrderBookEntry Previous { get; set }
	}
}

