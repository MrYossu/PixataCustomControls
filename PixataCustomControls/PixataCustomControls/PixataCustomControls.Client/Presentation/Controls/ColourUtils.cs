using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Shapes;

namespace PixataCustomControls.Presentation.Controls {
  public static class ColourUtils {
    // Helper method to convert the string to a colour object
    public static Color GetColourFromName(string ColorName) {
      Line lne = (Line)XamlReader.Load("<Line xmlns=\"http://schemas.microsoft.com/winfx/2006/xaml/presentation\" xmlns:x=\"http://schemas.microsoft.com/winfx/2006/xaml\" Fill=\"" + ColorName + "\" />");
      return (Color)lne.Fill.GetValue(SolidColorBrush.ColorProperty);
    }
  }
}