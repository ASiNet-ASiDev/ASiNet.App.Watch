using System.ComponentModel;
using System.Windows.Media;
using ASiNet.App.Watch.Model.Config;
using CommunityToolkit.Mvvm.ComponentModel;

namespace ASiNet.App.Watch.ViewModel;
public partial class ButtonParametersVM : ObservableObject
{
    public ButtonParametersVM(ButtonOptions options)
    {
        _colors = options;
        SetColors(_colors);
    }

    [ObservableProperty]
    public partial bool ShowText { get; set; }

    [ObservableProperty]
    public partial Brush MouseEnterGeometryColor { get; set; } = null!;

    [ObservableProperty]
    public partial Brush MouseEnterTextColor { get; set; } = null!;

    [ObservableProperty]
    public partial Brush DefaultGeometryColor { get; set; } = null!;

    [ObservableProperty]
    public partial Brush DefaultTextColor { get; set; } = null!;

    [ObservableProperty]
    public partial Brush PressedGeometryColor { get; set; } = null!;

    [ObservableProperty]
    public partial Brush PressedTextColor { get; set; } = null!;



    private ButtonOptions _colors;



    public void SetColors(ButtonOptions options)
    {
        _colors = options;
        ShowText = options.ShowText;
        MouseEnterGeometryColor = ConfigBrush.ReadBrush(options.MouseEnterGeometryColor);
        MouseEnterTextColor = ConfigBrush.ReadBrush(options.MouseEnterTextColor);
        DefaultGeometryColor = ConfigBrush.ReadBrush(options.GeometryColor);
        DefaultTextColor = ConfigBrush.ReadBrush(options.TextColor);
        PressedGeometryColor = ConfigBrush.ReadBrush(options.PressedGeometryColor);
        PressedTextColor = ConfigBrush.ReadBrush(options.PressedTextColor);
    }

    protected override void OnPropertyChanged(PropertyChangedEventArgs e)
    {
        switch (e.PropertyName)
        {
            case nameof(MouseEnterGeometryColor):
                _colors.MouseEnterGeometryColor = ConfigBrush.FromBrush(MouseEnterGeometryColor);
                break;
            case nameof(MouseEnterTextColor):
                _colors.MouseEnterTextColor = ConfigBrush.FromBrush(MouseEnterTextColor);
                break;
            case nameof(DefaultGeometryColor):
                _colors.GeometryColor = ConfigBrush.FromBrush(DefaultGeometryColor);
                break;
            case nameof(DefaultTextColor):
                _colors.TextColor = ConfigBrush.FromBrush(DefaultTextColor);
                break;
            case nameof(PressedGeometryColor):
                _colors.PressedGeometryColor = ConfigBrush.FromBrush(PressedGeometryColor);
                break;
            case nameof(PressedTextColor):
                _colors.PressedTextColor = ConfigBrush.FromBrush(PressedTextColor);
                break;
            case nameof(ShowText):
                _colors.ShowText = ShowText;
                break;
        }

        base.OnPropertyChanged(e);
    }
}
