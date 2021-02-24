using System;
using System.Globalization;
using System.IO;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace PLApp.Converters
{
    public sealed class PathToImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                string filePath = Path.Combine(Environment.CurrentDirectory,"Images/",  value.ToString() + ".jpg");
                return new BitmapImage(new Uri(filePath));
            }
            catch
            {
                return new BitmapImage();
            }
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
