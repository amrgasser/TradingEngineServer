using System;
using System.Collections.Generic;
using System.Text;
using TradingEngineServer.Orders;

namespace TradingEngineServer.OrderBook
{
	public interface IOrderEntryOrderBook :IReadOnlyOrderBook
	{
		void AddOrder(Order order);
		void ChangeOrder(ModifyOrder modifyOrder);
		void RemoveOrder(CancelOrder cancelOrder);
	}
}

