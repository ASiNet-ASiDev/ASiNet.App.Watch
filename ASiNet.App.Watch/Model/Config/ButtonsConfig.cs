using System.Text.Json.Serialization;

namespace ASiNet.App.Watch.Model.Config;
public class ButtonsConfig
{

    [JsonPropertyName("clock")]
    public ButtonOptions ClockButton { get; set; } = null!;

    [JsonPropertyName("timer")]
    public ButtonOptions TimerButton { get; set; } = null!;

    [JsonPropertyName("stopwatch")]
    public ButtonOptions StopWatchButton { get; set; } = null!;

}
