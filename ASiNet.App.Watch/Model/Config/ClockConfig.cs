using System.ComponentModel;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Windows;

namespace ASiNet.App.Watch.Model.Config;
public class ClockConfig
{
    [JsonPropertyName("state_more_options_hide")]
    public bool HideMoreOptions { get; set; }

    [JsonPropertyName("spacing")]
    public SpacingConfig ClockSegmentsSpacing { get; set; } = null!;

    [JsonPropertyName("colors")]
    public ColorsConfig Colors { get; set; } = null!;




    public static ClockConfig ReadOrDefault()
    {
        var cnfPath = Path.Join(AppDomain.CurrentDomain.BaseDirectory, "config.json");
        if (File.Exists(cnfPath))
        {
            try
            {
                using (var fs = File.OpenRead(cnfPath))
                {
                    return JsonSerializer.Deserialize<ClockConfig>(fs) ?? Default;
                }
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

    public static void Write(ClockConfig config)
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


    public static ClockConfig Default => new()
    {
        HideMoreOptions = false,


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
                    HEXColor = "#10000000"
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
                    HEXColor = "#10000000"
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
                    HEXColor = "#10000000"
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
                    HEXColor = "#10000000"
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
                    HEXColor = "#10000000"
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
                    HEXColor = "#10000000"
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
                    HEXColor = "#10000000"
                },
                SelectedSegmentColor = new SolidBrush()
                {
                    HEXColor = "#FFFF0000"
                },
            },
        },
    };
}
