using System.Windows.Media;
using System.Windows.Threading;
using CommunityToolkit.Mvvm.ComponentModel;

namespace ASiNet.App.Watch.ViewModel;
public partial class NumberVM : ObservableObject
{
    public NumberVM()
    {
        SelectedColor = new SolidColorBrush(Colors.Red);
        InactiveColor = new SolidColorBrush(Color.FromArgb(0,0,0,0));
        ActiveColor = new SolidColorBrush(Colors.White);
    }


    [ObservableProperty]
    public partial int Number { get; set; } = -1;

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

}
