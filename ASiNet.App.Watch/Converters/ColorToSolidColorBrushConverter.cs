using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace ASiNet.App.Watch.Converters;
public class ColorToSolidColorBrushConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if(value is Color)
            return (Color)value;
        else if(value is SolidColorBrush brush)
            return brush.Color;

        throw new NotImplementedException();
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is Color)
            return new SolidColorBrush((Color)value);
        else if (value is SolidColorBrush brush)
            return brush;

        throw new NotImplementedException();
    }
}
