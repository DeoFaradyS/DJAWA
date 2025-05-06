using System.Collections.Generic;

namespace OrderManagementAPI.Models
{
    public static class OrderStatusTransition
    {
        public static Dictionary<OrderStatus, List<OrderStatus>> ValidTransitions = new()
        {
            { OrderStatus.Pending, new List<OrderStatus> { OrderStatus.Diproses } },
            { OrderStatus.Diproses, new List<OrderStatus> { OrderStatus.Dikirim } },
            { OrderStatus.Dikirim, new List<OrderStatus> { OrderStatus.Selesai } },
            { OrderStatus.Selesai, new List<OrderStatus>() }
        };

        public static bool IsValidTransition(OrderStatus current, OrderStatus next)
        {
            return ValidTransitions.ContainsKey(current) && ValidTransitions[current].Contains(next);
        }
    }
}
