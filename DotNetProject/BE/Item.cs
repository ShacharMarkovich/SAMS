using SQLite;
using System;
using System.Drawing;

namespace BE
{
    public class Item
    {
        public Item(string itemName, string manufacturerName, string unitQty, string manufactureCountry, int quantity, int itemPrice)
        {
            ItemName = itemName;
            ManufacturerName = manufacturerName;
            UnitQty = unitQty;
            ManufactureCountry = manufactureCountry;
            Quantity = quantity;
            ItemPrice = itemPrice;
        }
        public Item()
        {

        }
        [PrimaryKey, AutoIncrement]
        public int ItemCode { get; set; }
        public string ItemName { get; set; }
        public string ManufacturerName { get; set; }
        public string UnitQty { get; set; }
        public string ManufactureCountry { get; set; }
        public int Quantity { get; set; }
        public int ItemPrice { get; set; }
    //    public Image ItemPic { get; set; }
    }
}
