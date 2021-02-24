using BE;
using BL;
using System.Collections.Generic;
using System.ComponentModel;

namespace PLApp.Pages.Models
{
    public class OrderModel : INotifyPropertyChanged
    {
        public DataHandle db { get; private set; }

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
            db = new BL.DataHandle();
            Orders = (List<Order>)db.GetOrders();
        }
        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
