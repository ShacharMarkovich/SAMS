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
            Items = new HashSet<Item>();
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

        public override bool Equals(object obj)
        {
            return obj is Order order &&
                   OrderId == order.OrderId;
        }

        public override int GetHashCode()
        {
            return 755918762 + OrderId.GetHashCode();
        }

        public override string ToString() => $"{OrderId} {StoreName} {OrderDate}\n{Items}";
    }
}
