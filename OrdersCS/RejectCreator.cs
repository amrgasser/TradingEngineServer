using System;

namespace TradingEngineServer.Orders
{
	public sealed class RejectCreator
	{

		public static Rejection GenerateOrderCoreRejection(IOrderCore rejectedOrder, RejectionReason rejectionReason)
		{
			return new Rejection(rejectedOrder, rejectionReason);
		}
	}
}
