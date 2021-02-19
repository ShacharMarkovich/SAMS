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
    /// Interaction logic for ShoppingRecommends.xaml
    /// </summary>
    public partial class ShoppingRecommends : UserControl
    {
        public String Title { get; set; }
        public ShoppingRecommends()
        {
            Title = "Shopping Recommends";
            InitializeComponent();
        }
    }
}
