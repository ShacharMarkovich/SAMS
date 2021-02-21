using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Reflection;
using BE;
using MessagingToolkit.QRCode.Codec;
using MessagingToolkit.QRCode.Codec.Data;
using Newtonsoft.Json;

namespace BL
{
    public class DataHandle
    {
        #region singleton
        private DAL.Database db;
        private static DataHandle _instance = null;
        public static DataHandle Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new DataHandle();
                return _instance;
            }
        }

        private DataHandle() => db = DAL.FactoryDAL.Instance;
        #endregion

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
            //Create folder if not exsists TODO
            string fullPath = System.IO.Path.GetDirectoryName(Assembly.GetEntryAssembly().Location) + @"\QRCodes\";

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
            foreach (QRCode itemQRCode in itemList)
            {
                int index = orderList.FindIndex(item => item.OrderDate == itemQRCode.date && item.StoreName == itemQRCode.Store);
                if (index > -1)
                    orderList[index].Items.Add(itemQRCode.item);
                else
                    orderList.Add(new Order() { OrderDate = itemQRCode.date, StoreName = itemQRCode.Store, Items = new List<Item>() { itemQRCode.item } });
            }
            ClearQRcodesDir();
            return orderList;
        }
        public static void ClearQRcodesDir()
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
            int barcodeNumber = 1;
            List<Item> items = new List<Item> {
            new Item() { BarcodeNumber = barcodeNumber++,  ItemName = "לחם", ItemPrice = 5.5 },
            new Item() { BarcodeNumber = barcodeNumber++,  Category = Category.Food , ItemName = "מלפפון", ItemPrice = 2 },
            new Item() { BarcodeNumber = barcodeNumber++,  Category = Category.Food ,ItemName = "שוקולד", ItemPrice = 5 },
            new Item() { BarcodeNumber = barcodeNumber++,  Category = Category.Food ,ItemName = "עגבניה", ItemPrice = 3 },
            new Item() { BarcodeNumber = barcodeNumber++,  Category = Category.Other ,ItemName = "קיסמי עץ", ItemPrice = 1 },
            new Item() { BarcodeNumber = barcodeNumber++,  Category = Category.Stationery ,ItemName = "עט", ItemPrice = 3 },
            new Item() { BarcodeNumber = barcodeNumber++,  Category = Category.Stationery ,ItemName = "עיפרון", ItemPrice = 1 },
            new Item() { BarcodeNumber = barcodeNumber++,  Category = Category.Food ,ItemName = "דג", ItemPrice = 30 },
            new Item() { BarcodeNumber = barcodeNumber++,  Category = Category.Food ,ItemName = "בשר", ItemPrice = 40 },
            new Item() { BarcodeNumber = barcodeNumber++,  Category = Category.Food ,ItemName = "עוף", ItemPrice = 15 },
            new Item() { BarcodeNumber = barcodeNumber++,  Category = Category.Food ,ItemName = "קולה", ItemPrice = 5.6 },
            new Item() { BarcodeNumber = barcodeNumber++,  Category = Category.Food ,ItemName = "ספרינג ענבים", ItemPrice = 4 },
            new Item() { BarcodeNumber = barcodeNumber++,  Category = Category.Food ,ItemName = "ספרייט", ItemPrice = 5.6 },
            new Item() { BarcodeNumber = barcodeNumber++,  Category = Category.Food ,ItemName = "ציפס חטיף", ItemPrice = 4 },
            new Item() { BarcodeNumber = barcodeNumber++,  Category = Category.Food ,ItemName = "ציטוס", ItemPrice = 4 },
            new Item() { BarcodeNumber = barcodeNumber++,  Category = Category.Food ,ItemName = "קליק", ItemPrice = 5 },
            new Item() { BarcodeNumber = barcodeNumber++,  Category = Category.Food ,ItemName = "פאנטה", ItemPrice = 4 },
            new Item() { BarcodeNumber = barcodeNumber++,  Category = Category.Other ,ItemName = "טישו", ItemPrice = 2 },
            new Item() { BarcodeNumber = barcodeNumber++,  Category = Category.Food ,ItemName = "מסטיק", ItemPrice = 2 },
            new Item() { BarcodeNumber = barcodeNumber++,  Category = Category.Food ,ItemName = "פסטה", ItemPrice = 3 },
            new Item() { BarcodeNumber = barcodeNumber++,  Category = Category.Food ,ItemName = "טונה", ItemPrice = 5.5 },
            new Item() { BarcodeNumber = barcodeNumber++,  Category = Category.Food ,ItemName = "חלב", ItemPrice = 5.5 },
            new Item() { BarcodeNumber = barcodeNumber++,  Category = Category.Food ,ItemName = "בורקס", ItemPrice = 40 },
            new Item() { BarcodeNumber = barcodeNumber++,  Category = Category.Cutlery ,ItemName = "כוסות חדפ", ItemPrice = 3 },
            new Item() { BarcodeNumber = barcodeNumber++,  Category = Category.Cutlery ,ItemName = "צלחות חדפ", ItemPrice = 4 },
            new Item() { BarcodeNumber = barcodeNumber++,  Category = Category.Food ,ItemName = "מנה חמה", ItemPrice = 5 },
            new Item() { BarcodeNumber = barcodeNumber++,  Category = Category.Food ,ItemName = "נמס בכוס", ItemPrice = 5 },
            new Item() { BarcodeNumber = barcodeNumber++,  Category = Category.Spice ,ItemName = "פלפל שחור", ItemPrice = 2 },
            new Item() { BarcodeNumber = barcodeNumber++,  Category = Category.Spice ,ItemName = "פפריקה", ItemPrice = 2 },
            new Item() { BarcodeNumber = barcodeNumber++,  Category = Category.Spice ,ItemName = "מלח", ItemPrice = 2 },
            new Item() { BarcodeNumber = barcodeNumber++,  Category = Category.Spice ,ItemName = "כמון", ItemPrice = 2.5 },
            new Item() { BarcodeNumber = barcodeNumber++,  Category = Category.Furniture ,ItemName = "כיסא עץ", ItemPrice = 150 },
            new Item() { BarcodeNumber = barcodeNumber++,  Category = Category.Furniture ,ItemName = "שולחן 18 מקומות", ItemPrice =  11200},
            new Item() { BarcodeNumber = barcodeNumber++,  Category = Category.Furniture ,ItemName = "ארון", ItemPrice = 5200 },
            new Item() { BarcodeNumber = barcodeNumber++,  Category = Category.Furniture ,ItemName = "מיטה", ItemPrice = 1800 },
            new Item() { BarcodeNumber = barcodeNumber++,  Category = Category.Furniture ,ItemName = "מיטה זוגית", ItemPrice = 4300 },
            new Item() { BarcodeNumber = barcodeNumber++,  Category = Category.Furniture ,ItemName = "שידה", ItemPrice = 1100 },
            new Item() { BarcodeNumber = barcodeNumber++,  Category = Category.Appliances ,ItemName = "Samsung SmartTV 56inc Full HD", ItemPrice = 7600 },
            new Item() { BarcodeNumber = barcodeNumber++,  Category = Category.Appliances ,ItemName = "iphone Xs Max", ItemPrice = 5600 },
            new Item() { BarcodeNumber = barcodeNumber++,  Category = Category.Appliances ,ItemName = "Samsung Galaxy G5", ItemPrice = 1800 },
            new Item() { BarcodeNumber = barcodeNumber++,  Category = Category.Appliances ,ItemName = "Dell 22inc laptop 250SSD 8GB RAM", ItemPrice = 2200 },
            new Item() { BarcodeNumber = barcodeNumber++,  Category = Category.Appliances ,ItemName = "microUSB 32GB", ItemPrice = 16 },
            new Item() { BarcodeNumber = barcodeNumber++,  Category = Category.Appliances ,ItemName = "microUSB 512GB", ItemPrice = 64 },
            new Item() { BarcodeNumber = barcodeNumber++,  Category = Category.Appliances ,ItemName = "Webcam 32pixels", ItemPrice = 200 },
            new Item() { BarcodeNumber = barcodeNumber++,  Category = Category.Appliances ,ItemName = "Studio microphone", ItemPrice = 180 } };

            List<QRCode> qRCodes = new List<QRCode>() {
                new QRCode() { Store = "רמי לוי" },
                new QRCode() { Store = "שופרסל" },
                new QRCode() { Store = "ויקטורי" },
                new QRCode() { Store = "יוחננוף" },
                new QRCode() { Store = "ויקטורי" },
                new QRCode() { Store = "יש חסד" }};

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
