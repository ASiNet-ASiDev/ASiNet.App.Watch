using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Media;
using ASiNet.App.Watch.Model.Config;
using ASiNet.App.Watch.View;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace ASiNet.App.Watch.ViewModel;
public partial class ParametersVM : ObservableObject
{

    public ParametersVM()
    {
        var cnf = AppConfig.ReadOrDefault();
        _config = cnf;
        UpdateValuesFromConfig();
    }


    [ObservableProperty]
    public partial double AppWidth { get; set; }

    [ObservableProperty]
    public partial double AppHeight { get; set; }

    [ObservableProperty]
    public partial bool HideMoreOptions { get; set; }
    [ObservableProperty]
    public partial NumberParametersVM HourFirst { get; set; } = null!;

    [ObservableProperty]
    public partial NumberParametersVM HourLast { get; set; } = null!;

    [ObservableProperty]
    public partial NumberParametersVM MinuteFirst { get; set; } = null!;

    [ObservableProperty]
    public partial NumberParametersVM MinuteLast { get; set; } = null!;

    [ObservableProperty]
    public partial NumberParametersVM SecondFirst { get; set; } = null!;

    [ObservableProperty]
    public partial NumberParametersVM SecondLast { get; set; } = null!;

    [ObservableProperty]
    public partial NumberParametersVM HourMinuteSplitter { get; set; } = null!;


    [ObservableProperty]
    public partial ButtonParametersVM ClockButton { get; set; } = null!;
    [ObservableProperty]
    public partial ButtonParametersVM TimerButton { get; set; } = null!;
    [ObservableProperty]
    public partial ButtonParametersVM StopWatchButton { get; set; } = null!;


    [ObservableProperty]
    public partial int HourSpace { get; set; }
    [ObservableProperty]
    public partial int HourMinuteSplitterSpace { get; set; }
    [ObservableProperty]
    public partial int MinuteSpace { get; set; }
    [ObservableProperty]
    public partial int MinuteSecondSpace { get; set; }
    [ObservableProperty]
    public partial int SecondSpace { get; set; }


    private AppConfig _config;

    [RelayCommand]
    private void ShowHideMoreOptions()
    {
        HideMoreOptions = !HideMoreOptions;
        AppConfig.Write(_config);
    }


    [RelayCommand]
    private void OpenClockSpacingOptions()
    {
        var ow = new OptionsWindow() { DataContext = this };
        ow.ShowDialog();
        AppConfig.Write(_config);
    }

    [RelayCommand]
    private void SaveConfig()
    {
        AppConfig.Write(_config);
    }


    [RelayCommand]
    private void LoadConfig()
    {
        _config = AppConfig.ReadOrDefault();
        UpdateValuesFromConfig();
    }


    [RelayCommand]
    public void ToDefault()
    {
        _config = AppConfig.Default;
        UpdateValuesFromConfig();
        AppConfig.Write(_config);
    }



    [RelayCommand]
    private void AppSizeSet(string preset)
    {
        switch (preset)
        {
            case "0":
                _config.AppWidth = 300;
                _config.AppHeight = 150;
                break;
            case "1":
                _config.AppWidth = 600;
                _config.AppHeight = 300;
                break;
            case "2":
                _config.AppWidth = 650;
                _config.AppHeight = 350;
                break;
            case "3":
                _config.AppWidth = 750;
                _config.AppHeight = 400;
                break;
            case "4":
                _config.AppWidth = 850;
                _config.AppHeight = 400;
                break;
            case "5":
                _config.AppWidth = 950;
                _config.AppHeight = 450;
                break;
            case "6":
                _config.AppWidth = 1000;
                _config.AppHeight = 500;
                break;
        }
        UpdateValuesFromConfig();
        AppConfig.Write(_config);
    }



    private void UpdateValuesFromConfig()
    {
        try
        {
            AppWidth = _config.AppWidth <= 100 ? 100 : _config.AppWidth;
            AppHeight = _config.AppHeight <= 50 ? 50 : _config.AppHeight;
            HideMoreOptions = _config.HideMoreOptions;
            HourSpace = _config.ClockSegmentsSpacing.HourSpace;
            MinuteSpace = _config.ClockSegmentsSpacing.MinuteSpace;
            SecondSpace = _config.ClockSegmentsSpacing.SecondSpace;
            MinuteSecondSpace = _config.ClockSegmentsSpacing.MinuteSecondSpace;
            HourMinuteSplitterSpace = _config.ClockSegmentsSpacing.HourMinuteSplitterSpace;

            (HourFirst ??= new(_config.Colors.HourFirst)).SetColors(_config.Colors.HourFirst);
            (HourLast ??= new(_config.Colors.HourLast)).SetColors(_config.Colors.HourLast);

            (MinuteFirst ??= new(_config.Colors.MinuteFirst)).SetColors(_config.Colors.MinuteFirst);
            (MinuteLast ??= new(_config.Colors.MinuteLast)).SetColors(_config.Colors.MinuteLast);

            (SecondFirst ??= new(_config.Colors.SecondFirst)).SetColors(_config.Colors.SecondFirst);
            (SecondLast ??= new(_config.Colors.SecondLast)).SetColors(_config.Colors.SecondLast);

            (HourMinuteSplitter ??= new(_config.Colors.HourMinuteSplitter)).SetColors(_config.Colors.HourMinuteSplitter);

            (ClockButton ??= new(_config.ButtonsConfig.ClockButton)).SetColors(_config.ButtonsConfig.ClockButton);
            (TimerButton ??= new(_config.ButtonsConfig.TimerButton)).SetColors(_config.ButtonsConfig.TimerButton);
            (StopWatchButton ??= new(_config.ButtonsConfig.StopWatchButton)).SetColors(_config.ButtonsConfig.StopWatchButton);
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Message:\n{ex.Message}", "Read config error :(", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}
