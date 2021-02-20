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
        public void BuildShopCart(Visibility yearStackPanelVisibility, Visibility monthStackPanelVisibility, StackPanel StackPanelCheckBoxesStoresName)
        {
            // {"storename":[$-1,$-2,...,$-31]}, where $i = average cost of order in day i of this month

            List<BE.Order> orders = App.db.GetOrders().ToList();

            // check if user want to see the average order cost per year
            if (yearStackPanelVisibility == Visibility.Collapsed && monthStackPanelVisibility== Visibility.Collapsed)
            {
                Xlabel = App.db.GetOrdersYear().Select(i => i.ToString()).ToArray();
                XTitle = "Year";
            }
            // check if user want to see the average order cost per months in given year
            else if (yearStackPanelVisibility == Visibility.Visible && monthStackPanelVisibility == Visibility.Collapsed)
            {
                orders = orders.Where(order => order.OrderDate.Year == AverageOrderCostModel.selectYear).ToList(); // get order in given year
                Xlabel = orders.Select(order => order.OrderDate.Month).Select(i => AverageOrderCostModel.months[i - 1]).Distinct().ToArray(); // get months of those orders
                XTitle = "Month";
            }
            else // check if user want to see the average order cost per day in given months and year
            {
                orders = orders.Where(order => order.OrderDate.Month == AverageOrderCostModel.selectMonth && order.OrderDate.Year == AverageOrderCostModel.selectYear).ToList();
                Xlabel = null; // the X axis labales will update in the end...
                XTitle = "Day in Month";
            }

            List<string> StoresNames = orders.Select(order => order.StoreName).Distinct().ToList(); // get all stores name

            // create checkboxes to choose which stores to show:
            StackPanelCheckBoxesStoresName.Children.Clear(); // remove previous stores
            foreach (var storeName in StoresNames)
            {
                CheckBox storeCheckBox = new CheckBox { IsChecked = true, Content = storeName };
                storeCheckBox.Command = StoreCheckBoxCommand;
                storeCheckBox.CommandParameter = storeCheckBox;
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

            if (Xlabel == null) // if the user chose to see the average order cost per day in given months and year:
            {
                // create list of days in them the user buy:
                List<string> lst = new List<string>();
                for (int i = 0; i < boolDays.Count; i++)
                    if (boolDays[i])
                        lst.Add((i + 1).ToString());
                Xlabel = lst.ToArray();
            }
            //MessageBox.Show(string.Join(", ", Xlabel));
            //MessageBox.Show(string.Join(", ", StoresAmountCollection.Select(s=>s.Title)));
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
