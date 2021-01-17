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
    }
}
