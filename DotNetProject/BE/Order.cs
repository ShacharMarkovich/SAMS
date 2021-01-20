using System;
using System.Collections.Generic;

namespace BE
{
    public class Order
    {
        public Order() { }

        public Order(string storeName, int orderId, DateTime orderDate, List<Item> itemList)
        {
            StoreName = storeName;
            OrderId = orderId;
            OrderDate = orderDate;
            ItemList = itemList;
        }

        public string StoreName { get; set; }
        
        public int OrderId { get; set; }
        
        public DateTime OrderDate { get; set; }
        
        public List<Item> ItemList { get; set; }

        public override String ToString() => Convert.ToString(OrderId) + " " + OrderDate.ToString();
    }
}
