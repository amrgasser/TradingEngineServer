using System;
namespace TradingEngineServer.Orders
{
	// Read only representation of an order
	public record OrderRecord(long OrderId, uint Quantity,
		long Price, bool isBuySide, string username, int SecurityId,
		uint TheoreticalQueuePosition
		);
}

namespace System.Runtime.CompilerServices
{
	internal static class IsExternalInit { };
}

