﻿using System.Windows;
using System.Windows.Input;
using ASiNet.App.Watch.ViewModel;

namespace ASiNet.App.Watch.View;
public partial class WatchWindow : Window
{
    public WatchWindow()
    {
        InitializeComponent();
    }

    private bool _isMoved;
    protected override void OnActivated(EventArgs e)
    {
        base.OnActivated(e);
    }

    private void Window_MouseMove(object sender, MouseEventArgs e)
    {
        if (_isMoved && e.LeftButton == MouseButtonState.Pressed)
            DragMove();
    }

    private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
    {
        if (!((WatchWindowVM)DataContext).WindowContextMenu.IsPinned)
            _isMoved = true;
    }

    private void Window_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
    {
        _isMoved = false;
#if !DEBUG
        Win32Helper.SendWpfWindowBack(this);
#endif
    }

    private void CloseBtn_Click(object sender, RoutedEventArgs e)
    {
        ((WatchWindowVM)DataContext).ClosedCommand?.Execute(null);
        Close();
    }

    private void Window_Loaded(object sender, RoutedEventArgs e)
    {
#if DEBUG
        Win32Helper.SendWpfWindowAsToolWindow(this);
        Win32Helper.SendWpfWindowBack(this);
#endif
        ((WatchWindowVM?)DataContext)?.InitCommand?.Execute(null);
    }

    protected override void OnPropertyChanged(DependencyPropertyChangedEventArgs e)
    {
        if(e.Property.Name == nameof(Width)) 
        {

        }
        else if(e.Property.Name == nameof(Height))
        {
            
        }
        base.OnPropertyChanged(e);
    }
}
