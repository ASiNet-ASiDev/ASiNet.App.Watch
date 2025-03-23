using System.Windows;
using System.Windows.Controls;

namespace ASiNet.App.Watch.View.Numbers;

public partial class ClassicNumber : Viewbox
{
    public ClassicNumber()
    {
        InitializeComponent();
        SetNumberActive(0);
    }


    public static readonly DependencyProperty IsSelectedProperty = DependencyProperty.Register(nameof(IsSelected), typeof(bool), typeof(ClassicNumber));
    public static readonly DependencyProperty NumberProperty = DependencyProperty.Register(nameof(Number), typeof(int), typeof(ClassicNumber));


    public int Number
    {
        get => (int)GetValue(NumberProperty);
        set => SetValue(NumberProperty, value);
    }

    public bool IsSelected
    {
        get => (bool)GetValue(IsSelectedProperty);
        set => SetValue(IsSelectedProperty, value);
    }


    private static readonly bool[][] _patterns = [
        // TM // TR // DR // DM // DL // TL // MM
        [false, false, false, false, false, false, true], // NaN(-1)
        [true, true, true, true, true, true, false], // 0
        [false, true, true, false, false, false, false], // 1
        [true, true, false, true, true, false, true], // 2
        [true, true, true, true, false, false, true], // 3
        [false, true, true, false, false, true, true], // 4
        [true, false, true, true, false, true, true], // 5
        [true, false, true, true, true, true, true], // 6
        [true, true, true, false, false, false, false], // 7
        [true, true, true, true, true, true, true], // 8
        [true, true, true, true, false, true, true], // 9
        ];

    protected override void OnPropertyChanged(DependencyPropertyChangedEventArgs e)
    {
        if(e.Property.Name == nameof(Number))
        {
            SetNumberActive(Number + 1);
        }
        else if (e.Property.Name == nameof(IsSelected))
        {
            SetNumberSelected(IsSelected);
        }
        else if(e.Property.Name == nameof(DataContext))
        {
            SetNumberActive(Number + 1);
        }
        base.OnPropertyChanged(e);
    }


    private void SetNumberActive(int number)
    {
        SegmentTopMiddle.ActiveSegment = _patterns[number][0];
        SegmentTopRight.ActiveSegment = _patterns[number][1];
        SegmentButtonRight.ActiveSegment = _patterns[number][2];
        SegmentButtonMiddle.ActiveSegment = _patterns[number][3];
        SegmentButtonLeft.ActiveSegment = _patterns[number][4];
        SegmentTopLeft.ActiveSegment = _patterns[number][5];
        SegmentMiddleMiddle.ActiveSegment = _patterns[number][6];
    }

    private void SetNumberSelected(bool isSelected)
    {
        SegmentTopMiddle.SelectedSegment = isSelected;
        SegmentTopRight.SelectedSegment = isSelected;
        SegmentButtonRight.SelectedSegment = isSelected;
        SegmentButtonMiddle.SelectedSegment = isSelected;
        SegmentButtonLeft.SelectedSegment = isSelected;
        SegmentTopLeft.SelectedSegment = isSelected;
        SegmentMiddleMiddle.SelectedSegment = isSelected;
    }
}
