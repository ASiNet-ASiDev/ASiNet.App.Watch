using System.Windows.Media;
using System.Windows.Threading;
using CommunityToolkit.Mvvm.ComponentModel;

namespace ASiNet.App.Watch.ViewModel;
public partial class NumberVM : ObservableObject
{
    public NumberVM(ParametersVM parameters)
    {
        SelectedColor = parameters.SelectedSegmentColor;
        InactiveColor = parameters.InactiveSegmentColor;
        ActiveColor = parameters.ActiveSegmentColor;
    }


    [ObservableProperty]
    public partial int Number { get; set; } = -1;

    [ObservableProperty]
    public partial bool Active { get; set; } = true;


    [ObservableProperty]
    public partial int AnimationNumber { get; set; }

    [ObservableProperty]
    public partial Brush ActiveColor { get; set; }

    [ObservableProperty]
    public partial Brush InactiveColor { get; set; }

    [ObservableProperty]
    public partial Brush SelectedColor { get; set; }


    public void UpdateNumber(int number)
    {
        Dispatcher.CurrentDispatcher.Invoke(() => Number = number);
    }

    public void SwitchActive(int _)
    {
        Dispatcher.CurrentDispatcher.Invoke(() => Active =! Active);
    }

}
