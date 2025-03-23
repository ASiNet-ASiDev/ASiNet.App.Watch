using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ASiNet.App.Watch.Model;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace ASiNet.App.Watch.ViewModel;
public partial class ClockVM : ObservableObject
{

    public ClockVM(WatchUpdater watchUpdater, TimerUpdater timerUpdater, StopWatchUpdater stopWatchUpdater, ParametersVM parameters, ClockMode defaultMode = ClockMode.Disable)
    {
        _watchUpdater = watchUpdater;
        _timerUpdater = timerUpdater;
        _stopWatchUpdater = stopWatchUpdater;
        SecondFirst = new(parameters);
        SecondLast = new(parameters);
        MinuteFirst = new(parameters);
        MinuteLast = new(parameters);
        HourFirst = new(parameters);
        HourLast = new(parameters);
        HourMinuteSplitter = new(parameters);

        InteractiveResult = TimeSpan.FromSeconds(1);

        SubscribeAllUpdaters([_watchUpdater, _timerUpdater, _stopWatchUpdater]);
        _timerUpdater.TimeUp += OnTimerTimeUp;
        _stopWatchUpdater.TimeUp += OnStopWatchTimeUp;
        Mode = defaultMode;
    }

    [ObservableProperty]
    public partial ClockMode Mode { get; set; }

    [ObservableProperty]
    public partial TimeSpan InteractiveResult { get; set; }

    [ObservableProperty]
    public partial bool IsRunning { get; set; }

    [ObservableProperty]
    public partial bool Interactive { get; set; }

    [ObservableProperty]
    public partial NumberVM SecondFirst { get; set; }
    [ObservableProperty]
    public partial NumberVM SecondLast { get; set; }

    [ObservableProperty]
    public partial NumberVM MinuteFirst { get; set; }
    [ObservableProperty]
    public partial NumberVM MinuteLast { get; set; }

    [ObservableProperty]
    public partial NumberVM HourFirst { get; set; }
    [ObservableProperty]
    public partial NumberVM HourLast { get; set; }

    [ObservableProperty]
    public partial NumberVM HourMinuteSplitter { get; set; }

    private WatchUpdater _watchUpdater;
    private TimerUpdater _timerUpdater;
    private StopWatchUpdater _stopWatchUpdater;

    public void ResetAllIndicatorsToLastTimerValue()
    {
        if (Mode is ClockMode.Clock or ClockMode.StopWatch)
            return;
        (HourFirst.Number, HourLast.Number) = InteractiveResult.ExtractSplitHours();
        (MinuteFirst.Number, MinuteLast.Number) = InteractiveResult.ExtractSplitMinutes();
        (SecondFirst.Number, SecondLast.Number) = InteractiveResult.ExtractSplitSeconds();
    }


    private void ResetAllIndicatorsToLastStopWatchValue()
    {
        if(Mode is ClockMode.Clock or ClockMode.Timer)
            return;
        (HourFirst.Number, HourLast.Number) = _stopWatchUpdater.CurrentTime.ExtractSplitHours();
        (MinuteFirst.Number, MinuteLast.Number) = _stopWatchUpdater.CurrentTime.ExtractSplitMinutes();
        (SecondFirst.Number, SecondLast.Number) = _stopWatchUpdater.CurrentTime.ExtractSplitSeconds();
    }


    [RelayCommand]
    private void StartStopTimer()
    {
        if (IsRunning)
        {
            _timerUpdater.Stop();
            IsRunning = false;
            Interactive = true;
        }
        else
        {
            if(_timerUpdater.CurrentTime == TimeSpan.Zero)
                _timerUpdater.CurrentTime = InteractiveResult;
            _timerUpdater.Start();
            Interactive = false;
            IsRunning = true;
        }
    }

    [RelayCommand]
    private void StartStopStopWatch()
    {
        if (IsRunning)
        {
            _stopWatchUpdater.Stop();
            IsRunning = false;
            Interactive= false;
        }
        else
        {
            _stopWatchUpdater.Start();
            Interactive = false;
            IsRunning = true;
        }
    }

    [RelayCommand]
    private void RestTimer()
    {
        if(IsRunning)
            return;
        _timerUpdater.CurrentTime = InteractiveResult;
        ResetAllIndicatorsToLastTimerValue();
    }

    [RelayCommand]
    private void RestStopWatch()
    {
        if (IsRunning)
            return;
        _stopWatchUpdater.CurrentTime = TimeSpan.Zero;
        ResetAllIndicatorsToLastStopWatchValue();
    }

    protected override void OnPropertyChanged(PropertyChangedEventArgs e)
    {
        if(e.PropertyName == nameof(InteractiveResult))
        {
            RestTimer();
        }
        else if (e.PropertyName == nameof(Mode))
        {
            switch (Mode)
            {
                case ClockMode.Clock:
                    InactiveAsClock();
                    InactiveAsTimer();
                    ActiveAsClock();

                    break;
                case ClockMode.StopWatch:
                    InactiveAsClock();
                    InactiveAsTimer();
                    ActiveAsStopWatch();
                    
                    break;
                case ClockMode.Timer:
                    InactiveAsClock();
                    InactiveAsStopWatch();
                    ActiveAsTimer();
                    break;
                case ClockMode.Disable:
                    InactiveAsClock();
                    InactiveAsStopWatch();
                    InactiveAsTimer();
                    break;
            }
        }
        base.OnPropertyChanged(e);
    }

    private void OnTimerTimeUp()
    {
        IsRunning = false;
        Interactive = true;
        HourMinuteSplitter.Active = true;
    }

    private void OnStopWatchTimeUp()
    {
        IsRunning = false;
        Interactive = false;
    }

    private void ActiveAsClock()
    {
        Interactive = false;
        IsRunning = true;
        _watchUpdater.Start();
    }

    private void InactiveAsClock()
    {
        Interactive = false;
        IsRunning = false;
        _watchUpdater.Stop();
        HourMinuteSplitter.Active = true;
    }

    private void ActiveAsStopWatch()
    {
        Interactive = false;
        IsRunning = false;
        ResetAllIndicatorsToLastStopWatchValue();
    }

    private void InactiveAsStopWatch()
    {
        IsRunning = false;
        Interactive = false;
        _stopWatchUpdater.Stop();
        HourMinuteSplitter.Active = true;
    }

    private void ActiveAsTimer()
    {
        IsRunning = false;
        Interactive = true;
        ResetAllIndicatorsToLastTimerValue();
    }

    private void InactiveAsTimer()
    {
        IsRunning = false;
        Interactive = false;
        _timerUpdater.Stop();
    }

    private void SubscribeAllUpdaters(params IClockUpdater[] updaters)
    {
        foreach (var item in updaters)
        {
            item.UpdatedFirstSecond += SecondFirst.UpdateNumber;
            item.UpdatedLastSecond += SecondLast.UpdateNumber;
            item.UpdatedFirstMinute += MinuteFirst.UpdateNumber;
            item.UpdatedLastMinute += MinuteLast.UpdateNumber;
            item.UpdatedFirstHour += HourFirst.UpdateNumber;
            item.UpdatedLastHour += HourLast.UpdateNumber;
            item.UpdatedLastSecond += HourMinuteSplitter.SwitchActive;
        }
    }

}
