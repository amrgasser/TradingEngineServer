using System;
namespace TradingEngineServer.Orders
{
	public enum RejectionReason
	{
		Unknown,
		OrderNotFound,
		InstrumentNotFound,
		AttemptingToModifyWrongSide
	}
}

