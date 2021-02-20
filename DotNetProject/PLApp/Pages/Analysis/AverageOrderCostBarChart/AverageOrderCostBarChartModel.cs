using LiveCharts;
using System;
using System.ComponentModel;


namespace PLApp.Pages.Analysis.AverageOrderCostBarChart
{
    /// <summary>
    /// The model of the average order total cost bar chart
    /// </summary>
    public class AverageOrderCostBarChartModel
    {
        #region time class property
        public readonly string[] months = { "January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December" };
        public int selectMonth; // property of showing orders which is in the user selected month
        public int selectYear; // property of showing orders which is in the user selected year
        #endregion

        public AverageOrderCostBarChartModel()
        {
            selectYear = DateTime.Today.Year;
            selectMonth = DateTime.Today.Month;
        }
    }
}
