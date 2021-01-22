using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
namespace BE
{
    public class Item
    {
        public Item() { }

        public Item(int id, int barcodeNumber, int quantity, string itemName, int itemPrice, string itemPic)
        {
            Id = id;
            BarcodeNumber = barcodeNumber;
            Quantity = quantity;
            ItemName = itemName;
            ItemPrice = itemPrice;
            ItemPic = itemPic;
        }

        [System.ComponentModel.DataAnnotations.Key] // make it the table key
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // make the DB generate it value by itself
        public int Id { get; set; }

        public int BarcodeNumber { get; set; }

        public int Quantity { get; set; }

        public string ItemName { get; set; }

        public int ItemPrice { get; set; }

        public string ItemPic { get; set; }

    }
}
