namespace ASiNet.App.Watch.Model;
public class WatchUpdater : ClockUpdaterBase
{
    private int _lastHour = -1;
    private int _lastMinute = -1;
    private int _lastSecond = -1;

    public override void Stop()
    {
        base.Stop();
        Reset();
    }

    public override void Reset()
    {
        _lastHour = -1;
        _lastMinute = -1;
        _lastSecond = -1;
    }


    protected override async Task Updater(CancellationToken token)
    {
        UpdateTime();
        while (!token.IsCancellationRequested)
        {
            UpdateTime();
            try
            {
                await Task.Delay(_updateDelay, token);
            }
            catch { }
        }
    }

    private void UpdateTime()
    {
        var dt = DateTime.Now;
        var second = dt.Second;
        var minute = dt.Minute;
        var hour = dt.Hour;
        if (second != _lastSecond)
            InvokeUpdateSecond(second);
        if (minute != _lastMinute)
            InvokeUpdateMinute(minute);
        if (hour != _lastHour)
            InvokeUpdateHour(hour);
        _lastMinute = minute;
        _lastHour = hour;
        _lastSecond = second;
    }
}
