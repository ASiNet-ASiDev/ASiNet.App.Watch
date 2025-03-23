using ASiNet.App.Watch.Model;

namespace ASiNet.App.Watch.ViewModel;
public partial class TimerVM : ClockBaseVM
{
    public TimerVM(TimerUpdater updater, ParametersVM parameters) : base(updater, parameters)
    {
        ShowOptions = true;
        _updater = updater;
        _updater.TimeUp += OnTimerTimeUp;
    }

    private TimerUpdater _updater;

    public override void Init()
    {
        Interactive = true;
        IsRunning = false;
        SetAllIndicatorsToValue(InteractiveResult);
    }

    public override void Start()
    {
        Interactive = false;
        IsRunning = true;
        if(_updater.CurrentTime == TimeSpan.Zero)
            _updater.CurrentTime = InteractiveResult;
        _updater.Start();
    }

    public override void Stop()
    {
        Interactive = true;
        IsRunning = false;
        _updater.Stop();
        HourMinuteSplitter.Active = true;
    }


    public override void Reset()
    {
        if(IsRunning)
            return;
        _updater.CurrentTime = InteractiveResult;
        SetAllIndicatorsToValue(InteractiveResult);
    }

    private void OnTimerTimeUp()
    {
        Stop();
    }
}
