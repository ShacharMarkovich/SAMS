using System.Windows.Controls;

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
