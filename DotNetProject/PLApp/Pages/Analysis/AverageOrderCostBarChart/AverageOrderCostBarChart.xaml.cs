using PLApp.Pages.Analysis.AverageOrderCostBarChart;
using System;
using System.Windows;
using System.Windows.Controls;


namespace PLApp.Pages.AverageOrderCostBarChart
{
    /// <summary>
    /// Interaction logic for AverageOrderCostBarChart.xaml
    /// The View of the average order total cost bar chart
    /// </summary>
    public partial class AverageOrderCostBarChart : UserControl
    {
        /*readonly string[] months = { "January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December" };

        int selectMonth; // property of showing orders which is in the user selected month

        int selectYear; // property of showing orders which is in the user selected year

        string[] xaxis; // property that hold the X axis labales. Implements INotifyPropertyChanged
        public string[] Xaxis
        {
            get => xaxis;
            set
            {
                xaxis = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("Xaxis"));
            }
        }

        public SeriesCollection StoresAmountCollection { get; set; } // hold the Stores Graph collection

        public Func<double, string> Formatter { get; set; } // the grapf Formatter


        public event PropertyChangedEventHandler PropertyChanged;*/
        public string Title { get; set; }

        public AverageOrderCostBarChartViewModel ViewModel;

        /// <summary>
        /// C'tor. Initialize Components.
        /// </summary>
        public AverageOrderCostBarChart()
        {
            Title = "Shopping Analysis";
            InitializeComponent();
            ViewModel = new AverageOrderCostBarChartViewModel();
            DataContext = ViewModel;
            StackPanelCheckBoxesStoresName.DataContext = ViewModel;


            monthComboBox.ItemsSource = ViewModel.AverageOrderCostModel.months;
            yearComboBox.ItemsSource = ViewModel.AverageOrderCostModel.db.GetOrdersYear();

            monthComboBox.SelectedItem = ViewModel.AverageOrderCostModel.months[DateTime.Today.Month - 1];
            yearComboBox.SelectedItem = DateTime.Today.Year;

            monthComboBox.SelectionChanged += monthComboBox_SelectionChanged;
            yearComboBox.SelectionChanged += yearComboBox_SelectionChanged;
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
            ViewModel.BuildShopCart(yearStackPanel.Visibility, monthStackPanel.Visibility, StackPanelCheckBoxesStoresName); // update the graph
        }

        /// <summary>
        /// event handler for user change which month to show in graph
        /// </summary>
        private void monthComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ViewModel.AverageOrderCostModel.selectMonth = monthComboBox.SelectedIndex + 1;
            ViewModel.BuildShopCart(yearStackPanel.Visibility, monthStackPanel.Visibility, StackPanelCheckBoxesStoresName); // update the graph
        }

        /// <summary>
        /// event handler for user change which year to show in graph
        /// </summary>
        private void yearComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ViewModel.AverageOrderCostModel.selectYear = (int)yearComboBox.SelectedItem;
            ViewModel.BuildShopCart(yearStackPanel.Visibility, monthStackPanel.Visibility, StackPanelCheckBoxesStoresName);
        }
    }
}
