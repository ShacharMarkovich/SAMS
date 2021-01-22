using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;
using DAL;
using BL;
namespace TEST
{
    class Program
    {
        static void Main(string[] args)
        {
            Item item1 = new Item() { BarcodeNumber = 1, Quantity = 1, ItemName = "1", ItemPrice = 1, ItemPic = "image1" };
            Item item2 = new Item() { BarcodeNumber = 2, Quantity = 2, ItemName = "2", ItemPrice = 2, ItemPic = "image2" };
            Item item3 = new Item() { BarcodeNumber = 3, Quantity = 3, ItemName = "3", ItemPrice = 3, ItemPic = "image3" };

            Order order1 = new Order() { StoreName = "store1", OrderDate = DateTime.Now, ItemList = new List<Item>() { item1 } };
            Order order2 = new Order() { StoreName = "store2", OrderDate = DateTime.Now, ItemList = new List<Item>() { item2 } };

            DataHandle bl = new DataHandle();
            bl.InsertOrder(order1);
            bl.InsertOrder(order2);
            ICollection<Order> orders = bl.GetOrders();
            bl.AddItemToOrder(order1, item3);
            bl.DelItemFromOrder(order1, item1);
            item1.ItemName = "new item name";
            bl.UpdateItem(item1);

            order1.StoreName = "new name";
            bl.UpdateOrder(order1);

            bl.DeleteOrder(order2);
        }
    }
}
