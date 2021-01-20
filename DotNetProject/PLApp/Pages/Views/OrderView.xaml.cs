using BE;
using PLApp.Pages.ViewModels;
using System;
using System.Collections.Generic;
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
       public OrderView()
        {
            InitializeComponent();
            AddOrderGrid.Visibility = Visibility.Hidden;
            CurrentVM = new OrderViewModel();
            this.DataContext = CurrentVM;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void UserControl_Loaded_1(object sender, RoutedEventArgs e)
        {

            // Do not load your data at design time.
            // if (!System.ComponentModel.DesignerProperties.GetIsInDesignMode(this))
            // {
            // 	//Load your data here and assign the result to the CollectionViewSource.
            // 	System.Windows.Data.CollectionViewSource myCollectionViewSource = (System.Windows.Data.CollectionViewSource)this.Resources["Resource Key for CollectionViewSource"];
            // 	myCollectionViewSource.Source = your data
            // }
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            itemListListView.ItemsSource = (OrdersComboBox.SelectedValue as Order).ItemList;
        }

        private void itemListListView_RowEditEnding(object sender, DataGridRowEditEndingEventArgs e)
        {
            e.Row.Item.ToString();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void AddItemBtnClick(object sender, RoutedEventArgs e)
        {
            if (storeNameTextBox.Text != "" && orderDateDatePicker.SelectedDate != null)
                App.db.InsertOrder(new Order(storeNameTextBox.Text, (DateTime)orderDateDatePicker.SelectedDate));
            // else - TODO: show fit msg
        }

        private void Canvas_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {

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
                App.db.InsertOrder(new Order(storeNameTextBox.Text, (DateTime)orderDateDatePicker.SelectedDate));
            // else - TODO: show fit msg
        }
    }
}
