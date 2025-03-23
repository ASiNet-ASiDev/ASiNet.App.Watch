using ASiNet.App.Watch.Model;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace ASiNet.App.Watch.ViewModel;
public partial class WatchWindowVM : ObservableObject
{
    public WatchWindowVM()
    {
        Parameters = new();
        _watchUpdater = new();
        _timerUpdater = new();
        _stopWatchUpdater = new();
        WindowContextMenu = new();
        Clock = new(_watchUpdater, _timerUpdater, _stopWatchUpdater, Parameters);
    }

    [ObservableProperty]
    public partial ClockVM Clock { get; set; }

    [ObservableProperty]
    public partial ParametersVM Parameters { get; set; }

    [ObservableProperty]
    public partial WindowContextMenuVM WindowContextMenu { get; set; }
    
    private WatchUpdater _watchUpdater;
    private TimerUpdater _timerUpdater;
    private StopWatchUpdater _stopWatchUpdater;
    

    [RelayCommand]
    private void Init()
    {
        StartClock();
    }

    [RelayCommand]
    private void Closed()
    {
        _watchUpdater.Dispose();
        _timerUpdater.Dispose();
    }

    [RelayCommand]
    private void StartClock()
    {
        Clock.Mode = ClockMode.Clock;
    }

    [RelayCommand]
    private void StartTimer()
    {
        Clock.Mode = ClockMode.Timer;
    }

    [RelayCommand]
    private void StartStopWatch()
    {
        Clock.Mode = ClockMode.StopWatch;
    }
}
