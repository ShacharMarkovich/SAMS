using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Reflection;
using BE;
using DAL;
using MessagingToolkit.QRCode.Codec;
using MessagingToolkit.QRCode.Codec.Data;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using static System.Net.Mime.MediaTypeNames;

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

        public void UpdateItemInOrder(Item item, Order order) => AddItemToOrder(item, order, 0);
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
        public List<int> GetOrdersYear() => GetOrders().Select(order => order.OrderDate.Year).Distinct().ToList();

        #endregion

        public static List<Bitmap> loadQRBitmaps()
        {
            //GenerateQRcodes();

            string fullPath = System.IO.Path.GetDirectoryName(Assembly.GetEntryAssembly().Location)+ @"\QRCodes\";

            string[] Files = Directory.GetFiles(fullPath);
            List<Bitmap> b = new List<Bitmap>();
            foreach (string f in Files)
            {
                Bitmap tmp = new Bitmap(f);
                tmp.Tag = Directory.GetCreationTime(f).ToString();
                b.Add(tmp);
            }
            return b;
        }
        /// <summary>
        /// parse bitmaps to Orders array
        /// </summary>
        /// <param name="b"></param>
        /// <returns>updated Order</returns>
        public static List<Order> parseBitmapList(List<Bitmap> QRlist)
        {
            List<QRCode> itemList = new List<QRCode>();
            List<Order> orderList = new List<Order>();

            foreach (Bitmap qr in QRlist)
            {
                QRCodeDecoder decoder = new QRCodeDecoder();
                QRCodeBitmapImage b = new QRCodeBitmapImage(qr);
                string result = decoder.Decode(b);
                QRCode qR = JsonConvert.DeserializeObject<QRCode>(result);
                qR.date = DateTime.Parse(qr.Tag.ToString()).Date;
                itemList.Add(qR);
                qr.Dispose();
            }
            foreach (QRCode i in itemList)
            {
                int index = orderList.FindIndex(item => item.OrderDate == i.date && item.StoreName == i.Store);
                if (index > -1)
                    orderList[index].Items.Add(i.item);
                else
                    orderList.Add(new Order() { OrderDate = i.date, StoreName = i.Store, Items = new List<Item>() { i.item } });
            }
            return orderList;
        }
        public static void clearQRcodesDir()
        {
            string fullPath = System.IO.Path.GetDirectoryName(Assembly.GetEntryAssembly().Location) + @"\QRCodes\";
            string[] Files = Directory.GetFiles(fullPath);

            foreach (string f in Files)
            {
                File.Delete(f);
            }
        }
        public static void GenerateQRcodes()
        {
            List<Item> items = new List<Item> {
            new Item() { BarcodeNumber = 1, ItemName = "לחם", ItemPrice = 5.5 },
            new Item() { BarcodeNumber = 2, ItemName = "מלפפון", ItemPrice = 2 },
            new Item() { BarcodeNumber = 3, ItemName = "שוקולד", ItemPrice = 5 },
            new Item() { BarcodeNumber = 4, ItemName = "עגבניה", ItemPrice = 3 },
            new Item() { BarcodeNumber = 5, ItemName = "קיסמי עץ", ItemPrice = 1 },
            new Item() { BarcodeNumber = 6, ItemName = "עט", ItemPrice = 3 },
            new Item() { BarcodeNumber = 7, ItemName = "עיפרון", ItemPrice = 1 },
            new Item() { BarcodeNumber = 8, ItemName = "דג", ItemPrice = 30 },
            new Item() { BarcodeNumber = 9, ItemName = "בשר", ItemPrice = 40 },
            new Item() { BarcodeNumber = 10,  ItemName = "עוף", ItemPrice = 15 },
            new Item() { BarcodeNumber = 11,  ItemName = "קולה", ItemPrice = 5.6 },
            new Item() { BarcodeNumber = 12,  ItemName = "ספרינג ענבים", ItemPrice = 4 },
            new Item() { BarcodeNumber = 13,  ItemName = "ספרייט", ItemPrice = 5.6 },
            new Item() { BarcodeNumber = 14,  ItemName = "ציפס חטיף", ItemPrice = 4 },
            new Item() { BarcodeNumber = 15,  ItemName = "ציטוס", ItemPrice = 4 },
            new Item() { BarcodeNumber = 16,  ItemName = "קליק", ItemPrice = 5 },
            new Item() { BarcodeNumber = 17,  ItemName = "פאנטה", ItemPrice = 4 },
            new Item() { BarcodeNumber = 18,  ItemName = "טישו", ItemPrice = 2 },
            new Item() { BarcodeNumber = 19,  ItemName = "מסטיק", ItemPrice = 2 },
            new Item() { BarcodeNumber = 20,  ItemName = "פסטה", ItemPrice = 3 },
            new Item() { BarcodeNumber = 21,  ItemName = "טונה", ItemPrice = 5.5 },
            new Item() { BarcodeNumber = 22,  ItemName = "חלב", ItemPrice = 5.5 },
            new Item() { BarcodeNumber = 23,  ItemName = "בורקס", ItemPrice = 40 },
            new Item() { BarcodeNumber = 24,  ItemName = "כוסות חדפ", ItemPrice = 3 },
            new Item() { BarcodeNumber = 25,  ItemName = "צלחות חדפ", ItemPrice = 4 },
            new Item() { BarcodeNumber = 26,  ItemName = "מנה חמה", ItemPrice = 5 },
            new Item() { BarcodeNumber = 27,  ItemName = "נמס בכוס", ItemPrice = 5 },
            new Item() { BarcodeNumber = 28,  ItemName = "פלפל שחור", ItemPrice = 20 },
            new Item() { BarcodeNumber = 29,  ItemName = "פפריקה", ItemPrice = 20 },
            new Item() { BarcodeNumber = 30,  ItemName = "כמון", ItemPrice = 25 }};

            List<QRCode> qRCodes = new List<QRCode>() {
                new QRCode() { Store = "רמי לוי" },// date=DateTime.Now.AddHours(-50)},
                new QRCode() { Store = "שופרסל" },// date=DateTime.Now.AddHours(-23)},
                new QRCode() { Store = "ויקטורי" },// date=DateTime.Now.AddHours(-43)},
                new QRCode() { Store = "יוחננוף" },// date=DateTime.Now.AddHours(-67)},
                new QRCode() { Store = "ויקטורי" },// date=DateTime.Now.AddHours(-54)},
                new QRCode() { Store = "יש חסד" } };// date=DateTime.Now.AddHours(-54)},

            Random rand = new Random(DateTime.Now.ToString().GetHashCode());
            for (int j = 0; j < 50; j++)
            {
                for (int i = 0; i < rand.Next(qRCodes.Count); i++)
                {
                    qRCodes[i].item = items[rand.Next(items.Count)];
                    string output = JsonConvert.SerializeObject(qRCodes[i]);
                    Console.WriteLine(output);
                    QRCodeEncoder encoder = new QRCodeEncoder();
                    var encoded = encoder.Encode(output);
                    string fullPath = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
                    encoded.Save(fullPath + @"\QRCodes\" + i.ToString() + j.ToString() + ".jpg", ImageFormat.Jpeg);

                }
            }
        }
    }
}
