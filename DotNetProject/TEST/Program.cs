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
            DataHandle bl = new DataHandle();
            bl.ClearAllData();

            Item item1 = new Item() { BarcodeNumber = 1, ItemName = "1", ItemPrice = 1, ItemPic = "1" };
            Item item2 = new Item() { BarcodeNumber = 2, ItemName = "2", ItemPrice = 2, ItemPic = "2" };
            Item item3 = new Item() { BarcodeNumber = 3, ItemName = "3", ItemPrice = 3, ItemPic = "3" };
            Item item4 = new Item() { BarcodeNumber = 4, ItemName = "4 - 2 del", ItemPrice = 4, ItemPic = "4 - 2 del" };

            Order order1 = new Order("name1", DateTime.Now);
            Order order2 = new Order("name2", DateTime.Now);
            Order order3 = new Order("to delete", DateTime.Now);

            
            item1 = bl.AddItem(item1);
            item2 = bl.AddItem(item2);
            item3 = bl.AddItem(item3);
            item4 = bl.AddItem(item4);

            order1 = bl.AddOrder(order1);
            order2 = bl.AddOrder(order2);
            order3 = bl.AddOrder(order3);

            bl.RemoveItem(item4);
            bl.RemoveOrder(order3);

            item2.ItemName = "new name";
            bl.UpdateItem(item2);

            order2.StoreName = "new store name";
            bl.UpdateOrder(order2);

            bl.AddItemToOrder(item1, order1, 1);
            bl.AddItemToOrder(item2, order1, 2);
            bl.AddItemToOrder(item3, order1, 3);
            bl.AddItemToOrder(item2, order2, 4);

            bl.RemoveItemFromOrder(item3, order1);

            List<Item> itemsInOrder = bl.GetItemsInOrder(order1);
            foreach (var item in itemsInOrder)
                Console.WriteLine(item);
            Console.ReadLine();
            
            bl.DeleteOrder(order1);
            Console.ReadLine();
        }
    }
}
