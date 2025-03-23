using System.ComponentModel;
using ASiNet.App.Watch.Model;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace ASiNet.App.Watch.ViewModel;
public partial class WatchWindowVM : ObservableObject
{
    public WatchWindowVM()
    {
        Parameters = new();
        WindowContextMenu = new();
        _watchUpdater = new();
        _timerUpdater = new();
        _stopWatchUpdater = new();
        _clockVM = new(_watchUpdater, Parameters);
        _stopWatchVM = new(_stopWatchUpdater, Parameters);
        _timerVM = new(_timerUpdater, Parameters);
    }

    [ObservableProperty]
    public partial ClockMode Mode { get; set; }

    [ObservableProperty]
    public partial ClockBaseVM Clock { get; set; } = null!;

    [ObservableProperty]
    public partial ParametersVM Parameters { get; set; }

    [ObservableProperty]
    public partial WindowContextMenuVM WindowContextMenu { get; set; }
    
    private WatchUpdater _watchUpdater;
    private TimerUpdater _timerUpdater;
    private StopWatchUpdater _stopWatchUpdater;
    
    private WatchVM _clockVM;
    private TimerVM _timerVM;
    private StopWatchVM _stopWatchVM;


    [RelayCommand]
    private void Init()
    {
        Mode = ClockMode.Clock;
    }

    [RelayCommand]
    private void Closed()
    {
        _stopWatchUpdater.Dispose();
        _watchUpdater.Dispose();
        _timerUpdater.Dispose();
    }

    [RelayCommand]
    private void StartClock()
    {
        Mode = ClockMode.Clock;
    }

    [RelayCommand]
    private void StartTimer()
    {
        Mode = ClockMode.Timer;
    }

    [RelayCommand]
    private void StartStopWatch()
    {
        Mode = ClockMode.StopWatch;
    }


    protected override void OnPropertyChanged(PropertyChangedEventArgs e)
    {
        if(e.PropertyName == nameof(Mode))
        {
            Clock?.Stop();
            switch (Mode)
            {
                case ClockMode.Disable:
                    Clock?.Stop();
                    break;
                case ClockMode.Clock:
                    Clock = _clockVM;
                    break;
                case ClockMode.Timer:
                    Clock = _timerVM;
                    break;
                case ClockMode.StopWatch:
                    Clock = _stopWatchVM;
                    break;
            }
            Clock?.Init();
        }
        base.OnPropertyChanged(e);
    }
}
