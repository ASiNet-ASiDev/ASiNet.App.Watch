namespace ASiNet.App.Watch.View.Separators;
/// <summary>
/// Логика взаимодействия для ClassicSeparator.xaml
/// </summary>
public partial class ClassicSeparator : BaseSegment
{
    public ClassicSeparator()
    {
        InitializeComponent();
        _segment = Segment;
        ActiveSegment = true;
    }
}
