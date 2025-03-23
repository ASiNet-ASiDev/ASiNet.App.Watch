using ASiNet.App.Watch.Model;

namespace ASiNet.App.Watch.ViewModel;
public partial class StopWatchVM : ClockBaseVM
{
    public StopWatchVM(StopWatchUpdater updater, ParametersVM parameters) : base(updater, parameters)
    {
        ShowOptions = true;
        _updater = updater;
        _updater.TimeUp += OnTimerTimeUp;
    }

    private StopWatchUpdater _updater;

    public override void Init()
    {
        Interactive = false;
        IsRunning = false;
        SetAllIndicatorsToValue(TimeSpan.Zero);
    }

    public override void Start()
    {
        IsRunning = true;
        _updater.Start();
    }

    public override void Stop()
    {
        IsRunning = false;
        _updater.Stop();
    }

    public override void Reset()
    {
        if (IsRunning)
            return;
        _updater.CurrentTime = TimeSpan.Zero;
        SetAllIndicatorsToValue(TimeSpan.Zero);
        HourMinuteSplitter.Active = true;
    }

    private void OnTimerTimeUp()
    {
        Stop();
    }
}
