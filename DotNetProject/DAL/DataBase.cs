using System;
using System.Collections.Generic;
using System.Data.Entity;
using BE;
namespace DAL
{
    public class Database : DbContext
    {
        public Database () : base("db")
        {
           
        }
        private DbSet<Order> Orders { get; set; }
        private DbSet<Item> Items { get; set; }
        
        public void AddItem(Item item)
        {
            Items.Add(item);
        }
        public void RemoveItem(Item item)
        {
            Items.Remove(item);
        }
        public Item FindItem(Item item)
        {
            return Items.Find(item);
        }

        public Order FindOrder(Order order)
        {
            return Orders.Find(order);
        }

        public void RemoveOrder(Order order)
        {
            Orders.Remove(order);
        }

        public void AddOrder(Order order)
        {
            Orders.Add(order);
        }
    }
}
