using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ASiNet.App.Watch.View.Segments;
/// <summary>
/// Логика взаимодействия для ClassicSegment.xaml
/// </summary>
public partial class ClassicSegmentHorizontal : BaseSegment
{
    public ClassicSegmentHorizontal()
    {
        InitializeComponent();
        _segment = Segment;
    }
}
