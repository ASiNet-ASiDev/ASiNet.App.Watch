using System.Windows.Media;
using CommunityToolkit.Mvvm.ComponentModel;

namespace ASiNet.App.Watch.ViewModel;
public partial class SegmentVM : ObservableObject
{
    public SegmentVM()
    {
        Active = true;
        InactiveColor = new SolidColorBrush(Color.FromArgb(0, 0, 0, 0));
        ActiveColor = new SolidColorBrush(Colors.White);
    }

    [ObservableProperty]
    public partial bool Active { get; set; }
    [ObservableProperty]
    public partial Brush ActiveColor { get; set; }

    [ObservableProperty]
    public partial Brush InactiveColor { get; set; }


    public void SwitchActive(int _)
    {
        Active = !Active;
    }
}
