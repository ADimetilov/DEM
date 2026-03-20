using System;
using System.Collections.Generic;
using System.Windows.Media;
using System.Globalization;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace DEM
{
    public class PriceConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var product = value as Product;
            if (product.Sale ==0)
                return DependencyProperty.UnsetValue;

            var style = new Style(typeof(TextBlock));
            style.Setters.Add(new Setter(TextBlock.TextDecorationsProperty, TextDecorations.Strikethrough));
            style.Setters.Add(new Setter(TextBlock.ForegroundProperty, Brushes.Red));
            //style.Setters.Add(new Setter(TextBlock.ForegroundProperty, Brushes.Red));
            return style;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
