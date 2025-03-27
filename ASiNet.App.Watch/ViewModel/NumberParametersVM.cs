using System.ComponentModel;
using System.Windows.Media;
using ASiNet.App.Watch.Model.Config;
using CommunityToolkit.Mvvm.ComponentModel;

namespace ASiNet.App.Watch.ViewModel;
public partial class NumberParametersVM : ObservableObject
{

    public NumberParametersVM(SegmentColors colors)
    {
        _colors = colors;
        ActiveColor = ConfigBrush.ReadBrush(colors.ActiveSegmentColor);
        InactiveColor = ConfigBrush.ReadBrush(colors.InactiveSegmentColor);
        SelectedColor = ConfigBrush.ReadBrush(colors.SelectedSegmentColor);
    }

    [ObservableProperty]
    public partial Brush ActiveColor { get; set; }

    [ObservableProperty]
    public partial Brush InactiveColor { get; set; }

    [ObservableProperty]
    public partial Brush SelectedColor { get; set; }

    private SegmentColors _colors;



    public void SetColors(SegmentColors colors)
    {
        _colors = colors;
        ActiveColor = ConfigBrush.ReadBrush(colors.ActiveSegmentColor);
        InactiveColor = ConfigBrush.ReadBrush(colors.InactiveSegmentColor);
        SelectedColor = ConfigBrush.ReadBrush(colors.SelectedSegmentColor);
    }

    protected override void OnPropertyChanged(PropertyChangedEventArgs e)
    {
        switch (e.PropertyName)
        {
            case nameof(ActiveColor):
                _colors.ActiveSegmentColor = ConfigBrush.FromBrush(ActiveColor);
                break;
            case nameof(InactiveColor):
                _colors.InactiveSegmentColor = ConfigBrush.FromBrush(InactiveColor);
                break;
            case nameof(SelectedColor):
                _colors.SelectedSegmentColor = ConfigBrush.FromBrush(SelectedColor);
                break;
        }

        base.OnPropertyChanged(e);
    }

}
