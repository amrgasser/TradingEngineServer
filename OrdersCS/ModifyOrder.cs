using System;

namespace TradingEngineServer.Orders
{
	public class ModifyOrder : IOrderCore
	{
		public ModifyOrder(IOrderCore orderCore, long modifyPrice, uint modifyQuantity, bool isBuySide)
		{
			Price = modifyPrice;
			Quantity = modifyQuantity;
			IsBuySide = isBuySide;

			_orderCore = orderCore; 

		}
		//Props
		public long Price { get; }
		public uint Quantity { get; }
		public bool IsBuySide { get; }
		public long OrderId => _orderCore.OrderId;
		public string Username => _orderCore.Username;
		public int SecurityId => _orderCore.SecurityId;

		//Methods
		public CancelOrder ToCancelOrder()
		{
			return new CancelOrder(this);
		}

		public Order ToNewOrder()
		{
			return new Order(this);
		}


		private readonly IOrderCore _orderCore;
	}

}

