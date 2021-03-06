﻿using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System;

namespace BE
{
    public class Item : IComparable
    {
        public Item(Item other)
        {
            Orders = new HashSet<Order>();
            BarcodeNumber = other.BarcodeNumber;
            ItemName = other.ItemName;
            ItemPrice = other.ItemPrice;
            Category = other.Category;
            Quantity = null;
        }
        public Item() => Orders = new HashSet<Order>();

        [Key] // make it the table key
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // make the DB generate it value by itself
        public int ItemId { get; set; }

        public int BarcodeNumber { get; set; }

        public string ItemName { get; set; }
        public Category Category { get; set; }
        public double ItemPrice { get; set; }
        public int? Quantity { get; set; }
        public virtual ICollection<Order> Orders { get; set; }

        public override string ToString() => $"{ItemId} {BarcodeNumber} {ItemName} {ItemPrice} {Quantity}";

        public override bool Equals(object obj) => obj is Item item && BarcodeNumber == item.BarcodeNumber;

        public override int GetHashCode()
        {
            int hashCode = 1130531465;
            hashCode = hashCode * -1521134295 + ItemId.GetHashCode();
            hashCode = hashCode * -1521134295 + BarcodeNumber.GetHashCode();
            return BarcodeNumber;
        }

        public int CompareTo(object obj)
        {
            if (!(obj is Item) || obj == null)
                return -1;
            Item other = obj as Item;
            return BarcodeNumber.CompareTo(other.BarcodeNumber);
        }
    }
}
