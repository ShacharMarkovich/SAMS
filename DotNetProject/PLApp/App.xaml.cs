using System;
using System.Windows;

namespace PLApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static BL.DataHandle db;
        static SplashScreen sp;
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            sp = new SplashScreen(@"Resources\SplashScreen1.png");
            sp.Show(true);
            db = BL.FactoryBL.Instance;

            //load QR from Drive to Items DB, Item without quantity, image.
            //db.loadQRfromDrive();
            //if QR exsits, auto add order, by qr struct, then
            //db.autoAddOrder();

            var bl = BL.DataHandle.loadQRBitmaps();
            if (bl.Count!=0)
            {
                var Orders = BL.DataHandle.parseBitmapList(bl);
                foreach (var o in Orders)
                    db.AddOrder(o);
            }
        }
}
}
