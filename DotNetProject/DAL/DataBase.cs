using System;
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
        public DbSet<Item> Itmes { get; set; }

    }
}
