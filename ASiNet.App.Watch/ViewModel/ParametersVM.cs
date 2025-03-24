using System.Windows.Media;
using ASiNet.App.Watch.View;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace ASiNet.App.Watch.ViewModel;
public partial class ParametersVM : ObservableObject
{

    public ParametersVM()
    {
        InactiveSegmentColor = new SolidColorBrush(Color.FromArgb(0, 0, 0, 0));
        ActiveSegmentColor = new SolidColorBrush(Colors.White);
        SelectedSegmentColor = new SolidColorBrush(Colors.Red);
    }

    [ObservableProperty]
    public partial bool HideMoreOptions { get; set; }
    [ObservableProperty]
    public partial Brush SelectedSegmentColor { get; set; }
    [ObservableProperty]
    public partial Brush ActiveSegmentColor { get; set; }
    [ObservableProperty]
    public partial Brush InactiveSegmentColor { get; set; }
    [ObservableProperty]
    public partial int HourSpace { get; set; } = 10;
    [ObservableProperty]
    public partial int HourMinuteSplitterSpace { get; set; } = 20;
    [ObservableProperty]
    public partial int MinuteSpace { get; set; } = 10;
    [ObservableProperty]
    public partial int MinuteSecondSpace { get; set; } = 25;
    [ObservableProperty]
    public partial int SecondSpace { get; set; } = 10;

    [RelayCommand]
    private void ShowHideMoreOptions()
    {
        HideMoreOptions = !HideMoreOptions;
    }


    [RelayCommand]
    private void OpenClockSpacingOptions()
    {
        var ow = new ClockSpacingOptionsWindow() { DataContext = this };
        ow.ShowDialog();
    }
}
