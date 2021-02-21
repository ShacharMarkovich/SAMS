using LiveCharts;
using LiveCharts.Defaults;
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

namespace PLApp.Pages.Analysis.CategoryPieChart
{
    /// <summary>
    /// Interaction logic for CategoryPieChartView.xaml
    /// </summary>
    public partial class CategoryPieChartView : UserControl
    {
        private CategoryPieChartViewModel ViewModel;
        public CategoryPieChartView()
        {
            InitializeComponent();
            ViewModel = new CategoryPieChartViewModel();
            DataContext = ViewModel;
        }
            

    }
}
