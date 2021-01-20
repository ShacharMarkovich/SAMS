using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace PLApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static BL.DataHandle db;

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            SplashScreen sp = new SplashScreen("spla.png");
            sp.Show(false);
            db = new BL.DataHandle();
            sp.Close(new TimeSpan(100));
        }
}
}
