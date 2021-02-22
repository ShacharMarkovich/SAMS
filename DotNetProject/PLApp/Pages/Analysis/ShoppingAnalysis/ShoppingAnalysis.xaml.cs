using LiveCharts.Events;
using System;
using System.Windows.Controls;

namespace PLApp.Pages
{
    /// <summary>
    /// Interaction logic for ShoppingAnalysis.xaml
    /// </summary>
    public partial class ShoppingAnalysis : UserControl
    {
        public string Title { get; set; }

        public ShoppingAnalysis()
        {
            Title = "Shopping Analysis";
            InitializeComponent();
        }
    }
}
