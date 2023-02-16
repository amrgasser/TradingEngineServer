using System;
namespace TradingEngineServer.Orders
{
	public sealed class OrderStatusCreator
	{

		public static CancelOrderStatus GenerateCancelOrderStatus(CancelOrder co)
		{
			return new CancelOrderStatus() ;
		}

        public static NewOrderStatus GenerateNewOrderStatus(Order co)
        {
            return new NewOrderStatus();
        }

        public static ModifyOrderStatus GenerateModifyOrderStatus(ModifyOrder co)
        {
            return new ModifyOrderStatus();
        }
    }
}

