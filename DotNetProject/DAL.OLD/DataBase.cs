using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using BE;

namespace DAL
{
    public class Database : DbContext
    {

        public Database() : base("SAMS DB") { }

        public DbSet<Order> Orders { get; set; }
        public DbSet<Item> Items { get; set; }

        public void ClearAllData()
        {
            foreach (var entity in Items)
                Items.Remove(entity);

            foreach (var entity in Orders)
                Orders.Remove(entity);

            SaveChanges();
        }

        #region Items functions
        public Item AddItem(Item item)
        {
            var entity = Items.Add(item);
            SaveChanges();
            return entity;
        }

        public void UpdateItem(Item item)
        {
            Items.AddOrUpdate(item);
            SaveChanges();
        }

        public Item RemoveItem(Item item)
        {
            var entity = Items.Remove(item);
            SaveChanges();
            return entity;
        }

        public Item FindItem(Item item) => Items.Find(item.ItemId);
        #endregion


        #region Orders functions
        public Order AddOrder(Order order)
        {
            var entity = Orders.Add(order);
            SaveChanges();
            return entity;
        }

        public void UpdateOrder(Order order)
        {
            Orders.AddOrUpdate(order);
            SaveChanges();
        }

        public Order RemoveOrder(Order order)
        {
            var entity = Orders.Remove(order);
            SaveChanges();
            return entity;
        }

        public Order FindOrder(Order order) => Orders.Find(order.OrderId);
        #endregion

        public List<Item> GetItemsInOrder(Order order) => order.Items.ToList();

        public void AddItemToOrder(Item item, Order order)
        {
            order.Items.Add(item);
            SaveChanges();
        }
    }
}
