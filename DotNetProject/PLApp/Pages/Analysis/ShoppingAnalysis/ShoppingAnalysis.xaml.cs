using LiveCharts;
using LiveCharts.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
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
