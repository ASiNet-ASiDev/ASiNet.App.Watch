using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace ASiNet.App.Watch.Converters;
public class IntToGridLengthConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        var res = new GridLength((int)value, GridUnitType.Pixel);
        return res;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
