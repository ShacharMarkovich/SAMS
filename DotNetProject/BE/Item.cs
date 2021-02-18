using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace BE
{
    public class Item
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
        public Item()
        {
            Orders = new HashSet<Order>();
        }

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

        public override bool Equals(object obj)
        {
            return obj is Item item &&
                   ItemId == item.ItemId &&
                   BarcodeNumber == item.BarcodeNumber;
        }

        public override int GetHashCode()
        {
            int hashCode = 1130531465;
            hashCode = hashCode * -1521134295 + ItemId.GetHashCode();
            hashCode = hashCode * -1521134295 + BarcodeNumber.GetHashCode();
            return hashCode;
        }
    }
}
