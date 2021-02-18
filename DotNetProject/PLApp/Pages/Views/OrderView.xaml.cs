using BE;
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
            storeNameTextBox.Text = "Store Name";
            AddOrderGrid.Visibility = Visibility.Hidden;
            CurrentVM = new OrderViewModel();
            this.DataContext = CurrentVM;
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (OrdersComboBox.SelectedIndex > -1)
            {
                _currOrder = CurrentVM.Orders[OrdersComboBox.SelectedIndex];
                itemListListView.ItemsSource = new ObservableCollection<Item>(CurrentVM.Orders[OrdersComboBox.SelectedIndex].Items);
                AddOrderbtn.Visibility = Visibility.Hidden;
                AddOrderGrid.Visibility = Visibility.Visible;
                AddItemToOrderBtn.Visibility = Visibility.Visible;

            }
            else
            {
                _currOrder = null;
                itemListListView.ItemsSource = null;
                AddOrderGrid.Visibility = Visibility.Hidden;
                AddOrderbtn.Visibility = Visibility.Visible;
                AddItemToOrderBtn.Visibility = Visibility.Hidden;

            }
        }

        private void OrdersComboBox_OnDropDownOpened(object sender, EventArgs e)
        {
            OrdersComboBox.SelectedIndex = -1;
        }
        private void itemListListView_RowEditEnding(object sender, DataGridRowEditEndingEventArgs e)
        {
            var v = (itemListListView.SelectedItem as Item);
            Order order = OrdersComboBox.SelectedItem as Order;
            MessageBox.Show("do it!");
            App.db.AddItemToOrder(v, order);
            App.db.UpdateOrder(order);
            e.Row.Item.ToString();
        }

        private void AddItemBtnClick(object sender, RoutedEventArgs e)
        {
            if (OrdersComboBox.SelectedIndex > -1)
            {
                AddItemView addItem = new AddItemView();
                addItem.ShowDialog();
                if (addItem.VM.isValid)
                {
                    int prevIndex = OrdersComboBox.SelectedIndex;
                    CurrentVM.AddItemToOrder((Order)OrdersComboBox.SelectedItem, addItem.VM.itemViewSource);
                    OrdersComboBox.SelectedIndex = prevIndex;
                }
            }
            else MessageBox.Show("Please Select an Order first!", "Please notice", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void NewOrderBtnClick(object sender, RoutedEventArgs e)
        {
            switch (AddOrderGrid.Visibility)
            {
                case Visibility.Hidden:
                    AddOrderGrid.Visibility = Visibility.Visible;
                    break;
                case Visibility.Visible:
                default:
                    AddOrderGrid.Visibility = Visibility.Hidden;
                    break;
            }
        }

        private void AddOrderBtn_Click(object sender, RoutedEventArgs e)
        {
            if (storeNameTextBox.Text != "" && orderDateDatePicker.SelectedDate != null)
            {
                _currOrder = CurrentVM.AddOrder(new Order(storeNameTextBox.Text, (DateTime)orderDateDatePicker.SelectedDate));
                OrdersComboBox.SelectedItem = _currOrder;
                itemListListView.ItemsSource = new ObservableCollection<Item>(CurrentVM.Orders[OrdersComboBox.SelectedIndex].Items);
                AddOrderGrid.Visibility = Visibility.Hidden;
            }
            else MessageBox.Show("Please fill the whole data!", "Oops.. Something went wrong", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void Apply_Click(object sender, RoutedEventArgs e)
        {
            if (OrdersComboBox.SelectedIndex != -1)
            {
                if (storeNameTextBox.Text != "" && orderDateDatePicker.SelectedDate != null)
                {
                    _currOrder.OrderDate = (DateTime)orderDateDatePicker.SelectedDate;
                    _currOrder.StoreName = storeNameTextBox.Text;
                    CurrentVM.UpdateOrder(_currOrder);
                    OrdersComboBox.SelectedItem = _currOrder;
                    AddOrderGrid.Visibility = Visibility.Hidden;
                }
                else MessageBox.Show("Please fill the whole data!", "Oops.. Something went wrong", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else MessageBox.Show("Please Select an existing order first!", "Oops.. Something went wrong", MessageBoxButton.OK, MessageBoxImage.Error);

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
                int index = OrdersComboBox.SelectedIndex;
                Item item = itemListListView.SelectedItem as Item;
                CurrentVM.RemoveItemFromOrder(_currOrder, item);
                OrdersComboBox.SelectedIndex = index;
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
    }
}