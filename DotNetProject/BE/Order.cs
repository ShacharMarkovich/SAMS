using System;
using System.Collections.Generic;

namespace BE
{
    public class Order
    {
        public Order()
        {

        }
        public Order(ICollection<Item> itemList, int orderId, DateTime orderDate)
        {
            ItemList = itemList;
            OrderId = orderId;
            OrderDate = orderDate;
        }
        public int OrderId { get; set; }
        public DateTime OrderDate { get; set; }
        public ICollection<Item> ItemList { get; set; }
    }
}
