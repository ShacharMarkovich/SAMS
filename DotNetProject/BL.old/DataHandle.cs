using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using BE;
using DAL;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace BL
{
    public class DataHandle
    {
        private Database db;

        public DataHandle() => db = new Database();
        public void ClearAllData() => db.ClearAllData();

        #region Item functions
        public Item AddItem(Item item) => db.AddItem(item);

        public void UpdateItem(Item item) => db.UpdateItem(item);

        public Item RemoveItem(Item item) => db.RemoveItem(item);

        public ICollection<Item> GetAllItems() => db.Set<Item>().ToList();
        #endregion


        #region Order functions
        public Order AddOrder(Order order) => db.AddOrder(order);

        public void RemoveOrder(Order order) => db.RemoveOrder(order);

        public void UpdateOrder(Order order) => db.UpdateOrder(order);

        public ICollection<Order> GetOrders() => db.Set<Order>().ToList();
        #endregion


        #region Items in Orders functions

        public void AddItemToOrder(Item item, Order order) => AddItemToOrder(item, order, 0);
        public void AddItemToOrder(Item item, Order order, int quantity)
        {
            Item newItem = new Item(item);
            newItem.Quantity = quantity;
            db.AddItemToOrder(newItem, order);
        }

        //public void RemoveItemFromOrder(Item item, Order order)
        //{
        //    List<Item> items = order.Items.FindAll(it => it.BarcodeNumber == item.BarcodeNumber);
        //    order.Items.RemoveAll(it => it.BarcodeNumber == item.BarcodeNumber);
        //    foreach (Item entity in items)
        //        db.RemoveItem(entity);
        //}

        public List<Item> GetItemsInOrder(Order order) => db.GetItemsInOrder(order);

        //public Order DeleteOrder(Order order)
        //{
        //    List<Item> items = order.Items.FindAll(_ => true);
        //    order.Items.Clear();
        //    foreach (Item entity in items)
        //        db.RemoveItem(entity);

        //    return db.RemoveOrder(order);
        //}

        #endregion

        /// <summary>
        /// parse bitmaps to Orders array
        /// </summary>
        /// <param name="b"></param>
        /// <returns>updated Order</returns>
        public List<Order> parseBitmapList(List<Bitmap> QRlist, Order o)
        {
            List<QRCode> itemList = new List<QRCode>();
            List<Order> orderList = new List<Order>();

            foreach (Bitmap qr in QRlist)
            {
                QRCodeDecoder decoder = new QRCodeDecoder();
                QRCodeBitmapImage b = new QRCodeBitmapImage(qr);
                string result = decoder.Decode(b);
                itemList.Add(JsonConvert.DeserializeObject<QRCode>(result));
            }
            foreach (QRCode i in itemList)
            {
                int index = orderList.FindIndex(item => item.OrderDate == i.date && item.StoreName == i.Store);
                if (index > 0)
                {
                    orderList[index].Items.Add(i);
                }
                else
                {
                    orderList.Add(new Order() { OrderDate = i.date, StoreName = i.Store, Items = new List<Item>() { i } }) ;
                }
            }
            return orderList;
        }
    }
}
