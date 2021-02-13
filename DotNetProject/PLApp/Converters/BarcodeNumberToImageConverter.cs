using PLApp.Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace PLApp.Converters
{
    class BarcodeNumberToImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                string filePath = Path.Combine(Environment.CurrentDirectory, "Images", (string)value + ".jpg");
                return new BitmapImage(new Uri(filePath));
            }
            catch
            {
                
                return new BitmapImage(new Uri("pack://application:,,,/PLApp;component/Properties/default.jpg"));
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
