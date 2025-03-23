namespace ASiNet.App.Watch.Model;
public static class ClockHelper
{


    public static (int first, int last) ExtractSplitHours(this TimeSpan timeSpan)
    {
        var totalHours = (int)Math.Floor(timeSpan.TotalHours);
        var first = totalHours / 10;
        var last = totalHours % 10;
        return (first, last);
    }

    public static (int first, int last) ExtractSplitMinutes(this TimeSpan timeSpan)
    {
        var totalHours = timeSpan.Minutes;
        var first = totalHours / 10;
        var last = totalHours % 10;
        return (first, last);
    }

    public static (int first, int last) ExtractSplitSeconds(this TimeSpan timeSpan)
    {
        var totalHours = timeSpan.Seconds;
        var first = totalHours / 10;
        var last = totalHours % 10;
        return (first, last);
    }

}
