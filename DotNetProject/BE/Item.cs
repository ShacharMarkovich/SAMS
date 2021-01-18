﻿using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
namespace BE
{
    public class Item
    {

        public Item()
        {

        }

        public Item(int id, string itemName, string manufacturerName, string unitQty, string manufactureCountry, int itemPrice, string itemPicLocation)
        {
            Id = id;
            ItemName = itemName;
            ManufacturerName = manufacturerName;
            UnitQty = unitQty;
            ManufactureCountry = manufactureCountry;
            ItemPrice = itemPrice;
            ItemPicLocation = itemPicLocation;
        }

        public int Id { get; set; }
        public string ItemName { get; set; }
        public string ManufacturerName { get; set; }
        public string UnitQty { get; set; }
        public string ManufactureCountry { get; set; }
        public int Quantity { get; set; }
        public int ItemPrice { get; set; }
        public string ItemPicLocation { get; set; }
    }
}
