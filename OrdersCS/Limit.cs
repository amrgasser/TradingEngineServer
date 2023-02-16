using System;
using System.Collections.Generic;

namespace TradingEngineServer.Orders
{
	public class Limit
	{
		public long Price { get; private set; }
		public OrderBookEntry Head { get; set; }
		public OrderBookEntry Tail { get; set; }
		public uint GetLevelOrderCount()
		{
			uint orderCount = 0;
			OrderBookEntry headPointer = Head;
			while(headPointer != null)
			{
				if(headPointer.CurrentOrder.CurrentQuantity != 0)
					orderCount++;
				headPointer = headPointer.Next;
				
			}
			return orderCount;
		}

		public uint GetLevelOrderQuantity()
		{
            uint orderQuantity = 0;
            OrderBookEntry headPointer = Head;
            while (headPointer != null)
            {

                orderQuantity += headPointer.CurrentOrder.CurrentQuantity;
                headPointer = headPointer.Next;

            }
            return orderQuantity;

        }

		public List<OrderRecord> GetLevelOrderRecords()
		{
			List<OrderRecord> orderRecords = new List<OrderRecord>();
			OrderBookEntry headPointer = Head;
			uint theoreticalQueuePosition = 0;
			while(headPointer != null)
			{
				var currentOrder = headPointer.CurrentOrder;
				if(currentOrder.CurrentQuantity != 0)
					orderRecords.Add(
						new OrderRecord(
							currentOrder.OrderId,
							currentOrder.CurrentQuantity,
                            Price,
							currentOrder.IsBuySide,
							currentOrder.Username,
							currentOrder.SecurityId,
							theoreticalQueuePosition
							));
				theoreticalQueuePosition++;
				headPointer = headPointer.Next;

			}

			return orderRecords;

        }

		public bool isEmpty
		{
			get
			{
				return Head == null && Tail == null;
			}
		}
		public Side Side
		{
			get
			{
				if (isEmpty)
					return Side.Unknown;
				else
				{
					return Head.CurrentOrder.IsBuySide ? Side.Bid : Side.Ask;
				}
			}
		}
		

		public Limit(long price)
		{
			Price = price;
		}
	}
}

