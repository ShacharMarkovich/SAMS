using LiveCharts;
using LiveCharts.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace PLApp.Pages
{
    /// <summary>
    /// Interaction logic for ShoppingAnalysis.xaml
    /// </summary>
    public partial class ShoppingAnalysis : UserControl
    {
        readonly string[] months = { "January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December" };
        readonly string[] weeks = { "1-8", "9-16", "16-24", "24-31" };
        readonly int[] days = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31 };
        private int? selectMonth;
        private int? selectYear;

        public SeriesCollection StoresAmountCollection { get; set; }
        public string[] Xaxis { get; set; }
        public Func<double, string> Formatter { get; set; }

        public string Title { get; set; }

        public ShoppingAnalysis()
        {
            Title = "Shopping Analysis";
            InitializeComponent();

            selectYear = DateTime.Today.Year;
            selectMonth = DateTime.Today.Month;

            monthComboBox.ItemsSource = months;
            yearComboBox.ItemsSource = App.db.GetOrdersYear();

            StoresAmountCollection = new SeriesCollection();
            Formatter = value => value.ToString("N");

            DataContext = this;
            //BuildShopCart();
        }

        /// <summary>
        /// in the Shops Statistics we show the Average Order Cost in each store, depending on time of year/month/week/day
        /// </summary>
        public void BuildShopCart()
        {
            // {"storename":[$-1,$-2,...,$-31]}, where $i = average cost of order in day i of this month

            // only order in this month and year:
            List<BE.Order> orders = App.db.GetOrders().ToList();

            if (selectYear == null && selectMonth == null) // year is null - so X axis values are the years
                Xaxis = App.db.GetOrdersYear().Select(i => i.ToString()).ToArray();

            else if (selectMonth != null)
            {
                orders = orders.Where(order => order.OrderDate.Month == selectMonth).ToList();
                Xaxis = orders.Select(order => order.OrderDate.Month).Select(i => months[i]).Distinct().ToArray();
            }
            else
            {
                orders = orders.Where(order => order.OrderDate.Month == selectMonth && order.OrderDate.Year == selectYear).ToList();
                Xaxis = null;
            }
            // get all stores name:
            List<string> TitleStoresNames = orders.Select(order => order.StoreName).Distinct().ToList();
            /*foreach (var storeName in TitleStoresNames)
            {
                CheckBox checkBox = new CheckBox();
                checkBox.= storeName;
                checkBox.SelectionChanged;
            }*/

            StoresAmountCollection.Clear();
            List<bool> boolDays = Enumerable.Repeat(0, 31).Select(i => i == 0).ToList();

            foreach (var storeName in TitleStoresNames)
            {
                // get all orders in `storeName` store:
                List<BE.Order> ordersInCurrStore = orders.Where(order => order.StoreName == storeName).ToList();

                // create a list of zerose. for each day of the week.
                List<List<double>> days = Enumerable.Repeat(0, 31).Select(i => new List<double> { i }).ToList();

                // for each order in that store, culculate the sums of items price that the user buy at a spesific day
                foreach (var order in ordersInCurrStore)
                {
                    double? cost = order.Items.Sum(item => item.Quantity * item.ItemPrice); // get the total cost of this order
                    if (cost != null)
                    {
                        boolDays[order.OrderDate.Day] = true;
                        days[order.OrderDate.Day].Add((double)cost);
                    }
                }
                List<double> avgPriceindays = days.Select(costList => costList.Average()).ToList();
                StoresAmountCollection.Add(new ColumnSeries { Title = storeName, Values = new ChartValues<double>(avgPriceindays) });
            }
            if (Xaxis == null)
            {
                List<string> lst = new List<string>();
                for (int i = 0; i < boolDays.Count; i++)
                    if (boolDays[i])
                        lst.Add((i + 1).ToString());
                Xaxis = lst.ToArray();
            }
        }
        private void AggregationCutChangeChecked(object sender, RoutedEventArgs e)
        {
            RadioButton radioButton = sender as RadioButton;
            switch (radioButton.Content as string)
            {
                case "Year":
                    yearComboBox.Visibility = Visibility.Collapsed;
                    monthComboBox.Visibility = Visibility.Collapsed;
                    selectYear = null;
                    selectMonth = null;
                    break;

                case "Month":
                    yearComboBox.Visibility = Visibility.Visible;
                    monthComboBox.Visibility = Visibility.Collapsed;
                    selectMonth = null;
                    break;

                case "Day":
                    yearComboBox.Visibility = Visibility.Visible;
                    monthComboBox.Visibility = Visibility.Visible;
                    break;

                default:
                    yearComboBox.Visibility = Visibility.Visible;
                    monthComboBox.Visibility = Visibility.Visible;
                    break;
            }
            BuildShopCart();
        }
        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            //var s = sender as CheckBox;
            //if (s.IsChecked == true) ;
        }

        private void monthComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selectMonth = monthComboBox.SelectedIndex + 1;
            BuildShopCart();
        }

        private void yearComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selectYear = (int)yearComboBox.SelectedItem;
            BuildShopCart();
        }
    }
}
