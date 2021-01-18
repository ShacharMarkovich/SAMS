using BE;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PLApp.Pages.Models
{
    class OrderModel : INotifyPropertyChanged
    {
        public List<Order> Orders { get; set; }
        public OrderModel()
        {
            Orders = new List<Order>();
            // goto bl & dal to get data .... 
            List<Item> Items = new List<Item>() { new Item(1,"מוצר 1","","","",50, @"Images\1.jpg"), new Item(1, "מוצר 2", "", "", "", 50, ""), new Item(1, "מוצר 3", "", "", "", 50, "") };
            Orders.Add(new Order { OrderId = 1, OrderDate = DateTime.Now,ItemList= Items });
            Orders.Add(new Order { OrderId = 2, OrderDate = DateTime.Now});
            Orders.Add(new Order { OrderId = 3, OrderDate = DateTime.Now, ItemList = Items});
            Orders.Add(new Order { OrderId = 4, OrderDate = DateTime.Now });
        }
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
