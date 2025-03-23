using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ASiNet.App.Watch.View.Numbers;

namespace ASiNet.App.Watch.View.Clocks;

public partial class ClassicClock : UserControl
{
    public ClassicClock()
    {
        InitializeComponent();
    }

    public static readonly DependencyProperty InteractResultProperty = DependencyProperty.Register(nameof(InteractResult), typeof(TimeSpan), typeof(ClassicClock));
    public static readonly DependencyProperty AllowInteractiveProperty = DependencyProperty.Register(nameof(AllowInteractive), typeof(bool), typeof(ClassicClock));


    public TimeSpan InteractResult
    {
        get => (TimeSpan)GetValue(InteractResultProperty); 
        set => SetValue(InteractResultProperty, value);
    }
    public bool AllowInteractive
    {
        get => (bool)GetValue(AllowInteractiveProperty);
        set => SetValue(AllowInteractiveProperty, value);
    }


    private void HourFirstSegment_MouseWheel(object sender, MouseWheelEventArgs e)
    {
        if(!AllowInteractive)
            return;
        var offset = TimeSpan.FromHours(10);
        SetInteractResult(e.Delta > 0 ? offset : offset.Negate());
        #if DEBUG
        Debug.WriteLine($"CLOCK_INTERACTIVE_HOUR_FIRST");
        #endif
    }

    private void HourLastSegment_MouseWheel(object sender, MouseWheelEventArgs e)
    {
        if (!AllowInteractive)
            return;
        var offset = TimeSpan.FromHours(1);
        SetInteractResult(e.Delta > 0 ? offset : offset.Negate());
#if DEBUG
        Debug.WriteLine($"CLOCK_INTERACTIVE_HOUR_LAST");
#endif
    }

    private void FirstMinute_MouseWheel(object sender, MouseWheelEventArgs e)
    {
        if (!AllowInteractive)
            return;
        var offset = TimeSpan.FromMinutes(10);
        SetInteractResult(e.Delta > 0 ? offset : offset.Negate());
#if DEBUG
        Debug.WriteLine($"CLOCK_INTERACTIVE_MINUTES_FIRST");
#endif
    }

    private void LastMinute_MouseWheel(object sender, MouseWheelEventArgs e)
    {
        if (!AllowInteractive)
            return;
        var offset = TimeSpan.FromMinutes(1);
        SetInteractResult(e.Delta > 0 ? offset : offset.Negate());
#if DEBUG
        Debug.WriteLine($"CLOCK_INTERACTIVE_MINUTES_LAST");
#endif
    }

    private void FirstSecond_MouseWheel(object sender, MouseWheelEventArgs e)
    {
        if (!AllowInteractive)
            return;
        var offset = TimeSpan.FromSeconds(10);
        SetInteractResult(e.Delta > 0 ? offset : offset.Negate());
#if DEBUG
        Debug.WriteLine($"CLOCK_INTERACTIVE_SECOND_FIRST");
#endif
    }

    private void LastSecond_MouseWheel(object sender, MouseWheelEventArgs e)
    {
        if (!AllowInteractive)
            return;
        var offset = TimeSpan.FromSeconds(1);
        SetInteractResult(e.Delta > 0 ? offset : offset.Negate());
#if DEBUG
        Debug.WriteLine($"CLOCK_INTERACTIVE_SECOND_LAST");
#endif
    }


    private void SetInteractResult(TimeSpan offset)
    {
        var res = InteractResult.Add(offset);
        if (res.TotalHours >= 99.99)
            res = new TimeSpan(99, 59, 59);
        else if (res.TotalHours <= 0)
            res = new TimeSpan(0, 0, 1);
        InteractResult = res;
    }

    private void FirstHourInteractBox_MouseEnter(object sender, MouseEventArgs e) =>
        SelectNumberChange(HourFirstSegment, true);

    private void FirstHourInteractBox_MouseLeave(object sender, MouseEventArgs e) =>
        SelectNumberChange(HourFirstSegment, false);

    private void LastHourInteractBox_MouseEnter(object sender, MouseEventArgs e) =>
        SelectNumberChange(HourLastSegment, true);

    private void LastHourInteractBox_MouseLeave(object sender, MouseEventArgs e) =>
        SelectNumberChange(HourLastSegment, false);

    private void FirstMinuteInteractBox_MouseEnter(object sender, MouseEventArgs e) =>
        SelectNumberChange(FirstMinute, true);

    private void FirstMinuteInteractBox_MouseLeave(object sender, MouseEventArgs e) =>
        SelectNumberChange(FirstMinute, false);

    private void LastMinuteInteractBox_MouseEnter(object sender, MouseEventArgs e) =>
        SelectNumberChange(LastMinute, true);

    private void LastMinuteInteractBox_MouseLeave(object sender, MouseEventArgs e) =>
        SelectNumberChange(LastMinute, false);

    private void FirstSecondInteractBox_MouseEnter(object sender, MouseEventArgs e) =>
        SelectNumberChange(FirstSecond, true);

    private void FirstSecondInteractBox_MouseLeave(object sender, MouseEventArgs e) =>
        SelectNumberChange(FirstSecond, false);

    private void LastSecondInteractBox_MouseEnter(object sender, MouseEventArgs e) =>
        SelectNumberChange(LastSecond, true);

    private void LastSecondInteractBox_MouseLeave(object sender, MouseEventArgs e) =>
        SelectNumberChange(LastSecond, false);

    private void SelectNumberChange(ClassicNumber number, bool selected)
    {
        if(!AllowInteractive)
            return;
        number.IsSelected = selected;
    }
}
