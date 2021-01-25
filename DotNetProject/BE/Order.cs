using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace BE
{
    public class Order
    {
        public Order() => Items = new HashSet<Item>();
        public Order(string storeName, DateTime orderDate)
        {
            Items = new List<Item>();
            StoreName = storeName;
            OrderDate = orderDate;
        }

        [System.ComponentModel.DataAnnotations.Key] // make it the table key
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // make the DB generate it value by itself
        public int OrderId { get; set; }

        public string StoreName { get; set; }


        public DateTime OrderDate { get; set; }

        //public List<Item> Items { get; set; }
        public virtual ICollection<Item> Items { get; set; }


        public override string ToString() => $"{OrderId} {StoreName} {OrderDate}\n{Items}";
    }
}
