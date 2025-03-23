namespace ASiNet.App.Watch.Model;
public interface IClockUpdater : IDisposable
{
    public event Action<int>? UpdatedFirstSecond;
    public event Action<int>? UpdatedLastSecond;
    public event Action<int>? UpdatedFirstMinute;
    public event Action<int>? UpdatedLastMinute;
    public event Action<int>? UpdatedFirstHour;
    public event Action<int>? UpdatedLastHour;


    public double UPS { get; set; }

    public bool IsRunning { get; }

    public void Start();

    public void Stop();

    public void CallUpdate();
}
