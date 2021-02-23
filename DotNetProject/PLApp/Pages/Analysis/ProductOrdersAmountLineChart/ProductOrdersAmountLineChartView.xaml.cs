using LiveCharts.Events;
using System;
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
        }

        private void Axis_OnRangeChanged(RangeChangedEventArgs eventargs)
        {
            double currentRange = eventargs.Range;

            if (currentRange < TimeSpan.TicksPerDay * 2)
            {
                ViewModel.Formatter = x => new DateTime((long)x).ToString("t");
                return;
            }

            if (currentRange < TimeSpan.TicksPerDay * 60)
            {
                ViewModel.Formatter = x => new DateTime((long)x).ToString("dd MMM yy");
                return;
            }

            if (currentRange < TimeSpan.TicksPerDay * 540)
            {
                ViewModel.Formatter = x => new DateTime((long)x).ToString("MMM yy");
                return;
            }

            ViewModel.Formatter = x => new DateTime((long)x).ToString("yyyy");
        }

        public void Dispose() => ViewModel.Values.Dispose();

        private void ItemsComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ViewModel.ItemCheckBoxSelectionChange((sender as ComboBox).SelectedItem as string, storesComboBox.SelectedItem as string);
        }

        private void StoresComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ItemsComboBox.SelectionChanged -= ItemsComboBox_SelectionChanged;

            ItemsComboBox.ItemsSource = ViewModel.UpdateItemsSource(storesComboBox.SelectedItem as string);
            ItemsComboBox.SelectedIndex = -1;

            ItemsComboBox.SelectionChanged += ItemsComboBox_SelectionChanged;
        }
    }
}
