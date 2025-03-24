using System.Text.Json.Serialization;

namespace ASiNet.App.Watch.Model.Config;
public class SegmentColors
{
    [JsonPropertyName("selected")]
    public ConfigBrush SelectedSegmentColor { get; set; } = null!;
    [JsonPropertyName("active")]
    public ConfigBrush ActiveSegmentColor { get; set; } = null!;
    [JsonPropertyName("inactive")]
    public ConfigBrush InactiveSegmentColor { get; set; } = null!;
}
