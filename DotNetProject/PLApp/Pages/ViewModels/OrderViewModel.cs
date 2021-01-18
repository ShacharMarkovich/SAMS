using BE;
using PLApp.Pages.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace PLApp.Pages.ViewModels
{
    public class OrderViewModel
    {
        private OrderModel OModel;
        public ObservableCollection<Order> Orders
        {
            get;
            set;
        }

        public OrderViewModel()
        {
           OModel = new OrderModel();
           Orders = new ObservableCollection<Order>(OModel.Orders);
            Orders.CollectionChanged += Orders_CollectionChanged;
        }
        private void Orders_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            var newData = e.NewItems[0] as Order;

            if (e.Action == NotifyCollectionChangedAction.Add)
                OModel.Orders.Add(newData);

        }
    }
}
