using System.Diagnostics;

namespace ASiNet.App.Watch.Model;
public class StopWatchUpdater : IClockUpdater
{
    public bool IsRunning => !_cts?.IsCancellationRequested ?? false;

    public event Action<int>? UpdatedFirstSecond;
    public event Action<int>? UpdatedLastSecond;
    public event Action<int>? UpdatedFirstMinute;
    public event Action<int>? UpdatedLastMinute;
    public event Action<int>? UpdatedFirstHour;
    public event Action<int>? UpdatedLastHour;
    public event Action? TimeUp;

    public double UPS
    {
        get => (double)1000 / (double)_updateDelay;
        set
        {
            if (value < 0)
                throw new IndexOutOfRangeException();
            if (value > 1000)
                throw new IndexOutOfRangeException();
            _updateDelay = (int)Math.Round(1000 / value);
        }
    }

    private int _updateDelay = 100;

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

    private CancellationTokenSource? _cts;

    public void Start()
    {
        if (_cts is not null && !_cts.IsCancellationRequested)
            return;

        _cts?.Dispose();
        _cts = new();
        _ = Updater(_cts.Token);
    }

    public void Stop()
    {
        if (_cts is null || _cts.IsCancellationRequested)
            return;
        _cts?.Cancel();
    }

    public void CallUpdate()
    {
        throw new NotImplementedException();
    }

    private async Task Updater(CancellationToken token)
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
            TimeUp?.Invoke();
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

    private void InvokeUpdateSecond(int second)
    {
        var first = second / 10;
        var last = second % 10;
        UpdatedFirstSecond?.Invoke(first);
        UpdatedLastSecond?.Invoke(last);
    }

    private void InvokeUpdateMinute(int minute)
    {
        var first = minute / 10;
        var last = minute % 10;
        UpdatedFirstMinute?.Invoke(first);
        UpdatedLastMinute?.Invoke(last);
    }

    private void InvokeUpdateHour(int hour)
    {
        var first = hour / 10;
        var last = hour % 10;
        UpdatedFirstHour?.Invoke(first);
        UpdatedLastHour?.Invoke(last);
    }

    public void Dispose()
    {
        if (_cts is not null)
        {
            if (!_cts.IsCancellationRequested)
                _cts.Cancel();
            _cts.Dispose();
        }
        UpdatedFirstHour = null;
        UpdatedLastHour = null;
        UpdatedFirstMinute = null;
        UpdatedLastMinute = null;
        UpdatedFirstSecond = null;
        UpdatedLastSecond = null;
        TimeUp = null;
    }
}
