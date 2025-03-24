using System.Diagnostics;

namespace ASiNet.App.Watch.Model;
public class StopWatchUpdater : ClockUpdaterBase
{
    public TimeSpan CurrentTime
    {
        get => _currentTime;
        set
        {
            if (IsRunning)
                throw new Exception();
            _currentTime = value;
        }
    }

    private TimeSpan _currentTime = TimeSpan.Zero;

    public override void Start()
    {
        base.Start();
    }

    public override void Stop()
    {
        base.Stop();
    }

    public override void Reset()
    {
        CurrentTime = TimeSpan.Zero;
    }

    protected override async Task Updater(CancellationToken token)
    {
        InvokeUpdateSecond(_currentTime.Seconds);
        InvokeUpdateMinute(_currentTime.Minutes);
        InvokeUpdateHour((int)Math.Floor(_currentTime.TotalHours));
        var lastTime = DateTime.Now;
        while (!token.IsCancellationRequested)
        {
            var dt = DateTime.Now;
            var time = dt - lastTime;
            if (time.TotalSeconds >= 1)
            {
                UpdateTime(_currentTime, TimeSpan.FromSeconds(time.Seconds));
                lastTime = dt;
#if DEBUG
                Debug.WriteLine($"STOPWATCH_TIME_RECALC: [{lastTime:G} -> {dt:G}] {time}");
#endif
            }
            try
            {
                await Task.Delay(_updateDelay, token);
            }
            catch { }
        }
    }
    private void UpdateTime(TimeSpan time, TimeSpan offset)
    {
        var second = time.Seconds;
        var minute = time.Minutes;
        var hour = (int)Math.Floor(time.TotalHours);
        var newTime = time + offset;
        var newSecond = newTime.Seconds;
        var newMinute = newTime.Minutes;
        var newHour = (int)Math.Floor(newTime.TotalHours);
        if (newTime.TotalHours >= 99)
        {
            InvokeUpdateSecond(59);
            InvokeUpdateMinute(59);
            InvokeUpdateHour(99);
            _currentTime = new(99, 59, 59);
            InvokeTimeUp();
            Stop();
#if DEBUG
            Debug.WriteLine($"STOPWATCH_TIMEUP: {time} -> {newTime}");
#endif
            return;
        }
        else
        {
            if (second != newSecond)
                InvokeUpdateSecond(newSecond);
            if (minute != newMinute)
                InvokeUpdateMinute(newMinute);
            if (hour != newHour)
                InvokeUpdateHour(newHour);
        }
#if DEBUG
        Debug.WriteLine($"STOPWATCH_UPDATE: {time} -> {newTime}");
#endif
        _currentTime = newTime;
    }
}
