using System.ComponentModel;
using System.Windows.Media;
using ASiNet.App.Watch.Model.Config;
using ASiNet.App.Watch.View;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace ASiNet.App.Watch.ViewModel;
public partial class ParametersVM : ObservableObject
{

    public ParametersVM()
    {
        var cnf = ClockConfig.ReadOrDefault();
        _config = cnf;
        UpdateValuesFromConfig();
    }

    [ObservableProperty]
    public partial bool HideMoreOptions { get; set; }
    [ObservableProperty]
    public partial Brush SelectedSegmentColor { get; set; } = null!;
    [ObservableProperty]
    public partial Brush ActiveSegmentColor { get; set; } = null!;
    [ObservableProperty]
    public partial Brush InactiveSegmentColor { get; set; } = null!;
    [ObservableProperty]
    public partial int HourSpace { get; set; }
    [ObservableProperty]
    public partial int HourMinuteSplitterSpace { get; set; }
    [ObservableProperty]
    public partial int MinuteSpace { get; set; }
    [ObservableProperty]
    public partial int MinuteSecondSpace { get; set; }
    [ObservableProperty]
    public partial int SecondSpace { get; set; }


    private ClockConfig _config;

    [RelayCommand]
    private void ShowHideMoreOptions()
    {
        HideMoreOptions = !HideMoreOptions;
        ClockConfig.Write(_config);
    }


    [RelayCommand]
    private void OpenClockSpacingOptions()
    {
        var ow = new ClockSpacingOptionsWindow() { DataContext = this };
        ow.ShowDialog();
        ClockConfig.Write(_config);
    }

    [RelayCommand]
    private void SaveConfig()
    {
        ClockConfig.Write(_config);
    }


    [RelayCommand]
    private void LoadConfig()
    {
        _config = ClockConfig.ReadOrDefault();
        UpdateValuesFromConfig();
    }


    protected override void OnPropertyChanged(PropertyChangedEventArgs e)
    {
        switch (e.PropertyName)
        {
            case nameof(HideMoreOptions):
                _config.HideMoreOptions = HideMoreOptions;
                break;
            case nameof(SelectedSegmentColor):
                _config.Colors.HourFirst.SelectedSegmentColor = ConfigBrush.FromBrush(SelectedSegmentColor);
                break;
            case nameof(ActiveSegmentColor):
                _config.Colors.HourFirst.ActiveSegmentColor = ConfigBrush.FromBrush(ActiveSegmentColor);
                break;
            case nameof(InactiveSegmentColor):
                _config.Colors.HourFirst.InactiveSegmentColor = ConfigBrush.FromBrush(InactiveSegmentColor);
                break;
            case nameof(HourSpace):
                _config.ClockSegmentsSpacing.HourSpace = HourSpace;
                break;
            case nameof(HourMinuteSplitterSpace):
                _config.ClockSegmentsSpacing.HourMinuteSplitterSpace = HourMinuteSplitterSpace;
                break;
            case nameof(MinuteSpace):
                _config.ClockSegmentsSpacing.MinuteSpace = MinuteSpace;
                break;
            case nameof(MinuteSecondSpace):
                _config.ClockSegmentsSpacing.MinuteSecondSpace = MinuteSecondSpace;
                break;
            case nameof(SecondSpace):
                _config.ClockSegmentsSpacing.SecondSpace = SecondSpace;
                break;
        }

        base.OnPropertyChanged(e);
    }


    private void UpdateValuesFromConfig()
    {
        HideMoreOptions = _config.HideMoreOptions;
        InactiveSegmentColor = ConfigBrush.ReadBrush(_config.Colors.HourFirst.InactiveSegmentColor);
        ActiveSegmentColor = ConfigBrush.ReadBrush(_config.Colors.HourFirst.ActiveSegmentColor);
        SelectedSegmentColor = ConfigBrush.ReadBrush(_config.Colors.HourFirst.SelectedSegmentColor);
        HourSpace = _config.ClockSegmentsSpacing.HourSpace;
        MinuteSpace = _config.ClockSegmentsSpacing.MinuteSpace;
        SecondSpace = _config.ClockSegmentsSpacing.SecondSpace;
        MinuteSecondSpace = _config.ClockSegmentsSpacing.MinuteSecondSpace;
        HourMinuteSplitterSpace = _config.ClockSegmentsSpacing.HourMinuteSplitterSpace;
    }
}
