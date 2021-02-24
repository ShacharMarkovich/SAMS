using BE;
using System.Collections.Generic;
using System.ComponentModel;

namespace PLApp.Pages.Models
{
    public class OrderModel : INotifyPropertyChanged
    {
        public List<Order> Orders
        {
            get => _Orders;
            set { _Orders = value;
                OnPropertyChanged("Orders");
            }
        }
        public List<Order> _Orders {
            get;set;
        }

        public OrderModel()
        {
            Orders = (List<Order>)App.db.GetOrders();
        }
        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
