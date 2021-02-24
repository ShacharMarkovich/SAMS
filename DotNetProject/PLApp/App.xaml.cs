using System.Windows;

namespace PLApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public BL.DataHandle db;
        static SplashScreen sp;
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            sp = new SplashScreen(@"Resources\SplashScreen1.png");
            sp.Show(true);
            db = new BL.DataHandle();
            db.LoadNewQRCodes();
        }
}
}
