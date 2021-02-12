using BE;
using PLApp.Pages.Models;
using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace PLApp.Pages.ViewModels
{
    public class OrderViewModel 
    {
        private OrderModel OModel;
        public ObservableCollection<Order> Orders { get; set; }

        public OrderViewModel()
        {
            OModel = new OrderModel();
            Orders = new ObservableCollection<Order>(OModel.Orders);
            Orders.CollectionChanged += Orders_CollectionChanged;
        }
        private void Orders_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            var newData = e.NewItems[0] as Order;
            if (e.Action == NotifyCollectionChangedAction.Add)
                OModel.Orders.Add(newData);
        }

        public Order AddOrder(Order order)
        {
            Orders.Add(order);
            return App.db.AddOrder(order);
        }
        public void RemoveOrder(Order order)
        {
            Orders.Remove(order);
            App.db.RemoveOrder(order);

        }
        public void UpdateOrder(Order order)
        {
            int i = Orders.IndexOf(order);
            if (i >= 0)
            {
                App.db.UpdateOrder(order);
                Orders[i] = order;
            }
        }
        public void AddItemToOrder(Order order, Item item)
        {
            int i = Orders.IndexOf(order);
            if (i >= 0)
            {
                order.Items.Add(item);
                App.db.UpdateOrder(order);
                Orders[i] = order;
            }
        }
        internal void RemoveItemFromOrder(Order order, Item item)
        {
            int i = Orders.IndexOf(order);
            if (i >= 0)
            {
                order.Items.Remove(item);
                App.db.UpdateOrder(order);
                Orders[i] = order;
            }
        }
    }
}
