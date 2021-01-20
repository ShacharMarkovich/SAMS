using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace BE
{
    public class Order
    {
        public Order() { }

        public Order(string storeName, DateTime orderDate)
        {
            StoreName = storeName;
            OrderDate = orderDate;
        }

        public Order(string storeName, int orderId, DateTime orderDate, List<Item> itemList)
        {
            StoreName = storeName;
            OrderId = orderId;
            OrderDate = orderDate;
            ItemList = itemList;
        }

        public string StoreName { get; set; }
        [System.ComponentModel.DataAnnotations.Key] // make it the table key
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // make the DB generate it value by itself
        public int OrderId { get; set; }
        
        public DateTime OrderDate { get; set; }
        
        public List<Item> ItemList { get; set; }

        public override String ToString() => Convert.ToString(OrderId) + " " + OrderDate.ToString();
    }
}
