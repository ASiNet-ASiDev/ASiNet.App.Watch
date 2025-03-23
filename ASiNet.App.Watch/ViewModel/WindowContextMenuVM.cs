using System.ComponentModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace ASiNet.App.Watch.ViewModel;
public partial class WindowContextMenuVM : ObservableObject
{

    [ObservableProperty]
    public partial bool IsPinned { get; set; }

    [ObservableProperty]
    public partial string PinnedItemHeader { get; set; } = "Pin window";


    [RelayCommand]
    private void ChangeIsPinned()
    {
        IsPinned = !IsPinned;
    }


    protected override void OnPropertyChanged(PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(IsPinned))
        {
            PinnedItemHeader = IsPinned ? "Unpin window" : "Pin window";
        }
        base.OnPropertyChanged(e);
    }
}
