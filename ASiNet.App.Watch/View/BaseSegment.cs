using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace ASiNet.App.Watch.View;

public class BaseSegment : UserControl
{
    public BaseSegment()
    {
        _currentSegmentColor = new SolidColorBrush(Colors.White);
    }

    public static readonly DependencyProperty SelectedSegmentProperty = DependencyProperty.Register(nameof(SelectedSegment), typeof(bool), typeof(BaseSegment));
    public static readonly DependencyProperty ActiveSegmentProperty = DependencyProperty.Register(nameof(ActiveSegment), typeof(bool), typeof(BaseSegment));
    public static readonly DependencyProperty ActiveColorProperty = DependencyProperty.Register(nameof(ActiveColor), typeof(Brush), typeof(BaseSegment));
    public static readonly DependencyProperty InactiveColorProperty = DependencyProperty.Register(nameof(InactiveColor), typeof(Brush), typeof(BaseSegment));
    public static readonly DependencyProperty SelectedColorProperty = DependencyProperty.Register(nameof(SelectedColor), typeof(Brush), typeof(BaseSegment));



    protected Path _segment = null!;

    public bool ActiveSegment
    {
        get => (bool)GetValue(ActiveSegmentProperty);
        set => SetValue(ActiveSegmentProperty, value);
    }

    public bool SelectedSegment
    {
        get => (bool)GetValue(SelectedSegmentProperty);
        set => SetValue(SelectedSegmentProperty, value);
    }

    public Brush ActiveColor
    {
        get => (Brush)GetValue(ActiveColorProperty);
        set => SetValue(ActiveColorProperty, value);
    }

    public Brush InactiveColor
    {
        get => (Brush)GetValue(InactiveColorProperty);
        set => SetValue(InactiveColorProperty, value);
    }

    public Brush SelectedColor
    {
        get => (Brush)GetValue(SelectedColorProperty);
        set => SetValue(SelectedColorProperty, value);
    }

    private Brush _currentSegmentColor;

    protected override void OnPropertyChanged(DependencyPropertyChangedEventArgs e)
    {
        switch (e.Property.Name)
        {
            case nameof(ActiveSegment):
                SetSegmentColor();
                break;
            case nameof(SelectedSegment):
                SetSegmentColor();
                break;
            case nameof(ActiveColor):
                SetSegmentColor();
                break;
            case nameof(InactiveColor):
                SetSegmentColor();
                break;
            case nameof(SelectedColor):
                SetSegmentColor();
                break;
        }

        base.OnPropertyChanged(e);
    }

    private void SetSegmentColor()
    {
        if(ActiveSegment)
        {
            if (SelectedSegment)
                _currentSegmentColor = SelectedColor;
            else
                _currentSegmentColor = ActiveColor;
        }
        else
            _currentSegmentColor = InactiveColor;

        _segment.Fill = _currentSegmentColor;
    }
}
