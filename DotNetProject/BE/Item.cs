using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
namespace BE
{
    public class Item
    {
        public Item() { }

        public Item(int quantity, int barcodeNumber, int id, string itemName, int itemPrice, string itemPicLocation)
        {
            Quantity = quantity;
            BarcodeNumber = barcodeNumber;
            Id = id;
            ItemName = itemName;
            ItemPrice = itemPrice;
            ItemPicLocation = itemPicLocation;
        }

       
        public int Quantity { get; set; }
       
        public int BarcodeNumber { get; set; }
       
        public int Id { get; set; }
       
        public string ItemName { get; set; }
       
        public int ItemPrice { get; set; }
       
        public string ItemPicLocation { get; set; }
    }
}
