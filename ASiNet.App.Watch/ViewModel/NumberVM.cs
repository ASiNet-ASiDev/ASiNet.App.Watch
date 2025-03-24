using System.ComponentModel;
using System.Windows.Media;
using System.Windows.Threading;
using CommunityToolkit.Mvvm.ComponentModel;

namespace ASiNet.App.Watch.ViewModel;
public partial class NumberVM : ObservableObject
{
    public NumberVM(ParametersVM parameters)
    {
        Parameters = parameters;
        Parameters.PropertyChanged += OnParametersChanged;
        SelectedColor = parameters.SelectedSegmentColor;
        InactiveColor = parameters.InactiveSegmentColor;
        ActiveColor = parameters.ActiveSegmentColor;
    }

    [ObservableProperty]
    public partial ParametersVM Parameters { get; set; }

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



    private void OnParametersChanged(object? sender, PropertyChangedEventArgs e)
    {
        if(e.PropertyName == nameof(Parameters.ActiveSegmentColor))
            ActiveColor = Parameters.ActiveSegmentColor;
        else if (e.PropertyName == nameof(Parameters.InactiveSegmentColor))
            InactiveColor = Parameters.InactiveSegmentColor;
        else if (e.PropertyName == nameof(Parameters.SelectedSegmentColor))
            SelectedColor = Parameters.SelectedSegmentColor;
    }
}
