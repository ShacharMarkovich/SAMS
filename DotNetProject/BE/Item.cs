using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
namespace BE
{
    public class Item
    {
        public Item(Item other)
        {
            BarcodeNumber = other.BarcodeNumber;
            ItemName = other.ItemName;
            ItemPrice = other.ItemPrice;
            ItemPic = other.ItemPic;
            Quantity = null;
        }
        public Item() { }

        [Key] // make it the table key
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // make the DB generate it value by itself
        public int ItemId { get; set; }

        public int BarcodeNumber { get; set; }

        public string ItemName { get; set; }

        public int ItemPrice { get; set; }

        public string ItemPic { get; set; }

        public int? Quantity { get; set; }

        public override string ToString() => $"{ItemId} {BarcodeNumber} {ItemName} {ItemPrice} {ItemPic} {Quantity}";
    }
}
