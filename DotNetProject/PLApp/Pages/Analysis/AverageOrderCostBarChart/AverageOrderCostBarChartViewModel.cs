using LiveCharts;
using LiveCharts.Wpf;
using PLApp.Commands;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace PLApp.Pages.Analysis.AverageOrderCostBarChart
{
    /// <summary>
    /// The View model of the average order total cost bar chart
    /// </summary>
    public class AverageOrderCostBarChartViewModel : INotifyPropertyChanged
    {
        #region Implement INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        #endregion

        #region LiveCharts class property
        string[] xlabel; // property that hold the X axis labels. Implements INotifyPropertyChanged
        public string[] Xlabel
        {
            get => xlabel;
            set
            {
                xlabel = value;
                OnPropertyChanged("xlabel");
            }
        }

        string xTitle; // property that hold the X axis labels. Implements INotifyPropertyChanged
        public string XTitle
        {
            get => xTitle;
            set
            {
                xTitle = value;
                OnPropertyChanged("xTitle");
            }
        }

        public SeriesCollection StoresAmountCollection { get; set; } // hold the Stores Graph collection
        public Func<double, string> Formatter { get; set; } // the grapf Formatter
        #endregion

        public AverageOrderCostBarChartModel AverageOrderCostModel;

        /// <summary>
        ///  Encapsulates a function that get an Order and return orderDate's day-in-month/month/year, according to user choise.
        /// </summary>
        private Func<BE.Order, string> xAxisVal;

        public ICommand StoreCheckBoxCommand { get; set; }

        public AverageOrderCostBarChartViewModel()
        {
            StoresAmountCollection = new SeriesCollection();
            Formatter = value => value.ToString("N");

            AverageOrderCostModel = new AverageOrderCostBarChartModel();

            StoreCheckBoxCommand = new RelayCommand(new Action<object>(StoreCheckBoxClick));
        }

        /// <summary>
        /// Build the Shops Average Order Total Cost graph.
        /// thr Graph is depending on time that the user select (year/month/day)
        /// Show in the "Shops Statistics" tab
        /// </summary>
        /// <param name="yearStackPanelVisibility">selct year's StackPanel visibility</param>
        /// <param name="monthStackPanelVisibility">selct month's StackPanel visibility</param>
        /// <param name="StackPanelCheckBoxesStoresName">StackPanel where the stores visibility checkboxes will set</param>
        public void BuildShopCart(Visibility yearStackPanelVisibility,
                                  Visibility monthStackPanelVisibility,
                                  StackPanel StackPanelCheckBoxesStoresName)
        {
            // {"storename":[$-1,$-2,...,$-31]}, where $i = average cost of order in day i of this month

            List<BE.Order> orders = SetLabalNTitle(yearStackPanelVisibility, monthStackPanelVisibility, AverageOrderCostModel.Orders);

            List<string> StoresNames = orders.Select(order=>order.StoreName).Distinct().ToList(); // get stores name

            CreateStoresCheckboxes(StackPanelCheckBoxesStoresName, StoresNames);

            StoresAmountCollection.Clear();
            foreach (var storeName in StoresNames)
            {
                // get all orders in `storeName` store:
                List<BE.Order> ordersInCurrStore = orders.Where(order => order.StoreName == storeName).ToList();

                // create a list of zerose. for each day of the week.
                // it is list of list because we maybe have more than 1 order in same day
                List<double>[] XaxisVals = Enumerable.Repeat(0, Xlabel.Length).Select(_ => new List<double> { 0 }).ToArray();
                // for each order in that store, culculate the sums of items price that the user buy at a spesific order date
                foreach (var order in ordersInCurrStore)
                {
                    double? cost = order.Items.Sum(item => item.Quantity * item.ItemPrice); // get the total cost of this order
                    if (cost != null && cost > 0)
                    {
                        int i = Array.IndexOf(Xlabel, xAxisVal(order));
                        if (XaxisVals[i][0] == 0)
                            XaxisVals[i][0] = (double)cost;
                        else
                            XaxisVals[i].Add((double)cost);
                    }
                }
                // calc the average cost in the store in specific day:
                var avgPrices = XaxisVals.Select(costsList => costsList.Average()).ToList();
                StoresAmountCollection.Add(new ColumnSeries { Title = storeName, Values = new ChartValues<double>(avgPrices) });
            }
        }

        /// <summary>
        /// create checkboxes to choose with funcuallity which stores to show
        /// </summary>
        /// <param name="StackPanelCheckBoxesStoresName">StackPanel where the stores visibility checkboxes will set</param>
        /// <param name="StoresNames">list of exisitng stores name</param>
        private void CreateStoresCheckboxes(StackPanel StackPanelCheckBoxesStoresName, List<string> StoresNames)
        {
            StackPanelCheckBoxesStoresName.Children.Clear(); // remove previous stores
            foreach (var storeName in StoresNames)
            {
                CheckBox storeCheckBox = new CheckBox { IsChecked = true, Content = storeName };
                storeCheckBox.Command = StoreCheckBoxCommand;
                storeCheckBox.CommandParameter = storeCheckBox;
                StackPanelCheckBoxesStoresName.Children.Add(storeCheckBox);
            }
        }

        /// <summary>
        /// Set the Tile and the X axis label of the graph
        /// </summary>
        /// <param name="yearStackPanelVisibility">selct year's StackPanel visibility</param>
        /// <param name="monthStackPanelVisibility">selct month's StackPanel visibility</param>
        /// <param name="orders"> list of all orders</param>
        /// <returns></returns>
        private List<BE.Order> SetLabalNTitle(Visibility yearStackPanelVisibility, Visibility monthStackPanelVisibility, List<BE.Order> orders)
        {
            // check if user want to see the average order cost per year
            if (yearStackPanelVisibility == Visibility.Collapsed && monthStackPanelVisibility == Visibility.Collapsed)
            {
                xAxisVal = order => order.OrderDate.Year.ToString();
                Xlabel = App.db.GetOrdersYear().Select(i => i.ToString()).ToArray();
                XTitle = "Year";
            }
            // check if user want to see the average order cost per months in given year
            else if (yearStackPanelVisibility == Visibility.Visible && monthStackPanelVisibility == Visibility.Collapsed)
            {
                xAxisVal = order => AverageOrderCostModel.months[order.OrderDate.Month - 1];
                orders = orders.Where(order => order.OrderDate.Year == AverageOrderCostModel.selectYear).ToList(); // get order in given year
                Xlabel = orders.Select(order => order.OrderDate.Month).Select(i => AverageOrderCostModel.months[i - 1]).Distinct().ToArray(); // get months of those orders
                XTitle = "Month";
            }
            else // check if user want to see the average order cost per day in given months and year
            {
                xAxisVal = order => (order.OrderDate.Day).ToString();
                orders = orders.Where(order => order.OrderDate.Month == AverageOrderCostModel.selectMonth && order.OrderDate.Year == AverageOrderCostModel.selectYear).ToList();
                Xlabel = Enumerable.Range(1, 31).Select(i => i.ToString()).ToArray(); // the X axis labales will update in the end...
                XTitle = "Day in Month";
            }

            return orders;
        }

        /// <summary>
        /// event handler for which stores to show in the graph
        /// </summary>
        private void StoreCheckBoxClick(object sender)
        {
            CheckBox checkBox = sender as CheckBox;
            foreach (ColumnSeries item in StoresAmountCollection)
                if (item.Title == checkBox.Content as string)
                    item.Visibility = checkBox.IsChecked == true ? Visibility.Visible : Visibility.Hidden;
        }
    }
}
