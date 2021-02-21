using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Wpf;
using PLApp.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Input;

namespace PLApp.Pages.Analysis.CategoryPieChart
{
    internal class CategoryPieChartViewModel
    {
        public SeriesCollection SeriesCollection_productsCount { get; set; }
        public SeriesCollection SeriesCollection_totalPriceInCategory { get; set; }
        public SeriesCollection SeriesCollection_avgPriceInCategory { get; set; }

        public CategoryPieChartViewModel()
        {
            List<CategoryPieChartModel> categoryPieChartList = GetCategoriesStatistic();

            SeriesCollection_productsCount = new SeriesCollection();
            SeriesCollection_totalPriceInCategory = new SeriesCollection();
            SeriesCollection_avgPriceInCategory = new SeriesCollection();

            foreach (var itemModel in categoryPieChartList)
            {
                SeriesCollection_productsCount.Add(new PieSeries
                {
                    Title = itemModel.category.ToString("G"),
                    Values = new ChartValues<ObservableValue> { new ObservableValue(itemModel.productsCount) },
                    LabelPoint = chartPoint => string.Format("{0:0.0}", chartPoint.Y),
                    DataLabels = true
                });
                SeriesCollection_totalPriceInCategory.Add(new PieSeries
                {
                    Title = itemModel.category.ToString("G"),
                    Values = new ChartValues<ObservableValue> { new ObservableValue(itemModel.totalPriceInCategory) },
                    LabelPoint = chartPoint => string.Format("{0:0.0}", chartPoint.Y),
                    DataLabels = true
                });
                SeriesCollection_avgPriceInCategory.Add(new PieSeries
                {
                    Title = itemModel.category.ToString("G"),
                    Values = new ChartValues<ObservableValue> { new ObservableValue(itemModel.avgPriceInCategory) },
                    LabelPoint = chartPoint => string.Format("{0:0.0}", chartPoint.Y),
                    DataLabels = true
                });

            }
        }

        /// <summary>
        /// Get the following categories statistic:
        /// <para>- Average product price in category</para>
        /// <para>- Products price' sum in category  </para>
        /// <para>- Amount of products in category   </para>
        /// </summary>
        /// <returns>list of those statistic in each category</returns>
        public List<CategoryPieChartModel> GetCategoriesStatistic()
        {
            return App.db.GetAllItems().GroupBy(item => item.Category)
                .Select(g => new CategoryPieChartModel
                {
                    category = g.Key,
                    productsCount = g.Count(),
                    totalPriceInCategory = g.Sum(item => item.ItemPrice),
                    avgPriceInCategory = g.Average(item => item.ItemPrice)
                }).ToList();
        }
    }
}
