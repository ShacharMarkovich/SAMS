using BE;
using Microsoft.Win32;
using PLApp.Pages.Models;
using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Drawing;
using System.IO;
using System.Windows;

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
                Orders.Remove(order);
                Orders.Add(order);
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

        internal void LoadImageByBarcode(int barcodeNumber)
        {
            try
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                if (openFileDialog.ShowDialog() == true)
                {
                    string newImagePath = Path.Combine(Environment.CurrentDirectory, "Images", barcodeNumber.ToString() + ".jpg");
                    File.Copy(openFileDialog.FileName, newImagePath,true);
                }
            }
            catch(Exception e)
            {
            }
        }

        internal void UpdateItem(Item item)
        {
            App.db.UpdateItem(item);
        }
    }
}
