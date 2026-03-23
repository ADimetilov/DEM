using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace DEM
{
    public class SafeImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string filePath && File.Exists(filePath))
            {
                try
                {
                    using (var stream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read))
                    {
                        var bitmap = new BitmapImage();
                        bitmap.BeginInit();//Начало инициализации
                        bitmap.CacheOption = BitmapCacheOption.OnLoad; //Вызвобождение кэша
                        bitmap.StreamSource = stream; //Источник
                        bitmap.EndInit(); //Окончание инициализации
                        bitmap.Freeze(); //Блокировка потока для редактирование
                        return bitmap;
                    }
                }
                catch
                {
                    return new BitmapImage(new Uri("pack://application:,,,/Images/placeholder.png"));
                }
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
