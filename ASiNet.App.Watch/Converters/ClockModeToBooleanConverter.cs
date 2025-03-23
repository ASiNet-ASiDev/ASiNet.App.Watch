using System.Globalization;
using System.Windows.Data;
using ASiNet.App.Watch.Model;

namespace ASiNet.App.Watch.Converters;
public class ClockModeToBooleanConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        var v = (ClockMode)value;
        var p = (string)parameter;
        if (v is ClockMode.Clock && p is "clock")
            return true;
        if (v is ClockMode.StopWatch && p is "stopwatch")
            return true;
        if (v is ClockMode.Timer && p is "timer")
            return true;
        return false;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
