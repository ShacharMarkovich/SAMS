using System;
namespace BE
{
    public class Order
    {
        public string OrderId { get; set; }
        public DateTime _orderDate { get; set; }
        public Product[] _productsList { get; set; }

        public int GetOrderPrice()
        {
            return -1;
        }

    }
}
