using Accord.MachineLearning.Rules;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using BE;

namespace PLApp.Pages
{
    /// <summary>
    /// Interaction logic for ShoppingRecommends.xaml
    /// </summary>
    public partial class ShoppingRecommends : UserControl
    {
        public string Title { get; set; }

        public ShoppingRecommends()
        {
            Title = "Shopping Recommends";
            InitializeComponent();
        }

        public void foo()
        {

        }


        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {

        }
    }
}
