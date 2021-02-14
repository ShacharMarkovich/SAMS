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
        
        public double ItemPrice { get; set; }

        [NotMapped]
        public int? Quantity { get; set; }
        public virtual ICollection<Order> Orders { get; set; }

        //public override string ToString() => $"{ItemId} {BarcodeNumber} {ItemName} {ItemPrice} {Quantity}";
    }
}
