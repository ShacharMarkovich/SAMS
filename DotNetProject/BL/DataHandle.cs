using System;
using System.Collections.Generic;
using System.Linq;
using BE;
using DAL;
namespace BL
{
    public class DataHandle
    {
        private Database db;
        
        public DataHandle() => db = new Database();

        #region Item
        public void InsertItem(Item item) => db.AddItem(item);
        
        public void RemoveItem(Item item) => db.RemoveItem(item);
        
        public void UpdateItem(Item item)
        {
            var entity = db.FindItem(item);
            if (entity == null)
                //TODO: create NOTFOUNDEXCEPTION 
                throw new Exception("Item Not Found");
            else
                db.Entry(entity).CurrentValues.SetValues(item);
        }
        
        public ICollection<Item> getAllItems() => db.Set<Item>().ToList();
        #endregion

        # region Order
        public ICollection<Order> GetOrders() => db.Set<Order>().ToList();
        
        public void InsertOrder(Order order) => db.AddOrder(order);
        
        public void RemoveOrder(Order order) => db.RemoveOrder(order);
        
        public void UpdateOrder(Order order)
        {
            var entity = db.FindOrder(order);
            if (entity == null)
                //TODO: create NOTFOUNDEXCEPTION 
                throw new Exception("Order Not Found");
            else
                db.Entry(entity).CurrentValues.SetValues(order);
        }
        #endregion
    }
}
