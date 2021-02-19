using PLApp.Pages.Analysis.AverageOrderCostBarChart;
using System.Windows;
using System.Windows.Controls;


namespace PLApp.Pages.AverageOrderCostBarChart
{
    /// <summary>
    /// Interaction logic for AverageOrderCostBarChart.xaml
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

            /*
            StoresAmountCollection = new SeriesCollection();
            StoresAmountCollection.Clear();
            Formatter = value => value.ToString("N");

            monthComboBox.ItemsSource = months;
            yearComboBox.ItemsSource = App.db.GetOrdersYear();

            monthComboBox.SelectedItem = months[DateTime.Today.Month - 1];
            yearComboBox.SelectedItem = selectYear;

            monthComboBox.SelectionChanged += monthComboBox_SelectionChanged;
            yearComboBox.SelectionChanged += yearComboBox_SelectionChanged;

            DataContext = this;*/
        }


        /*
        /// <summary>
        /// Build the Shops Average Order Total Cost graph.
        /// thr Graph is depending on time that the user select (year/month/day)
        /// Show in the "Shops Statistics" tab
        /// </summary>
        public void BuildShopCart()
        {
            // {"storename":[$-1,$-2,...,$-31]}, where $i = average cost of order in day i of this month

            List<BE.Order> orders = App.db.GetOrders().ToList();

            // check if user want to see the average order cost per year
            if (yearStackPanel.Visibility == Visibility.Collapsed && monthStackPanel.Visibility == Visibility.Collapsed)
            {
                Xaxis = App.db.GetOrdersYear().Select(i => i.ToString()).ToArray();
                myAxis.Title = "Year";
            }
            // check if user want to see the average order cost per months in given year
            else if (yearStackPanel.Visibility == Visibility.Visible && monthStackPanel.Visibility == Visibility.Collapsed)
            {
                orders = orders.Where(order => order.OrderDate.Year == selectYear).ToList(); // get order in given year
                Xaxis = orders.Select(order => order.OrderDate.Month).Select(i => months[i - 1]).Distinct().ToArray(); // get months of those orders
                myAxis.Title = "Month";
            }
            else // check if user want to see the average order cost per day in given months and year
            {
                orders = orders.Where(order => order.OrderDate.Month == selectMonth && order.OrderDate.Year == selectYear).ToList();
                Xaxis = null; // the X axis labales will update in the end...
                myAxis.Title = "Day in Month";
            }

            List<string> StoresNames = orders.Select(order => order.StoreName).Distinct().ToList(); // get all stores name

            // create checkboxes to choose which stores to show:
            StackPanelCheckBoxesStoresName.Children.Clear(); // remove previous stores
            foreach (var storeName in StoresNames)
            {
                CheckBox storeCheckBox = new CheckBox { IsChecked = true, Content = storeName };
                storeCheckBox.Click += storeCheckBoxClick;
                StackPanelCheckBoxesStoresName.Children.Add(storeCheckBox);
            }
            // in order to show in graph only days which the user by on them, we create this checked list of days in week to see in which day the user made an order.
            List<bool> boolDays = Enumerable.Repeat(false, 31).ToList();
            StoresAmountCollection.Clear();
            foreach (var storeName in StoresNames)
            {
                // get all orders in `storeName` store:
                List<BE.Order> ordersInCurrStore = orders.Where(order => order.StoreName == storeName).ToList();

                // create a list of zerose. for each day of the week.
                // it is list of list because we maybe have more than 1 order in same day
                List<List<double>> days = Enumerable.Repeat(0, 31).Select(i => new List<double> { i }).ToList();

                // for each order in that store, culculate the sums of items price that the user buy at a spesific day
                foreach (var order in ordersInCurrStore)
                {
                    double? cost = order.Items.Sum(item => item.Quantity * item.ItemPrice); // get the total cost of this order
                    if (cost != null && cost > 0)
                    {
                        boolDays[order.OrderDate.Day - 1] = true;
                        days[order.OrderDate.Day - 1].Add((double)cost);
                    }
                }
                // calc the average cost in the store in specific day:
                List<double> avgPriceindays = days.Select(costList => costList.Average()).Where(totalPrice => totalPrice > 0).ToList();
                StoresAmountCollection.Add(new ColumnSeries { Title = storeName, Values = new ChartValues<double>(avgPriceindays) });
            }

            if (Xaxis == null) // if the user chose to see the average order cost per day in given months and year:
            {
                // create list of days in them the user buy:
                List<string> lst = new List<string>();
                for (int i = 0; i < boolDays.Count; i++)
                    if (boolDays[i])
                        lst.Add((i + 1).ToString());
                Xaxis = lst.ToArray();
            }
        }

         /// <summary>
        /// event handler for which stores to show in the graph
        /// </summary>
        private void storeCheckBoxClick(object sender, RoutedEventArgs e)
        {
            CheckBox checkBox = sender as CheckBox;
            foreach (ColumnSeries item in StoresAmountCollection)
                if (item.Title == checkBox.Content as string)
                    item.Visibility = checkBox.IsChecked == true ? Visibility.Visible : Visibility.Hidden;
        }
        */

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
