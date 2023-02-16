using System;

namespace TradingEngineServer.Orders
{
	public class Rejection : IOrderCore
	{
		public Rejection(IOrderCore rejectedOrder, RejectionReason rejectionReason)
		{
			RejectionReason = rejectionReason;

			_orderCore = rejectedOrder;
		}
		public RejectionReason RejectionReason { get; }

		public long OrderId => _orderCore.OrderId;
		public string Username => _orderCore.Username;
        public int SecurityId => _orderCore.SecurityId;

        private readonly IOrderCore _orderCore;
    }
}

