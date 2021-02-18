using LiveCharts;
using LiveCharts.Wpf;
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

        public SeriesCollection StoresAmountCollection { get; set; }
        public string[] Xaxis { get; set; }
        public Func<double, string> Formatter { get; set; }

        public string Title { get; set; }
        public ShoppingAnalysis()
        {
            Title = "Shopping Analysis";
            InitializeComponent();

            StoresAmountCollection = new SeriesCollection();
            //{ new ColumnSeries { Title = "storename1", Values = new ChartValues<double> { 10, 50, 39, 50 } } };

            //adding series will update and animate the chart automatically
            //StoresAmountCollection.Add(new ColumnSeries { Title = "storename2", Values = new ChartValues<double> { 11, 56, 42 } });

            //also adding values updates and animates the chart automatically
            //StoresAmountCollection[1].Values.Add(48d);
            //Xaxis = new[] { "Maria", "Susan", "Charles", "Frida" };
            Formatter = value => value.ToString("N");

            DataContext = this;
            BuildShopCart();
        }
        /// <summary>
        /// in the Shops Statistics we show the Average Order Cost in each store, depending on time of year/month/week/day
        /// </summary>
        public void BuildShopCart()
        {
            int currYear = DateTime.Today.Year;
            int currMonth = DateTime.Today.Month;

            // only order in this month and year:
            List<BE.Order> orders = App.db.GetOrders().ToList();//.Where(order => order.OrderDate.Month == currMonth && order.OrderDate.Year == currYear).ToList();

            // create a Dictionary that contains: {"storename":[$-1,$-2,...,$-31]}, where $i = average cost of order in day i of this month

            // get all stores name:
            List<string> TitleStoresNames = orders.Select(order => order.StoreName).Distinct().ToList();

            foreach (var storeName in TitleStoresNames)
            {
                // get all orders in `storeName` store:
                List<BE.Order> ordersInCurrStore = orders.Where(order => order.StoreName == storeName).ToList();

                // create a list of zerose. for each day of the week.
                List<List<double>> days = Enumerable.Repeat(0, 31).Select(i => new List<double>{ i }).ToList();

                // for each order in that store, culculate the sums of items price that the user buy at a spesific day
                foreach (var order in ordersInCurrStore)
                {
                    double? cost = order.Items.Sum(item => item.Quantity * item.ItemPrice); // get the total cost of this order
                    if (cost != null)
                        days[order.OrderDate.Day].Add((double)cost);
                }
                List<double> avgPriceindays = days.Select(costList => costList.Average()).ToList();
                StoresAmountCollection.Add(new ColumnSeries { Title = storeName, Values = new ChartValues<double> (avgPriceindays) });
            }
            Xaxis = days.Select(day => day.ToString()).ToArray();
        }
        private void AggregationCutChangeChecked(object sender, RoutedEventArgs e)
        {
            RadioButton s = sender as RadioButton;
            //MessageBox.Show(s.Name);
        }
    }
}
