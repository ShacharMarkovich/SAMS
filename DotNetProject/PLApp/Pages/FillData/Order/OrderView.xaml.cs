﻿using BE;
using MahApps.Metro.Controls;
using MaterialDesignThemes.Wpf;
using PLApp.Pages.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace PLApp.Pages.Views
{
    /// <summary>
    /// Interaction logic for OrderView.xaml
    /// </summary>
    public partial class OrderView : UserControl
    {
        public OrderViewModel CurrentVM;
        private Order _currOrder;
        public OrderView()
        {
            InitializeComponent();
            CurrentVM = new OrderViewModel();
            this.DataContext = CurrentVM;
        }



        private void itemListListView_RowEditEnding(object sender, DataGridRowEditEndingEventArgs e)
        {
            (sender as DataGrid).RowEditEnding -= itemListListView_RowEditEnding;
            (sender as DataGrid).CommitEdit();
            Item i = e.Row.Item as Item;
            (sender as DataGrid).Items.Refresh();
            (sender as DataGrid).RowEditEnding += itemListListView_RowEditEnding;

            CurrentVM.UpdateItem(i);
        }

        private void UpdateImageByBarcode_BtnClick(object sender, RoutedEventArgs e)
        {
            CurrentVM.LoadImageByBarcode(((Item)itemListListView.SelectedValue).BarcodeNumber);
            itemListListView.Items.Refresh();
        }
        private void RemoveItemFromOrder_BtnClick(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Are you Sure That you want to delete this item?", "Pay attention",
                                                      MessageBoxButton.YesNoCancel, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                int index = OrderListView.SelectedIndex;
                Item item = itemListListView.SelectedItem as Item;
                CurrentVM.RemoveItemFromOrder(_currOrder, item);
                OrderListView.SelectedIndex = index;
            }
        }

        private void ListMode_Toggled(object sender, RoutedEventArgs e)
        {
            ToggleSwitch toggleSwitch = sender as ToggleSwitch;
            if (toggleSwitch != null)
            {
                if (toggleSwitch.IsOn == true)
                    itemListListView.ItemsSource = null;
                else
                    itemListListView.ItemsSource = App.db.GetAllItems();
            }
        }

        private void OrderListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            metroProgressBar.Visibility = Visibility.Visible;
            ICollection<Item> list = ((sender as ListView).SelectedItem as Order).Items;
            itemListListView.Visibility = Visibility.Hidden;
            Task.Factory.StartNew(() => LoadItemListFromOrder(list));
        }

        private void LoadItemListFromOrder(ICollection<Item> list)
        {
            ObservableCollection<Item> items = new ObservableCollection<Item>(list);
            Dispatcher.BeginInvoke(new Action(() =>
            {
                itemListListView.ItemsSource = items;
                itemListListView.Visibility = Visibility.Visible;
                metroProgressBar.Visibility = Visibility.Hidden;


            }), DispatcherPriority.Background);
        }
    }
}