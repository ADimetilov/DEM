using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows.Data;
using System.Windows.Media;

namespace DEM
{
    public class ScoreProductConvertor: IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var score = value;
            if ((System.Convert.ToInt32(score)) == 0)
            {
                return new SolidColorBrush((Color)ColorConverter.ConvertFromString("#42aaff"));
            }
            return Brushes.Transparent;
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
