using System;
using System.Windows.Controls;

namespace PLApp.Pages
{
    /// <summary>
    /// Interaction logic for FillData.xaml
    /// </summary>
    public partial class FillData : UserControl
    {
        public String Title { get; set; }

        public FillData()
        {
            Title = "Fill Data";
            //App.MainWindow.Title = "Fill Data";

            InitializeComponent();
        }
    }
}
