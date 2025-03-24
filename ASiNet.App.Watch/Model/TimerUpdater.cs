using System.Diagnostics;

namespace ASiNet.App.Watch.Model;
public class TimerUpdater : ClockUpdaterBase
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
        if (CurrentTime.TotalSeconds <= 1)
            return;
        base.Start();
    }

    public override void Stop()
    {
        base.Stop();
    }

    public override void Reset()
    {
        _currentTime = TimeSpan.Zero;
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
        var newTime = time - offset;
        var newSecond = newTime.Seconds;
        var newMinute = newTime.Minutes;
        var newHour = (int)Math.Floor(newTime.TotalHours);
        if (newTime.TotalSeconds <= 0)
        {
            InvokeUpdateSecond(0);
            InvokeUpdateMinute(0);
            InvokeUpdateHour(0);
            InvokeTimeUp();
            Reset();
            Stop();
            return;
        }
        else
        {
            if (second != newSecond)
                InvokeUpdateSecond(newTime.Seconds);
            if (minute != newMinute)
                InvokeUpdateMinute(newMinute);
            if (hour != newHour)
                InvokeUpdateHour(newHour);
        }
#if DEBUG
        Debug.WriteLine($"TIMER_UPDATE: {time} -> {newTime}");
#endif
        _currentTime = newTime;
    }
}
