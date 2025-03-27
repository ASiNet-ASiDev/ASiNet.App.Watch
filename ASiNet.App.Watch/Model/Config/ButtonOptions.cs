using System.Text.Json.Serialization;

namespace ASiNet.App.Watch.Model.Config;
public class ButtonOptions
{
    [JsonPropertyName("show_text")]
    public bool ShowText { get; set; }
    [JsonPropertyName("color_geometry_default")]
    public ConfigBrush GeometryColor { get; set; } = null!;
    [JsonPropertyName("color_text_default")]
    public ConfigBrush TextColor { get; set; } = null!;
    [JsonPropertyName("color_geometry_enter")]
    public ConfigBrush MouseEnterGeometryColor { get; set; } = null!;
    [JsonPropertyName("color_text_enter")]
    public ConfigBrush MouseEnterTextColor { get; set; } = null!;
    [JsonPropertyName("color_geometry_pressed")]
    public ConfigBrush PressedGeometryColor { get; set; } = null!;
    [JsonPropertyName("color_text_pressed")]
    public ConfigBrush PressedTextColor { get; set; } = null!;
}
