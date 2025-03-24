using System.ComponentModel;
using ASiNet.App.Watch.Model;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace ASiNet.App.Watch.ViewModel;
public abstract partial class ClockBaseVM : ObservableObject
{
    protected ClockBaseVM(IClockUpdater updater, ParametersVM parameters)
    {
        ClockParameters = parameters;

        SecondFirst = new(parameters);
        SecondLast = new(parameters);
        MinuteFirst = new(parameters);
        MinuteLast = new(parameters);
        HourFirst = new(parameters);
        HourLast = new(parameters);
        HourMinuteSplitter = new(parameters);

        InteractiveResult = TimeSpan.FromSeconds(1);
        updater.UpdatedFirstSecond += SecondFirst.UpdateNumber;
        updater.UpdatedLastSecond += SecondLast.UpdateNumber;
        updater.UpdatedFirstMinute += MinuteFirst.UpdateNumber;
        updater.UpdatedLastMinute += MinuteLast.UpdateNumber;
        updater.UpdatedFirstHour += HourFirst.UpdateNumber;
        updater.UpdatedLastHour += HourLast.UpdateNumber;
        updater.UpdatedLastSecond += HourMinuteSplitter.SwitchActive;
    }

    [ObservableProperty]
    public partial ParametersVM ClockParameters { get; set; }

    [ObservableProperty]
    public partial bool ShowOptions { get; set; }

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


    public void SetAllIndicatorsToValue(TimeSpan value)
    {
        (HourFirst.Number, HourLast.Number) = value.ExtractSplitHours();
        (MinuteFirst.Number, MinuteLast.Number) = value.ExtractSplitMinutes();
        (SecondFirst.Number, SecondLast.Number) = value.ExtractSplitSeconds();
    }

    [RelayCommand]
    public abstract void Init();


    [RelayCommand]
    public abstract void Start();


    [RelayCommand]
    public abstract void Stop();

    [RelayCommand]
    public abstract void Reset();

    [RelayCommand]
    public virtual void StartStop()
    {
        if(IsRunning)
            Stop();
        else
            Start();
    }

    protected override void OnPropertyChanged(PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(InteractiveResult))
            SetAllIndicatorsToValue(InteractiveResult);
        base.OnPropertyChanged(e);
    }
}
