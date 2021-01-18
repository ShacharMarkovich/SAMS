using System;
using System.Collections.Generic;

namespace BE
{
    public class Order
    {
        public Order()
        {

        }
        public Order(List<Item> itemList, int orderId, DateTime orderDate)
        {
            ItemList = itemList;
            OrderId = orderId;
            OrderDate = orderDate;
        }
        public int OrderId { get; set; }
        public DateTime OrderDate { get; set; }
        public List<Item> ItemList { get; set; }
        public override String ToString()
        {
            return Convert.ToString(OrderId)+ " " + OrderDate.ToString();
        }
    }
}
