using System.Globalization;
using System.Windows;
using System.Windows.Data;
using ASiNet.App.Watch.Model;

namespace ASiNet.App.Watch.Converters;
public class ClockModeToVisConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        var v = (ClockMode)value;
        var p = (string)parameter;
        if (v is ClockMode.Clock && p is "clock")
            return Visibility.Visible;
        if (v is ClockMode.StopWatch && p is "stopwatch")
            return Visibility.Visible;
        if (v is ClockMode.Timer && p is "timer")
            return Visibility.Visible;
        return Visibility.Collapsed;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}