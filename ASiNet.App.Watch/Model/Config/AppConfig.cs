using System.ComponentModel;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Windows;

namespace ASiNet.App.Watch.Model.Config;
public class AppConfig
{


    [JsonPropertyName("app_width")]
    public int AppWidth { get; set; }

    [JsonPropertyName("app_height")]
    public int AppHeight { get; set; }

    [JsonPropertyName("state_more_options_hide")]
    public bool HideMoreOptions { get; set; }

    [JsonPropertyName("spacing")]
    public SpacingConfig ClockSegmentsSpacing { get; set; } = null!;

    [JsonPropertyName("colors")]
    public ColorsConfig Colors { get; set; } = null!;

    [JsonPropertyName("buttons")]
    public ButtonsConfig ButtonsConfig { get; set; } = null!;


    public static AppConfig ReadOrDefault()
    {
        var cnfPath = Path.Join(AppDomain.CurrentDomain.BaseDirectory, "config.json");
        if (File.Exists(cnfPath))
        {
            try
            {
                using var fs = File.OpenRead(cnfPath);
                var result = JsonSerializer.Deserialize<AppConfig>(fs) ?? Default;

                result.Colors ??= Default.Colors;
                result.ButtonsConfig ??= Default.ButtonsConfig;
                result.ClockSegmentsSpacing ??= Default.ClockSegmentsSpacing;


                return result;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Read config ERROR :(", MessageBoxButton.OK, MessageBoxImage.Error);
                return Default;
            }
        }
        else
        {
            var def = Default;
            Write(def);
            return def;
        }
    }

    public static void Write(AppConfig config)
    {
        var cnfPath = Path.Join(AppDomain.CurrentDomain.BaseDirectory, "config.json");

        try
        {
            using (var fs = File.Create(cnfPath))
            {
                JsonSerializer.Serialize(fs, config, new JsonSerializerOptions() { WriteIndented = true });
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message, "Write config ERROR :(", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }


    public static AppConfig Default => new()
    {
        HideMoreOptions = false,
        AppWidth = 600,
        AppHeight = 300,

        ClockSegmentsSpacing = new()
        { 
            HourMinuteSplitterSpace = 20,
            HourSpace = 10,
            MinuteSecondSpace = 25,
            MinuteSpace = 10,
            SecondSpace = 10,
        },
        Colors = new()
        {
            HourFirst = new()
            { 
                ActiveSegmentColor = new SolidBrush()
                {
                    HEXColor = "#FFFFFFFF",
                }, InactiveSegmentColor = new SolidBrush()
                { 
                    HEXColor = "#15FFFFFF"
                }, 
                SelectedSegmentColor = new SolidBrush()
                { 
                    HEXColor = "#FFFF0000"
                }, 
            },
            HourLast = new()
            {
                ActiveSegmentColor = new SolidBrush()
                {
                    HEXColor = "#FFFFFFFF",
                },
                InactiveSegmentColor = new SolidBrush()
                {
                    HEXColor = "#15FFFFFF"
                },
                SelectedSegmentColor = new SolidBrush()
                {
                    HEXColor = "#FFFF0000"
                },
            },
            MinuteFirst = new()
            {
                ActiveSegmentColor = new SolidBrush()
                {
                    HEXColor = "#FFFFFFFF",
                },
                InactiveSegmentColor = new SolidBrush()
                {
                    HEXColor = "#15FFFFFF"
                },
                SelectedSegmentColor = new SolidBrush()
                {
                    HEXColor = "#FFFF0000"
                },
            },
            MinuteLast = new()
            {
                ActiveSegmentColor = new SolidBrush()
                {
                    HEXColor = "#FFFFFFFF",
                },
                InactiveSegmentColor = new SolidBrush()
                {
                    HEXColor = "#15FFFFFF"
                },
                SelectedSegmentColor = new SolidBrush()
                {
                    HEXColor = "#FFFF0000"
                },
            },
            SecondFirst = new()
            {
                ActiveSegmentColor = new SolidBrush()
                {
                    HEXColor = "#FFFFFFFF",
                },
                InactiveSegmentColor = new SolidBrush()
                {
                    HEXColor = "#15FFFFFF"
                },
                SelectedSegmentColor = new SolidBrush()
                {
                    HEXColor = "#FFFF0000"
                },
            },
            SecondLast = new()
            {
                ActiveSegmentColor = new SolidBrush()
                {
                    HEXColor = "#FFFFFFFF",
                },
                InactiveSegmentColor = new SolidBrush()
                {
                    HEXColor = "#15FFFFFF"
                },
                SelectedSegmentColor = new SolidBrush()
                {
                    HEXColor = "#FFFF0000"
                },
            },
            HourMinuteSplitter = new()
            {
                ActiveSegmentColor = new SolidBrush()
                {
                    HEXColor = "#FFFFFFFF",
                },
                InactiveSegmentColor = new SolidBrush()
                {
                    HEXColor = "#15FFFFFF"
                },
                SelectedSegmentColor = new SolidBrush()
                {
                    HEXColor = "#FFFF0000"
                },
            },
        }, 
        ButtonsConfig = new()
        { 
            ClockButton = new()
            { 
                ShowText = false,
                GeometryColor = new SolidBrush()
                {
                    HEXColor = "#FFFFFFFF"
                },
                TextColor = new SolidBrush()
                {
                    HEXColor = "#FFFFFFFF"
                },
                MouseEnterGeometryColor = new SolidBrush()
                {
                    HEXColor = "#FFEEEEEE"
                },
                MouseEnterTextColor = new SolidBrush()
                {
                    HEXColor = "#FFEEEEEE"
                },
                PressedGeometryColor = new SolidBrush()
                {
                    HEXColor = "#FFFF0000"
                },
                PressedTextColor = new SolidBrush()
                {
                    HEXColor = "#FFFF0000"
                },
            },
            StopWatchButton = new()
            {
                ShowText = false,
                GeometryColor = new SolidBrush()
                {
                    HEXColor = "#FFFFFFFF"
                },
                TextColor = new SolidBrush()
                {
                    HEXColor = "#FFFFFFFF"
                },
                MouseEnterGeometryColor = new SolidBrush()
                {
                    HEXColor = "#FFEEEEEE"
                },
                MouseEnterTextColor = new SolidBrush()
                {
                    HEXColor = "#FFEEEEEE"
                },
                PressedGeometryColor = new SolidBrush()
                {
                    HEXColor = "#FFFF0000"
                },
                PressedTextColor = new SolidBrush()
                {
                    HEXColor = "#FFFF0000"
                },
            },
            TimerButton = new()
            {
                ShowText = false,
                GeometryColor = new SolidBrush()
                {
                    HEXColor = "#FFFFFFFF"
                },
                TextColor = new SolidBrush()
                {
                    HEXColor = "#FFFFFFFF"
                },
                MouseEnterGeometryColor = new SolidBrush()
                {
                    HEXColor = "#FFEEEEEE"
                },
                MouseEnterTextColor = new SolidBrush()
                {
                    HEXColor = "#FFEEEEEE"
                },
                PressedGeometryColor = new SolidBrush()
                {
                    HEXColor = "#FFFF0000"
                },
                PressedTextColor = new SolidBrush()
                {
                    HEXColor = "#FFFF0000"
                },
            },
        },
    };
}
