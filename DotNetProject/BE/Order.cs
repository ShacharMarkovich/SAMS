using SQLite;
using System;
using System.Collections.Generic;

namespace BE
{
    [Table("Order")]
    public class Order
    {
        public Order(int orderId, DateTime orderDate)
        {
            OrderId = orderId;
            OrderDate = orderDate;
            ItemList = new LinkedList<Item>();
        }
        public Order(int orderId, DateTime orderDate, LinkedList<Item> list)
        {
            OrderId = orderId;
            OrderDate = orderDate;
            ItemList = list;
        }
        public Order()
        {
        }
        public void AddItem(Item item)
        {
            ItemList.AddLast(item);
        }
        [PrimaryKey, AutoIncrement]
        public int OrderId { get; set; }
        public DateTime OrderDate { get; set; }
        [Ignore]
        public LinkedList<Item> ItemList { get; set; }
    }
}
