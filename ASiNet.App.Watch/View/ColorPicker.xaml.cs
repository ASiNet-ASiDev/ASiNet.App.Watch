using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using ASiNet.App.Watch.ViewModel;

namespace ASiNet.App.Watch.View;
/// <summary>
/// Логика взаимодействия для ColorPiker.xaml
/// </summary>
public partial class ColorPicker : UserControl
{
    public ColorPicker()
    {
        InitializeComponent();
        vsA.ValueChanged += ComponentAChanged;
        vsR.ValueChanged += ComponentRChanged;
        vsG.ValueChanged += ComponentGChanged;
        vsB.ValueChanged += ComponentBChanged;
    }

    public static readonly DependencyProperty ResultColorProperty = DependencyProperty.Register(nameof(ResultColor), typeof(Color), typeof(ColorPicker));

    public static readonly DependencyProperty ColorComponentAProperty = DependencyProperty.Register(nameof(ColorComponentA), typeof(byte), typeof(ColorPicker));

    public static readonly DependencyProperty ColorComponentRProperty = DependencyProperty.Register(nameof(ColorComponentR), typeof(byte), typeof(ColorPicker));

    public static readonly DependencyProperty ColorComponentGProperty = DependencyProperty.Register(nameof(ColorComponentG), typeof(byte), typeof(ColorPicker));

    public static readonly DependencyProperty ColorComponentBProperty = DependencyProperty.Register(nameof(ColorComponentB), typeof(byte), typeof(ColorPicker));


    public Color ResultColor
    {
        get => (Color)GetValue(ResultColorProperty);
        set => SetValue(ResultColorProperty, value);
    }

    public byte ColorComponentA
    {
        get => (byte)GetValue(ColorComponentAProperty);
        set => SetValue(ColorComponentAProperty, value);
    }

    public byte ColorComponentR
    {
        get => (byte)GetValue(ColorComponentRProperty);
        set => SetValue(ColorComponentRProperty, value);
    }

    public byte ColorComponentG
    {
        get => (byte)GetValue(ColorComponentGProperty);
        set => SetValue(ColorComponentGProperty, value);
    }

    public byte ColorComponentB
    {
        get => (byte)GetValue(ColorComponentBProperty);
        set => SetValue(ColorComponentBProperty, value);
    }


    protected override void OnPropertyChanged(DependencyPropertyChangedEventArgs e)
    {
        if(e.Property.Name == nameof(ResultColor))
        {
            if(e.OldValue == e.NewValue)
                return;
            ColorComponentA = ResultColor.A;
            ColorComponentR = ResultColor.R;
            ColorComponentG = ResultColor.G;
            ColorComponentB = ResultColor.B;
            vsA.Value = ColorComponentA;
            vsR.Value = ColorComponentR;
            vsG.Value = ColorComponentG;
            vsB.Value = ColorComponentB;
        }

        base.OnPropertyChanged(e);
    }


    private void ComponentBChanged(int obj)
    {
        ColorComponentB = (byte)obj;
        ResultColor = Color.FromArgb(ColorComponentA, ColorComponentR, ColorComponentG, ColorComponentB);
    }

    private void ComponentGChanged(int obj)
    {
        ColorComponentG = (byte)obj;
        ResultColor = Color.FromArgb(ColorComponentA, ColorComponentR, ColorComponentG, ColorComponentB);
    }

    private void ComponentRChanged(int obj)
    {
        ColorComponentR = (byte)obj;
        ResultColor = Color.FromArgb(ColorComponentA, ColorComponentR, ColorComponentG, ColorComponentB);
    }

    private void ComponentAChanged(int obj)
    {
        ColorComponentA = (byte)obj;
        ResultColor = Color.FromArgb(ColorComponentA, ColorComponentR, ColorComponentG, ColorComponentB);
    }

}
