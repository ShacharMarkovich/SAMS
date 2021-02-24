using System;
using System.Windows;
using System.Windows.Controls;

namespace PLApp.Pages.Analysis.ProductOrdersAmountLineChart
{
    /// <summary>
    /// Interaction logic for ProductOrdersAmountLineChartView.xaml
    /// </summary>
    public partial class ProductOrdersAmountLineChartView : UserControl
    {
        ProductOrdersAmountLineChartViewModel ViewModel;

        public ProductOrdersAmountLineChartView()
        {
            InitializeComponent();
            ViewModel = new ProductOrdersAmountLineChartViewModel();
            DataContext = ViewModel;

            monthComboBox.ItemsSource = ViewModel.Model.months;
            yearComboBox.ItemsSource = App.db.GetOrdersYear();

            monthComboBox.SelectedItem = ViewModel.Model.months[DateTime.Today.Month - 1];
            yearComboBox.SelectedItem = DateTime.Today.Year;

            monthComboBox.SelectionChanged += monthComboBox_SelectionChanged;
            yearComboBox.SelectionChanged += yearComboBox_SelectionChanged;
        }


        private void ItemsComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ViewModel.ItemCheckBoxSelectionChange((sender as ComboBox).SelectedItem as string, storesComboBox.SelectedItem as string);
            ViewModel.BuildShopCart(yearStackPanel.Visibility, monthStackPanel.Visibility);
        }

        /// <summary>
        /// when user change the selected store,
        /// we update the ItemsComboBox's ItemsSource list to the items in the selected store.
        /// </summary>
        private void StoresComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ItemsComboBox.SelectionChanged -= ItemsComboBox_SelectionChanged;
            ViewModel.Model.selectedStoreName = storesComboBox.SelectedItem as string;
            ItemsComboBox.ItemsSource = ViewModel.UpdateItemsSource(storesComboBox.SelectedItem as string);
            ItemsComboBox.SelectedIndex = -1;

            ItemsComboBox.SelectionChanged += ItemsComboBox_SelectionChanged;
        }


        /// <summary>
        /// event handler of when the user change the cut aggregation
        /// </summary>
        private void AggregationCutChangeChecked(object sender, RoutedEventArgs e)
        {
            RadioButton radioButton = sender as RadioButton;
            switch (radioButton.Content as string)
            {
                case "Year": // user chose to see per year:
                    yearStackPanel.Visibility = Visibility.Collapsed;
                    monthStackPanel.Visibility = Visibility.Collapsed;
                    break;

                case "Month": // user chose to see per month:
                    yearStackPanel.Visibility = Visibility.Visible;
                    monthStackPanel.Visibility = Visibility.Collapsed;
                    break;

                case "Day": // user chose to see per day in month:
                    yearStackPanel.Visibility = Visibility.Visible;
                    monthStackPanel.Visibility = Visibility.Visible;
                    break;

                default: // for errors
                    yearStackPanel.Visibility = Visibility.Visible;
                    monthStackPanel.Visibility = Visibility.Visible;
                    break;
            }
            ViewModel.BuildShopCart(yearStackPanel.Visibility, monthStackPanel.Visibility); // update the graph
        }

        /// <summary>
        /// event handler for user change which month to show in graph
        /// </summary>
        private void monthComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ViewModel.Model.selectMonth = monthComboBox.SelectedIndex + 1;
            ViewModel.BuildShopCart(yearStackPanel.Visibility, monthStackPanel.Visibility); // update the graph
        }

        /// <summary>
        /// event handler for user change which year to show in graph
        /// </summary>
        private void yearComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ViewModel.Model.selectYear = (int)yearComboBox.SelectedItem;
            ViewModel.BuildShopCart(yearStackPanel.Visibility, monthStackPanel.Visibility);
        }
    }
}
