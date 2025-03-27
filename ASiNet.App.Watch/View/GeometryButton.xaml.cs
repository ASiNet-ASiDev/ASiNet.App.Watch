using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace ASiNet.App.Watch.View;
/// <summary>
/// Логика взаимодействия для GeometryButton.xaml
/// </summary>
public partial class GeometryButton : Button
{
    public GeometryButton()
    {
        InitializeComponent();
    }

    public static readonly DependencyProperty GeometryProperty = DependencyProperty.Register(nameof(Geometry), typeof(Geometry), typeof(GeometryButton));
    public static readonly DependencyProperty TextProperty = DependencyProperty.Register(nameof(Text), typeof(string), typeof(GeometryButton));
    public static readonly DependencyProperty ShowTextProperty = DependencyProperty.Register(nameof(ShowText), typeof(bool), typeof(GeometryButton));
    public static readonly DependencyProperty GeometryColorProperty = DependencyProperty.Register(nameof(GeometryColor), typeof(Brush), typeof(GeometryButton));
    public static readonly DependencyProperty TextColorProperty = DependencyProperty.Register(nameof(TextColor), typeof(Brush), typeof(GeometryButton));

    public static readonly DependencyProperty MouseEnterGeometryColorProperty = DependencyProperty.Register(nameof(MouseEnterGeometryColor), typeof(Brush), typeof(GeometryButton));
    public static readonly DependencyProperty PressedGeometryColorProperty = DependencyProperty.Register(nameof(PressedGeometryColor), typeof(Brush), typeof(GeometryButton));

    public static readonly DependencyProperty MouseEnterTextColorProperty = DependencyProperty.Register(nameof(MouseEnterTextColor), typeof(Brush), typeof(GeometryButton));
    public static readonly DependencyProperty PressedTextColorProperty = DependencyProperty.Register(nameof(PressedTextColor), typeof(Brush), typeof(GeometryButton));

    public Geometry? Geometry
    {
        get => (Geometry)GetValue(GeometryProperty); 
        set => SetValue(GeometryProperty, value);
    }

    public string? Text
    {
        get => (string)GetValue(TextProperty);
        set => SetValue(TextProperty, value);
    }

    public bool ShowText
    {
        get => (bool)GetValue(ShowTextProperty);
        set => SetValue(ShowTextProperty, value);
    }

    public Brush? GeometryColor
    {
        get => (Brush)GetValue(GeometryColorProperty);
        set => SetValue(GeometryColorProperty, value);
    }

    public Brush? MouseEnterGeometryColor
    {
        get => (Brush)GetValue(MouseEnterGeometryColorProperty);
        set => SetValue(MouseEnterGeometryColorProperty, value);
    }

    public Brush? PressedGeometryColor
    {
        get => (Brush)GetValue(PressedGeometryColorProperty);
        set => SetValue(PressedGeometryColorProperty, value);
    }

    public Brush? TextColor
    {
        get => (Brush)GetValue(TextColorProperty);
        set => SetValue(TextColorProperty, value);
    }

    public Brush? MouseEnterTextColor
    {
        get => (Brush)GetValue(MouseEnterTextColorProperty);
        set => SetValue(MouseEnterTextColorProperty, value);
    }

    public Brush? PressedTextColor
    {
        get => (Brush)GetValue(PressedTextColorProperty);
        set => SetValue(PressedTextColorProperty, value);
    }
}
