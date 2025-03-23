using System.Windows.Media;
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


    [RelayCommand]
    private void ShowHideMoreOptions()
    {
        HideMoreOptions = !HideMoreOptions;
    }

}
