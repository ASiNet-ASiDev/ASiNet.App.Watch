using System.Text.Json.Serialization;

namespace ASiNet.App.Watch.Model.Config;
public class ColorsConfig
{
    [JsonPropertyName("Hh")]
    public SegmentColors HourFirst { get; set; } = null!;
    [JsonPropertyName("hH")]
    public SegmentColors HourLast { get; set; } = null!;
    [JsonPropertyName("Mm")]
    public SegmentColors MinuteFirst { get; set; } = null!;
    [JsonPropertyName("mM")]
    public SegmentColors MinuteLast { get; set; } = null!;
    [JsonPropertyName("Ss")]
    public SegmentColors SecondFirst { get; set; } = null!;
    [JsonPropertyName("sS")]
    public SegmentColors SecondLast { get; set; } = null!;


    [JsonPropertyName("hhmm_splitter")]
    public SegmentColors HourMinuteSplitter { get; set; } = null!;

}
