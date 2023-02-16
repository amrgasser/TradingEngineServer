using System;
using System.Collections.Generic;
using System.Linq;
using TradingEngineServer.Orders;
using TradingServerEngine.Instrument;

namespace TradingEngineServer.OrderBook
{
	public class OrderBook : IRetrievalOrderBook
	{
        private readonly Security _instrument;
        private readonly Dictionary<long, OrderBookEntry> _orders = new Dictionary<long, OrderBookEntry>();
        private readonly SortedSet<Limit> _askLimits = new SortedSet<Limit>(AskLimitComparer.Comparer);
        private readonly SortedSet<Limit> _bidLimits = new SortedSet<Limit>(BidLimitComparer.Comparer);
        

        public OrderBook(Security instrument)
		{
            _instrument = instrument;
		}

        public int Count => _orders.Count;

        public void AddOrder(Order order)
        {
            var baseLimit = new Limit(order.Price);
            AddOrder(order, baseLimit, order.IsBuySide ? _bidLimits : _askLimits, _orders);

        }

        private static void AddOrder(Order order, Limit baseLimit, SortedSet<Limit> limitlevels, Dictionary<long, OrderBookEntry> internalBook)
        {
            OrderBookEntry orderBookEntry = new OrderBookEntry(order, baseLimit);
            if (limitlevels.TryGetValue(baseLimit, out Limit limit))
            {
                if(limit.isEmpty)
                {
                    limit.Head = orderBookEntry;
                    limit.Tail = orderBookEntry;
                }
                OrderBookEntry tailPointer = limit.Tail;
                tailPointer.Next = orderBookEntry;
                orderBookEntry.Previous = tailPointer;
                limit.Tail = orderBookEntry;

            }
            else
            {
                limitlevels.Add(baseLimit);
                baseLimit.Head = orderBookEntry;
                baseLimit.Tail = orderBookEntry;
           
            }
            internalBook.Add(order.OrderId, orderBookEntry);
        }

        public void ChangeOrder(ModifyOrder modifyOrder)
        {
            if(_orders.TryGetValue(modifyOrder.OrderId, out OrderBookEntry obe))
            {
                RemoveOrder(modifyOrder.ToCancelOrder());
                AddOrder(modifyOrder.ToNewOrder(), obe.ParentLimit, modifyOrder.IsBuySide ? _bidLimits : _askLimits, _orders);
            }
        }

        public bool ContainsOrder(long orderId)
        {
            return _orders.ContainsKey(orderId);
        }

        public List<OrderBookEntry> GetAskOrders()
        {
            List<OrderBookEntry> orderBookEntries = new List<OrderBookEntry>();
            foreach (var bid in _bidLimits)
            {
                if (bid.isEmpty) continue;
                else
                {
                    OrderBookEntry bidPointer = bid.Head;
                    while (bidPointer != null)
                    {
                        orderBookEntries.Add(bidPointer);
                        bidPointer = bidPointer.Next;
                    }
                }
            }
            return orderBookEntries;
        }

        public List<OrderBookEntry> GetBidOrders()
        {
            List<OrderBookEntry> orderBookEntries = new List<OrderBookEntry>();
            foreach(var ask in _askLimits)
            {
                if (ask.isEmpty) continue;
                else
                {
                    OrderBookEntry askPointer = ask.Head;
                    while(askPointer != null)
                    {
                        orderBookEntries.Add(askPointer);
                        askPointer = askPointer.Next;
                    }
                }
            }
            return orderBookEntries;
        }

        public OrderBookSpread GetSpread()
        {
            long? bestAsk = null, bestBid = null;

            if(_askLimits.Any() && !_askLimits.Min.isEmpty)
            {
                bestAsk = _askLimits.Min.Price;
            }
            if(_bidLimits.Any() && !_bidLimits.Max.isEmpty)
            {
                bestBid = _bidLimits.Max.Price;
            }
            return new OrderBookSpread(bestBid, bestAsk);
        }

        public void RemoveOrder(CancelOrder cancelOrder)
        {
            if (_orders.TryGetValue(cancelOrder.OrderId, out OrderBookEntry obe))
            {
                RemoveOrder(cancelOrder.OrderId, obe, _orders);
            }
        }

        private static void RemoveOrder(long orderId, OrderBookEntry obe, Dictionary<long, OrderBookEntry> internalBook)
        {
            // LinkedList fix
            if(obe.Previous != null && obe.Next != null)
            {
                obe.Next.Previous = obe.Previous;
                obe.Previous.Next = obe.Next;
            }
            else if(obe.Previous != null)
            {
                obe.Previous.Next = null;
            }
            else if(obe.Next != null)
            {
                obe.Next.Previous = null;
            }

            // OrderBookEntry on Limit Level
            if(obe.ParentLimit.Head == obe && obe.ParentLimit.Tail == obe)
            {
                obe.ParentLimit.Head = null;
                obe.ParentLimit.Tail = null;
            }
            else if(obe.ParentLimit.Head == obe)
            {
                obe.ParentLimit.Head = obe.Next;
            }else if(obe.ParentLimit.Tail == obe)
            {
                obe.ParentLimit.Tail = obe.Previous;
            }

            internalBook.Remove(orderId);

        }
    }
}

