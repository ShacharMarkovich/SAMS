using BE;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PLApp.Pages.Analysis.AverageOrderCostBarChart
{
    /// <summary>
    /// The model of the average order total cost bar chart
    /// </summary>
    public class AverageOrderCostBarChartModel
    {
        #region time class property
        public int selectMonth; // property of showing orders which is in the user selected month
        public int selectYear; // property of showing orders which is in the user selected year

        public readonly string[] months = { "January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December" };
        #endregion

        public string selectedStoreName;
        public Item selectedItem;

        public List<Order> Orders { get; set; }
        public ICollection<Item> Items { get; private set; }

        public AverageOrderCostBarChartModel()
        {
            Items = App.db.GetAllItems();
            Orders = App.db.GetOrders().ToList();

            selectYear = DateTime.Today.Year;
            selectMonth = DateTime.Today.Month;
        }
    }
}
