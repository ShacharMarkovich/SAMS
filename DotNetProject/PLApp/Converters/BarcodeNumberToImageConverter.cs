using System;
using System.Globalization;
using System.IO;
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
                string filePath = Path.Combine(Environment.CurrentDirectory, "Images", value.ToString() + ".jpg");
                return new BitmapImage(new Uri(filePath));
            }
            catch(Exception e)
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
