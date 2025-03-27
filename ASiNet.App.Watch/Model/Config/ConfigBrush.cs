using System.Text.Json.Serialization;
using System.Windows;
using System.Windows.Media;

namespace ASiNet.App.Watch.Model.Config;

[JsonPolymorphic(TypeDiscriminatorPropertyName = "brush_type")]
[JsonDerivedType(typeof(SolidBrush), "solid")]
public abstract class ConfigBrush
{
    [JsonPropertyName("hex")]
    public string HEXColor { get; set; } = null!;



    public static Brush ReadBrush(ConfigBrush brush, Brush? def = null)
    {
        if(brush is SolidBrush solid)
        {
            try
            {
                var color = (Color)ColorConverter.ConvertFromString(solid.HEXColor);
                return new SolidColorBrush(color);
            }
            catch (FormatException)
            {
                MessageBox.Show($"Color '{brush.HEXColor}' it is not a color of the supported format '#00000000'-'#FFFFFFFF'.", "Read color error :(", MessageBoxButton.OK, MessageBoxImage.Error);
                return def ?? throw new NullReferenceException();
            }
        }
        throw new NotImplementedException();
    }


    public static ConfigBrush FromBrush(Brush brush)
    {
        if(brush is SolidColorBrush solid)
        {
            var a = solid.Color.A;
            var r = solid.Color.R;
            var g = solid.Color.G;
            var b = solid.Color.B;
            
            var hex = '#' + Convert.ToHexString([a, r, g, b]);

            var res = new SolidBrush()
            {
                HEXColor = hex,
            };
            
            return res;
        }
        throw new NotImplementedException();
    }

}
