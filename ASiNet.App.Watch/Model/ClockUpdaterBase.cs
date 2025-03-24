using System.Diagnostics;

namespace ASiNet.App.Watch.Model;
public abstract class ClockUpdaterBase : IClockUpdater
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

    protected int _updateDelay = 100;

    private CancellationTokenSource? _cts;

    protected abstract Task Updater(CancellationToken token);

    public virtual void Reset()
    {

    }

    public virtual void Start()
    {
        if (_cts is not null && !_cts.IsCancellationRequested)
            return;

        _cts?.Dispose();
        _cts = new();
        _ = Updater(_cts.Token);
    }

    public virtual void Stop()
    {
        if (_cts is null || _cts.IsCancellationRequested)
            return;
        _cts?.Cancel();
    }

    protected virtual void InvokeUpdateSecond(int second)
    {
        var first = second / 10;
        var last = second % 10;
        UpdatedFirstSecond?.Invoke(first);
        UpdatedLastSecond?.Invoke(last);
#if DEBUG
        Debug.WriteLine($"SECOND_UPDATE: {second} [{first}:{last}]");
#endif
    }

    protected virtual void InvokeUpdateMinute(int minute)
    {
        var first = minute / 10;
        var last = minute % 10;
        UpdatedFirstMinute?.Invoke(first);
        UpdatedLastMinute?.Invoke(last);
#if DEBUG
        Debug.WriteLine($"MINUTE_UPDATE: {minute} [{first}:{last}]");
#endif
    }

    protected virtual void InvokeUpdateHour(int hour)
    {
        var first = hour / 10;
        var last = hour % 10;
        UpdatedFirstHour?.Invoke(first);
        UpdatedLastHour?.Invoke(last);
#if DEBUG
        Debug.WriteLine($"HOUR_UPDATE: {hour} [{first}:{last}]");
#endif
    }

    protected virtual void InvokeTimeUp()
    {
        TimeUp?.Invoke();
#if DEBUG
        Debug.WriteLine($"TIME_UP.");
#endif
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
