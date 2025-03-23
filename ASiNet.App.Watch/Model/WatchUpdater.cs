using System.CodeDom.Compiler;
using System.Diagnostics;

namespace ASiNet.App.Watch.Model;
public class WatchUpdater : IClockUpdater
{
    public event Action<int>? UpdatedFirstSecond;
    public event Action<int>? UpdatedLastSecond;
    public event Action<int>? UpdatedFirstMinute;
    public event Action<int>? UpdatedLastMinute;
    public event Action<int>? UpdatedFirstHour;
    public event Action<int>? UpdatedLastHour;


    public bool IsRunning => !_cts?.IsCancellationRequested ?? false;

    public double UPS
    { 
        get => (double)1000 / (double)_updateDelay;
        set
        {
            if(value < 0)
                throw new IndexOutOfRangeException();
            if(value > 1000)
                throw new IndexOutOfRangeException();
            _updateDelay = (int)Math.Round(1000 / value);
        }
    }

    private int _updateDelay = 100;

    private int _lastHour = -1;
    private int _lastMinute = -1;
    private int _lastSecond = -1;

    private CancellationTokenSource? _cts;

    public void Start()
    {
        if (_cts is not null && !_cts.IsCancellationRequested)
            return;

        _cts?.Dispose();
        _cts = new();
        UpdateTime();
        _ = Updater(_cts.Token);
    }

    public void Stop()
    {
        if (_cts is null || _cts.IsCancellationRequested)
            return;
        _cts?.Cancel();
        _lastHour = -1;
        _lastMinute = -1;
        _lastSecond = -1;
    }


    public void CallUpdate()
    {
        UpdateTime();
    }

    private async Task Updater(CancellationToken token)
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
        {
            var first = second / 10;
            var last = second % 10;
            UpdatedFirstSecond?.Invoke(first);
            UpdatedLastSecond?.Invoke(last);
#if DEBUG
            Debug.WriteLine($"SECOND_UPDATE: {_lastSecond} -> {second} [{first}:{last}]");
#endif
        }
        if (minute != _lastMinute)
        {
            var first = minute / 10;
            var last = minute % 10;
            UpdatedFirstMinute?.Invoke(first);
            UpdatedLastMinute?.Invoke(last);
#if DEBUG
            Debug.WriteLine($"MINUTE_UPDATE: {_lastMinute} -> {minute} [{first}:{last}]");
#endif
        }
        if (hour != _lastHour)
        {
            var first = hour / 10;
            var last = hour % 10;
            UpdatedFirstHour?.Invoke(first);
            UpdatedLastHour?.Invoke(last);
#if DEBUG
            Debug.WriteLine($"HOUR_UPDATE: {_lastHour} -> {hour} [{first}:{last}]");
#endif
        }
        _lastMinute = minute;
        _lastHour = hour;
        _lastSecond = second;
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
    }
}
