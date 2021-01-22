using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using BE;
namespace DAL
{
    public class Database : DbContext
    {
        private int asdf;
        public Database () : base("db")
        {
            asdf = 8;
        }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Item> Items { get; set; }
        
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
            SaveChanges();
        }

        public void UpdateItem(Item item)
        {
            Items.AddOrUpdate(item);
            SaveChanges();
        }

        public void AddItemToOrder(Order order, Item item)
        {

        }

        public void DelItemFromOrder(Order order, Item item)
        {
            throw new NotImplementedException();
        }

        public void UpdateOrder(Order order)
        {
            throw new NotImplementedException();
        }

        public void DeleteOrder(Order order)
        {
            throw new NotImplementedException();
        }
    }
}
