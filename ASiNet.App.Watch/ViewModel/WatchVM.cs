using ASiNet.App.Watch.Model;

namespace ASiNet.App.Watch.ViewModel;
public partial class WatchVM : ClockBaseVM
{
    public WatchVM(WatchUpdater updater, ParametersVM parameters) : base(updater, parameters)
    {
        ShowOptions = false;
        Interactive = false;
        _updater = updater;
    }

    private WatchUpdater _updater;

    public override void Init()
    {
        Start();
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
        HourMinuteSplitter.Active = true;
    }

    public override void Reset()
    {
        
    }
}
