using System;
using System.Collections.Generic;
using System.Linq;
using BE;
using DAL;
namespace BL
{
    public class DataHandle
    {
        Database db;
        public DataHandle()
        {
            Database db = new Database();
        }
        #region Item
        public void InsertItem(Item item)
        {
            db.AddItem(item);
        }
        public void RemoveItem(Item item)
        {
            db.RemoveItem(item);
        }
        public void UpdateItem(Item item)
        {
            var entity = db.FindItem(item);
            if (entity == null)
            {
                //TODO: create NOTFOUNDEXCEPTION 
                throw new Exception("Item Not Found");
             }
            else
            {
                db.Entry(entity).CurrentValues.SetValues(item);
            }
        }
        public ICollection<Item> getAllItems()
        {
            return db.Set<Item>().ToList();
        }
        #endregion
        # region Order
        public void GetOrders()
        {
           
        }
        public void InsertOrder(Order order)
        {

        }
        public void RemoveOrder(Order order)
        {

        }
        public void UpdateOrder(Order order)
        {

        }
        #endregion
    }
}
